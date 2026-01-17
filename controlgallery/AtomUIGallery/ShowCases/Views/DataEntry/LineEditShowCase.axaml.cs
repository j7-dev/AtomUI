using AtomUIGallery.ShowCases.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.ShowCases.Views;

/// <summary>
/// 文字輸入框展示案例 - 展示各種文字輸入控件的樣式和功能
/// Line edit (text input) showcase - Demonstrates various styles and functionalities of text input controls
/// 這是一個簡單的展示案例，主要透過 XAML 定義來展示控件特性
/// This is a simple showcase case that mainly demonstrates control features through XAML definitions
/// </summary>
public partial class LineEditShowCase : ReactiveUserControl<LineEditViewModel>
{
    /// <summary>
    /// 建構函式 - 初始化文字輸入框展示案例
    /// Constructor - Initialize line edit showcase
    /// </summary>
    public LineEditShowCase()
    {
        // 當控件被啟用時執行（此處無需額外初始化邏輯）
        // Execute when control is activated (no additional initialization logic needed here)
        this.WhenActivated(disposables => { });
        
        // 初始化 XAML 定義的元件
        // Initialize XAML-defined components
        InitializeComponent();
    }
}