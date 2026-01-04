using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace AtomUIGallery.Generator;

/// <summary>
/// 解析 AXAML 文件，提取 ShowCaseItem 的 Content 內容
/// </summary>
internal class ShowCaseItemAxamlParser
{
    private const string GalleryNamespace = "https://atomui.net/oss-controls/gallery";
    private const string ShowCaseItemElementName = "ShowCaseItem";

    public List<ShowCaseItemInfo> Parse(string axamlContent, string filePath)
    {
        var result = new List<ShowCaseItemInfo>();

        try
        {
            // 移除 x:Class 等可能導致解析問題的內容，並準備解析
            var doc = XDocument.Parse(axamlContent, LoadOptions.PreserveWhitespace);

            // 找到所有 ShowCaseItem 元素
            var showCaseItems = FindShowCaseItems(doc.Root);

            int index = 0;
            foreach (var item in showCaseItems)
            {
                var info = ExtractShowCaseItemInfo(item, axamlContent, index);
                if (info != null)
                {
                    result.Add(info);
                }
                index++;
            }
        }
        catch (Exception)
        {
            // 解析失敗時返回空列表
        }

        return result;
    }

    private IEnumerable<XElement> FindShowCaseItems(XElement? root)
    {
        if (root == null) yield break;

        var seen = new HashSet<XElement>();

        foreach (var element in root.DescendantsAndSelf())
        {
            var localName = element.Name.LocalName;
            var ns = element.Name.NamespaceName;

            bool isShowCaseItem = false;

            // 檢查是否是 ShowCaseItem (可能帶有前綴如 gallery:ShowCaseItem)
            if (localName == ShowCaseItemElementName ||
                localName.EndsWith(":" + ShowCaseItemElementName))
            {
                isShowCaseItem = true;
            }

            // 也檢查命名空間
            if (ns == GalleryNamespace && localName == ShowCaseItemElementName)
            {
                isShowCaseItem = true;
            }

            if (isShowCaseItem && seen.Add(element))
            {
                yield return element;
            }
        }
    }

    private ShowCaseItemInfo? ExtractShowCaseItemInfo(XElement element, string originalContent, int index)
    {
        // 獲取 Title 屬性
        var titleAttr = element.Attribute("Title");
        var title = titleAttr?.Value ?? $"ShowCase_{index}";

        // 提取 Content - 即 ShowCaseItem 的子元素內容
        var contentBuilder = new StringBuilder();
        var childElements = element.Elements()
            .Where(e => !IsPropertyElement(e, element))
            .ToList();

        if (childElements.Count == 0)
        {
            return null;
        }

        foreach (var child in childElements)
        {
            var childXml = FormatXmlContent(child);
            if (!string.IsNullOrWhiteSpace(childXml))
            {
                if (contentBuilder.Length > 0)
                {
                    contentBuilder.AppendLine();
                }
                contentBuilder.Append(childXml);
            }
        }

        var content = contentBuilder.ToString();
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        return new ShowCaseItemInfo
        {
            Title = title,
            Index = index,
            ContentXaml = content,
            SafeIdentifier = GenerateSafeIdentifier(title, index)
        };
    }

    /// <summary>
    /// 檢查元素是否是屬性元素 (如 ShowCaseItem.Content)
    /// </summary>
    private bool IsPropertyElement(XElement element, XElement parent)
    {
        var localName = element.Name.LocalName;
        var parentLocalName = parent.Name.LocalName;

        // 移除可能的命名空間前綴
        if (localName.Contains(":"))
        {
            localName = localName.Substring(localName.IndexOf(':') + 1);
        }
        if (parentLocalName.Contains(":"))
        {
            parentLocalName = parentLocalName.Substring(parentLocalName.IndexOf(':') + 1);
        }

        // 檢查是否是 Parent.PropertyName 格式
        return localName.StartsWith(parentLocalName + ".");
    }

