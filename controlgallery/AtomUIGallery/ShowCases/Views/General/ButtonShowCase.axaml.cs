using AtomUI;
using AtomUI.Desktop.Controls;
using AtomUIGallery.ShowCases.ViewModels;
using Avalonia.Interactivity;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.ShowCases.Views;

/// <summary>
/// 按鈕展示案例控件 - 展示各種按鈕樣式和功能
/// Button showcase control - Demonstrates various button styles and functionalities
/// 使用 ReactiveUI 的 MVVM 模式，支援響應式資料繫結
/// Uses ReactiveUI's MVVM pattern, supporting reactive data binding
/// </summary>
public partial class ButtonShowCase : ReactiveUserControl<ButtonViewModel>
{
    /// <summary>
    /// 按鈕視圖模型的私有欄位
    /// Private field for button view model
    /// </summary>
    private ButtonViewModel? _viewModel;
    
    /// <summary>
    /// 建構函式 - 初始化按鈕展示案例
    /// Constructor - Initialize button showcase
    /// </summary>
    public ButtonShowCase()
    {
        // 當控件被啟用時設定視圖模型
        // Set view model when control is activated
        this.WhenActivated(disposables =>
        {
            // 從 DataContext 取得視圖模型
            // Get view model from DataContext
            _viewModel = DataContext as ButtonViewModel;
        });
        
        // 初始化 XAML 定義的元件
        // Initialize XAML-defined components
        InitializeComponent();
    }
    
    /// <summary>
    /// 處理按鈕尺寸類型選項變更事件
    /// Handle button size type option changed event
    /// 根據選擇的索引更新按鈕的尺寸類型（大、中、小）
    /// Update button size type (Large, Middle, Small) based on selected index
    /// </summary>
    /// <param name="sender">事件來源 (Event source)</param>
    /// <param name="args">選項變更事件參數 (Option changed event arguments)</param>
    public void HandleButtonSizeTypeOptionCheckedChanged(object? sender, OptionCheckedChangedEventArgs args)
    {
        if (_viewModel != null)
        {
            if (args.Index == 0)
            {
                // 設定為大尺寸按鈕
                // Set to large size button
                _viewModel.ButtonSizeType = SizeType.Large;
            }
            else if (args.Index == 1)
            {
                // 設定為中等尺寸按鈕（預設）
                // Set to middle size button (default)
                _viewModel.ButtonSizeType = SizeType.Middle;
            }
            else
            {
                // 設定為小尺寸按鈕
                // Set to small size button
                _viewModel.ButtonSizeType = SizeType.Small;
            }
        }
        
    }

    /// <summary>
    /// 處理載入中按鈕點擊事件
    /// Handle loading button click event
    /// 顯示按鈕的載入狀態效果（旋轉圖示），3 秒後自動恢復
    /// Show button loading state effect (spinning icon), automatically recover after 3 seconds
    /// </summary>
    /// <param name="sender">事件來源（被點擊的按鈕）(Event source - clicked button)</param>
    /// <param name="args">路由事件參數 (Routed event arguments)</param>
    public void HandleLoadingBtnClick(object? sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            // 啟用按鈕的載入狀態
            // Enable button loading state
            button.IsLoading = true;
            
            // 使用 UI 執行緒的非同步操作
            // Use UI thread asynchronous operation
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                // 等待 3 秒鐘
                // Wait for 3 seconds
                await Task.Delay(TimeSpan.FromSeconds(3));
                
                // 停用按鈕的載入狀態
                // Disable button loading state
                button.IsLoading = false;
            });
        }
    }
}