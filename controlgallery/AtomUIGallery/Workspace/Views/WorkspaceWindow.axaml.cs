using System.Diagnostics;
using AtomUI.Controls;
using AtomUI.Desktop.Controls;
using AtomUI.Theme.Language;
using AtomUIGallery.Workspace.ViewModels;
using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace AtomUIGallery.Workspace.Views;

/// <summary>
/// 視窗選單項目類型列舉 - 定義所有可用的視窗選單操作
/// Window menu item kind enumeration - Defines all available window menu operations
/// </summary>
internal enum WindowMenuItemKind 
{
    FullScreen,     // 全螢幕 (Full screen)
    Pin,            // 釘選視窗 (Pin window)
    Minimize,       // 最小化 (Minimize)
    Maximize,       // 最大化 (Maximize)
    Move,           // 移動視窗 (Move window)
    Resize,         // 調整大小 (Resize)
    DarkMode,       // 深色模式 (Dark mode)
    Compact,        // 緊湊模式 (Compact mode)
    Motion,         // 動畫效果 (Motion effects)
    WaveSpirit,     // 波浪精神效果 (Wave spirit effect)
    LanguageZhCN,   // 簡體中文語言 (Simplified Chinese language)
    LanguageEnUS,   // 英文語言 (English language)
}

/// <summary>
/// 工作區主視窗類別 - Gallery 應用程式的主要視窗
/// Workspace main window class - Main window of the Gallery application
/// 負責管理視窗狀態、主題切換、語言切換等功能
/// Responsible for managing window state, theme switching, language switching, etc.
/// </summary>
public partial class WorkspaceWindow : ReactiveWindow<WorkspaceWindowViewModel>
{
    /// <summary>
    /// 語言資源識別碼
    /// Language resource identifier
    /// </summary>
    public const string LanguageId = nameof(WorkspaceWindow);
    
    /// <summary>
    /// 建構函式 - 初始化工作區視窗
    /// Constructor - Initialize workspace window
    /// </summary>
    public WorkspaceWindow()
    {
#if DEBUG
        // 在偵錯模式下附加開發者工具（F12）
        // Attach developer tools (F12) in debug mode
        this.AttachDevTools();
#endif
        // 設定視圖模型
        // Set view model
        DataContext = new WorkspaceWindowViewModel();
        
        // 初始化 XAML 定義的視窗元件
        // Initialize XAML-defined window components
        InitializeComponent();
        
        // 註冊選單項目勾選狀態改變事件處理器
        // Register menu item check state changed event handler
        AddHandler(MenuItem.IsCheckStateChangedEvent, HandleMenuItemCheckChanged);
    }

    /// <summary>
    /// 顯示視窗時的覆寫方法
    /// Override method when showing the window
    /// 重設視窗尺寸為自動調整
    /// Reset window size to auto-adjust
    /// </summary>
    public override void Show()
    {
        base.Show();
        // 設定高度和寬度為 NaN（自動調整）
        // Set height and width to NaN (auto-adjust)
        Height = double.NaN;
        Width  = double.NaN;
    }

    /// <summary>
    /// 處理選單項目勾選狀態改變事件
    /// Handle menu item check state changed event
    /// 根據不同的選單項目類型執行對應的操作
    /// Execute corresponding operations based on different menu item types
    /// </summary>
    /// <param name="sender">事件來源 (Event source)</param>
    /// <param name="e">事件參數 (Event arguments)</param>
    private void HandleMenuItemCheckChanged(object? sender, RoutedEventArgs e)
    {
        if (e.Source is MenuItem menuItem && menuItem.Tag is WindowMenuItemKind kind)
        {
            // 取得應用程式實例
            // Get application instance
            var application = Application.Current;
            Debug.Assert(application != null);
            
            if (kind == WindowMenuItemKind.FullScreen)
            {
                // 啟用或停用全螢幕按鈕
                // Enable or disable full screen button
                IsFullScreenCaptionButtonEnabled = menuItem.IsChecked;
            }
            else if (kind == WindowMenuItemKind.Pin)
            {
                // 啟用或停用釘選按鈕（視窗置頂）
                // Enable or disable pin button (window always on top)
                IsPinCaptionButtonEnabled = menuItem.IsChecked;
            }
            else if (kind == WindowMenuItemKind.Minimize)
            {
                // 啟用或停用最小化功能
                // Enable or disable minimize functionality
                CanMinimize = menuItem.IsChecked;
            }
            else if (kind == WindowMenuItemKind.Maximize)
            {
                // 啟用或停用最大化功能
                // Enable or disable maximize functionality
                CanMaximize = menuItem.IsChecked;
            }
            else if (kind == WindowMenuItemKind.Move)
            {
                // 啟用或停用視窗移動功能
                // Enable or disable window move functionality
                IsMoveEnabled = menuItem.IsChecked;
            }
            else if (kind == WindowMenuItemKind.Resize)
            {
                // 啟用或停用視窗大小調整功能
                // Enable or disable window resize functionality
                CanResize = menuItem.IsChecked;
            }
            else if (kind == WindowMenuItemKind.DarkMode)
            {
                // 切換深色主題模式
                // Toggle dark theme mode
                Dispatcher.UIThread.Post(() =>
                {
                    application.SetDarkThemeMode(menuItem.IsChecked);
                });
            }
            else if (kind == WindowMenuItemKind.Compact)
            {
                // 切換緊湊主題模式（較小的間距和控件尺寸）
                // Toggle compact theme mode (smaller spacing and control sizes)
                Dispatcher.UIThread.Post(() =>
                {
                    application.SetCompactThemeMode(menuItem.IsChecked);
                });
            }
            else if (kind == WindowMenuItemKind.Motion)
            {
                // 啟用或停用動畫效果
                // Enable or disable motion effects
                if (menuItem.Parent is MenuItem themeMenuItem)
                {
                    // 當停用動畫時，也停用波浪精神效果
                    // When disabling motion, also disable wave spirit effect
                    foreach (var item in themeMenuItem.Items)
                    {
                        if (item is MenuItem themeMenuChildItem && themeMenuChildItem.Tag is WindowMenuItemKind themeMenuChildItemKind)
                        {
                            if (themeMenuChildItemKind == WindowMenuItemKind.WaveSpirit)
                            {
                                if (!menuItem.IsChecked)
                                {
                                    themeMenuChildItem.IsChecked = false;
                                }
                            }
                        }
                    }
                }
                application.SetMotionEnabled(menuItem.IsChecked);
            }
            else if (kind == WindowMenuItemKind.WaveSpirit)
            {
                // 啟用或停用波浪精神效果（特殊的動畫效果）
                // Enable or disable wave spirit effect (special animation effect)
                application.SetWaveSpiritEnabled(menuItem.IsChecked);
            }
            else if (kind == WindowMenuItemKind.LanguageZhCN)
            {
                // 切換到簡體中文語言
                // Switch to Simplified Chinese language
                Dispatcher.UIThread.Post(() =>
                {
                    application.SetLanguageVariant(LanguageVariant.zh_CN);
                });
            }
            else if (kind == WindowMenuItemKind.LanguageEnUS)
            {
                // 切換到英文語言
                // Switch to English language
                Dispatcher.UIThread.Post(() =>
                {
                    application.SetLanguageVariant(LanguageVariant.en_US);
                });
            }
        }
    }
}