    private string FormatXmlContent(XElement element)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "    ",
            OmitXmlDeclaration = true,
            NewLineHandling = NewLineHandling.Replace
        };

        var sb = new StringBuilder();
        using (var writer = XmlWriter.Create(sb, settings))
        {
            element.WriteTo(writer);
        }

        // 清理命名空間聲明，使輸出更簡潔
        var result = sb.ToString();
        result = CleanupNamespaceDeclarations(result);

        // 規範化縮排：移除所有行的共同前導空白
        result = NormalizeIndentation(result);

        return result;
    }

    /// <summary>
    /// 規範化縮排：找出除第一行外所有非空行的最小縮排量，然後從這些行中減去這個量
    /// </summary>
    private string NormalizeIndentation(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        if (lines.Length <= 1)
            return text;

        // 找出除第一行外所有非空行的最小縮排量
        int minIndent = int.MaxValue;
        for (int i = 1; i < lines.Length; i++) // 從第二行開始
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            int indent = GetLeadingSpaceCount(line);

            if (indent < minIndent)
                minIndent = indent;
        }

        // 如果沒有找到有效的縮排，或縮排為 0，直接返回
        if (minIndent == int.MaxValue || minIndent == 0)
            return text;

        // 從除第一行外的所有行中移除最小縮排量
        var resultBuilder = new StringBuilder();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            if (i == 0)
            {
                // 第一行保持原樣
                resultBuilder.Append(line);
                if (lines.Length > 1)
                    resultBuilder.AppendLine();
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
                // 空行保持原樣（但不要在最後一行添加換行）
                if (i < lines.Length - 1)
                    resultBuilder.AppendLine();
            }
            else
            {
                // 移除前導空白（最多移除 minIndent 個空格）
                var trimmedLine = RemoveLeadingSpaces(line, minIndent);
                resultBuilder.Append(trimmedLine);

                if (i < lines.Length - 1)
                    resultBuilder.AppendLine();
            }
        }

        return resultBuilder.ToString();
    }

    private int GetLeadingSpaceCount(string line)
    {
        int count = 0;
        foreach (var c in line)
        {
            if (c == ' ')
                count++;
            else if (c == '\t')
                count += 4; // 將 tab 視為 4 個空格
            else
                break;
        }
        return count;
    }

    private string RemoveLeadingSpaces(string line, int spacesToRemove)
    {
        int removed = 0;
        int charsToSkip = 0;

        foreach (var c in line)
        {
            if (removed >= spacesToRemove)
                break;

            if (c == ' ')
            {
                removed++;
                charsToSkip++;
            }
            else if (c == '\t')
            {
                removed += 4;
                charsToSkip++;
            }
            else
            {
                break;
            }
        }

        return line.Substring(charsToSkip);
    }

    private string CleanupNamespaceDeclarations(string xml)
    {
        // 移除常見的命名空間聲明，保持代碼簡潔
        var patterns = new[]
        {
            @"\s*xmlns=""[^""]*""",
            @"\s*xmlns:x=""[^""]*""",
            @"\s*xmlns:atom=""[^""]*""",
            @"\s*xmlns:gallery=""[^""]*""",
            @"\s*xmlns:antdicons=""[^""]*""",
            @"\s*xmlns:[a-zA-Z]+=""[^""]*"""
        };

        foreach (var pattern in patterns)
        {
            xml = Regex.Replace(xml, pattern, "", RegexOptions.IgnoreCase);
        }

        return xml.Trim();
    }

    private string GenerateSafeIdentifier(string title, int index)
    {
        // 將標題轉換為有效的 C# 標識符
        var identifier = new StringBuilder();

        foreach (var c in title)
        {
            if (char.IsLetterOrDigit(c))
            {
                identifier.Append(c);
            }
            else if (c == ' ' || c == '-' || c == '_')
            {
                identifier.Append('_');
            }
        }

        var result = identifier.ToString();

        // 確保不以數字開頭
        if (result.Length == 0 || char.IsDigit(result[0]))
        {
            result = $"Item_{index}_{result}";
        }

        return result;
    }
}

internal class ShowCaseItemInfo
{
    public string Title { get; set; } = string.Empty;
    public int Index { get; set; }
    public string ContentXaml { get; set; } = string.Empty;
    public string SafeIdentifier { get; set; } = string.Empty;
}
