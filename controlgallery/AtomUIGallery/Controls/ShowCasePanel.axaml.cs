using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Metadata;
using AtomUIGallery.ShowCases;
using AvaloniaControlList = Avalonia.Controls.Controls;

namespace AtomUIGallery.Controls;

public class ShowCasePanel : TemplatedControl
{
    internal const string MainPanelPart = "PART_MainPanel";

    private bool _initialized;
    private Grid? _layoutPanel;

    [Content]
    public AvaloniaControlList Children { get; } = new();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        var effectCount = 0;
        var showCaseIndex = 0;

        // 獲取父級 ShowCase 頁面的類名
        var showCaseClassName = GetShowCaseClassName();

        foreach (var child in Children)
        {
            if (child is ShowCaseItem showCaseItem)
            {
                // 設置索引
                showCaseItem.Index = showCaseIndex;

                // 嘗試從生成的代碼中獲取源代碼
                if (showCaseClassName != null)
                {
                    var sourceCode = ShowCaseSourceCodeProvider.GetSourceCode(showCaseClassName, showCaseIndex);
                    if (sourceCode != null)
                    {
                        showCaseItem.SourceCode = sourceCode;
                    }
                }

                showCaseIndex++;
                effectCount++;
                if (showCaseItem.IsOccupyEntireRow)
                {
                    effectCount++;
                }
            }
        }
        if (effectCount % 2 != 0)
        {
            var extra = new ShowCaseItem()
            {
                IsFake = true
            };
            Children.Add(extra);
        }
        base.OnApplyTemplate(e);
        _layoutPanel = e.NameScope.Get<Grid>(MainPanelPart);
        if (_layoutPanel != null && !_initialized)
        {
            var row = 0;
            var column = 0;

            for (var i = 0; i < Children.Count; ++i)
            {
                if (Children[i] is ShowCaseItem item)
                {
                    if (item.IsOccupyEntireRow)
                    {
                        if (column != 0)
                        {
                            row++;
                        }
                        Grid.SetRow(item, row++);

                        Grid.SetColumn(item, 0);
                        Grid.SetColumnSpan(item, 2);
                    }
                    else
                    {
                        Grid.SetRow(item, row);
                        Grid.SetColumn(item, column++);
                        if (column == 2)
                        {
                            row++;
                            column = 0;
                        }
                    }
                    _layoutPanel.Children.Add(item);
                    LogicalChildren.Add(item);
                }
            }

            var rowDefinitions = new RowDefinitions();
            for (var i = 0; i < row; ++i)
            {
                rowDefinitions.Add(new RowDefinition(GridLength.Auto));
            }
            _layoutPanel.RowDefinitions = rowDefinitions;
            _initialized                = true;
        }
    }

    internal virtual void NotifyAboutToActive()
    {
    }

    internal virtual void NotifyActivated()
    {
    }

    internal virtual void NotifyAboutToDeactivated()
    {
    }

    internal virtual void NotifyDeactivated()
    {
    }

    /// <summary>
    /// 獲取包含此 ShowCasePanel 的 ShowCase 頁面的類名
    /// </summary>
    private string? GetShowCaseClassName()
    {
        // 向上遍歷視覺樹，找到 UserControl 類型的父級
        var parent = this.Parent;
        while (parent != null)
        {
            if (parent is UserControl userControl)
            {
                return userControl.GetType().Name;
            }
            parent = (parent as Control)?.Parent;
        }
        return null;
    }
}