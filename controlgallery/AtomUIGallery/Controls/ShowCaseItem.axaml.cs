using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace AtomUIGallery.Controls;

public class ShowCaseItem : ContentControl {
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<ShowCaseItem, string>(nameof(Title));

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<ShowCaseItem, string>(nameof(Description));

    public static readonly StyledProperty<bool> IsOccupyEntireRowProperty =
        AvaloniaProperty.Register<ShowCaseItem, bool>(nameof(IsOccupyEntireRow));

    public static readonly StyledProperty<bool?> IsCheckedProperty =
        ToggleButton.IsCheckedProperty.AddOwner<ShowCaseItem>();

    /// <summary>
    /// 此 ShowCaseItem 在 ShowCasePanel 中的索引（用於查找對應的源代碼）
    /// </summary>
    internal static readonly StyledProperty<int> IndexProperty =
        AvaloniaProperty.Register<ShowCaseItem, int>(nameof(Index), -1);

    /// <summary>
    /// 源代碼內容（AXAML 純文字）
    /// </summary>
    public static readonly StyledProperty<string?> SourceCodeProperty =
        AvaloniaProperty.Register<ShowCaseItem, string?>(nameof(SourceCode));

    internal static readonly StyledProperty<bool> IsFakeProperty =
        AvaloniaProperty.Register<ShowCaseItem, bool>(nameof(IsFake), false);

    public string Title {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public bool IsOccupyEntireRow {
        get => GetValue(IsOccupyEntireRowProperty);
        set => SetValue(IsOccupyEntireRowProperty, value);
    }

    public bool IsFake {
        get => GetValue(IsFakeProperty);
        set => SetValue(IsFakeProperty, value);
    }

    public bool? IsChecked {
        get => GetValue(IsCheckedProperty) ?? false;
        set => SetValue(IsCheckedProperty, value);
    }

    internal int Index {
        get => GetValue(IndexProperty);
        set => SetValue(IndexProperty, value);
    }

    /// <summary>
    /// 獲取或設置此 ShowCaseItem 的 Content 的 AXAML 源代碼
    /// </summary>
    public string? SourceCode {
        get => GetValue(SourceCodeProperty);
        set => SetValue(SourceCodeProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
        base.OnApplyTemplate(e);

        var codeBlock = e.NameScope.Find<TextBox>("PART_CodeBlock");
        if (codeBlock is not null && SourceCode is not null) {
            if (SourceCode is not null) {
                codeBlock.Text = SourceCode;
            }
            else {
                codeBlock.IsVisible = false;
            }
        }
    }
}