using AtomUI.Desktop.Controls;
using AtomUI.Icons.AntDesign;
using AtomUIGallery.ShowCases.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.ShowCases.Views;

/// <summary>
/// 通知提醒展示案例 - 展示通知提醒框的各種類型和位置
/// Notification showcase - Demonstrates various types and positions of notification boxes
/// 通知提醒框用於在視窗邊緣顯示較為詳細的通知訊息，可配置不同的顯示位置
/// Notification box is used to display more detailed notification messages at window edges with configurable positions
/// </summary>
public partial class NotificationShowCase : ReactiveUserControl<NotificationViewModel>
{
    // 各種位置的通知管理器
    // Notification managers for different positions
    
    /// <summary>基本通知管理器（預設位置：右上角）(Basic manager - default position: top right)</summary>
    private WindowNotificationManager? _basicManager;
    
    /// <summary>左上角通知管理器 (Top left notification manager)</summary>
    private WindowNotificationManager? _topLeftManager;
    
    /// <summary>頂部中央通知管理器 (Top center notification manager)</summary>
    private WindowNotificationManager? _topManager;
    
    /// <summary>右上角通知管理器 (Top right notification manager)</summary>
    private WindowNotificationManager? _topRightManager;

    /// <summary>左下角通知管理器 (Bottom left notification manager)</summary>
    private WindowNotificationManager? _bottomLeftManager;
    
    /// <summary>底部中央通知管理器 (Bottom center notification manager)</summary>
    private WindowNotificationManager? _bottomManager;
    
    /// <summary>右下角通知管理器 (Bottom right notification manager)</summary>
    private WindowNotificationManager? _bottomRightManager;
    
    /// <summary>
    /// 建構函式 - 初始化通知提醒展示案例
    /// Constructor - Initialize notification showcase
    /// </summary>
    public NotificationShowCase()
    {
        this.WhenActivated(disposables => { });
        
        // 初始化 XAML 定義的元件
        // Initialize XAML-defined components
        InitializeComponent();
        
        // 綁定滑鼠懸停選項變更事件
        // Bind hover option changed event
        HoverOptionGroup.OptionCheckedChanged += HandleHoverOptionGroupCheckedChanged;
    }
    
    /// <summary>
    /// 處理滑鼠懸停選項變更事件
    /// Handle hover option change event
    /// 控制通知在滑鼠懸停時是否暫停自動關閉計時
    /// Control whether notification pauses auto-close timer when mouse hovers
    /// </summary>
    /// <param name="sender">事件來源 (Event source)</param>
    /// <param name="args">選項變更事件參數 (Option changed event arguments)</param>
    private void HandleHoverOptionGroupCheckedChanged(object? sender, OptionCheckedChangedEventArgs args)
    {
        if (_basicManager is not null)
        {
            if (args.Index == 0)
            {
                // 啟用滑鼠懸停時暫停自動關閉
                // Enable pause on hover
                _basicManager.IsPauseOnHover = true;
            }
            else
            {
                // 停用滑鼠懸停時暫停自動關閉
                // Disable pause on hover
                _basicManager.IsPauseOnHover = false;
            }
        }
    }

    /// <summary>
    /// 當控件附加到視覺樹時呼叫
    /// Called when control is attached to visual tree
    /// 初始化所有位置的通知管理器
    /// Initialize notification managers for all positions
    /// </summary>
    /// <param name="e">視覺樹附加事件參數 (Visual tree attachment event arguments)</param>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        var topLevel = TopLevel.GetTopLevel(this);
        
        // 基本通知管理器（預設右上角）
        // Basic notification manager (default top right)
        _basicManager = new WindowNotificationManager(topLevel)
        {
            MaxItems = 3  // 最多同時顯示 3 個通知 (Max 3 notifications displayed simultaneously)
        };

        // 左上角通知管理器
        // Top left notification manager
        _topLeftManager = new WindowNotificationManager(topLevel)
        {
            MaxItems = 3,
            Position = NotificationPosition.TopLeft
        };

        // 頂部中央通知管理器
        // Top center notification manager
        _topManager = new WindowNotificationManager(topLevel)
        {
            Position = NotificationPosition.TopCenter,
            MaxItems = 3
        };

        // 右上角通知管理器
        // Top right notification manager
        _topRightManager = new WindowNotificationManager(topLevel)
        {
            Position = NotificationPosition.TopRight,
            MaxItems = 3
        };

        // 左下角通知管理器
        // Bottom left notification manager
        _bottomLeftManager = new WindowNotificationManager(topLevel)
        {
            Position = NotificationPosition.BottomLeft,
            MaxItems = 3
        };

        // 底部中央通知管理器
        // Bottom center notification manager
        _bottomManager = new WindowNotificationManager(topLevel)
        {
            Position = NotificationPosition.BottomCenter,
            MaxItems = 3
        };

        // 右下角通知管理器
        // Bottom right notification manager
        _bottomRightManager = new WindowNotificationManager(topLevel)
        {
            Position = NotificationPosition.BottomRight,
            MaxItems = 3
        };
    }

    /// <summary>
    /// 顯示簡單通知 - 只包含標題和內容的基本通知
    /// Show simple notification - Basic notification with only title and content
    /// </summary>
    private void ShowSimpleNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            "Notification Title",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>
    /// 顯示不自動關閉的通知 - 需要手動關閉
    /// Show never-closing notification - Requires manual closing
    /// 設定過期時間為 Zero 表示永不自動關閉
    /// Setting expiration to Zero means never auto-close
    /// </summary>
    private void ShowNeverCloseNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            expiration: TimeSpan.Zero,  // 永不過期 (Never expires)
            title: "Notification Title",
            content:
            "I will never close automatically. This is a purposely very very long description that has many many characters and words."
        ));
    }

    /// <summary>
    /// 顯示成功通知 - 帶有綠色成功圖示的通知
    /// Show success notification - Notification with green success icon
    /// </summary>
    private void ShowSuccessNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            type: NotificationType.Success,
            title: "Notification Title",
            content:
            "This is the content of the notification. This is the content of the notification. This is the content of the notification."
        ));
    }

    /// <summary>
    /// 顯示資訊通知 - 帶有藍色資訊圖示的通知
    /// Show info notification - Notification with blue information icon
    /// </summary>
    private void ShowInfoNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            type: NotificationType.Information,
            title: "Notification Title",
            content:
            "This is the content of the notification. This is the content of the notification. This is the content of the notification."
        ));
    }

    /// <summary>
    /// 顯示警告通知 - 帶有黃色警告圖示的通知
    /// Show warning notification - Notification with yellow warning icon
    /// </summary>
    private void ShowWarningNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            type: NotificationType.Warning,
            title: "Notification Title",
            content:
            "This is the content of the notification. This is the content of the notification. This is the content of the notification."
        ));
    }

    /// <summary>
    /// 顯示錯誤通知 - 帶有紅色錯誤圖示的通知
    /// Show error notification - Notification with red error icon
    /// </summary>
    private void ShowErrorNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            type: NotificationType.Error,
            title: "Notification Title",
            content:
            "This is the content of the notification. This is the content of the notification. This is the content of the notification."
        ));
    }

    // === 不同位置的通知展示方法 ===
    // === Methods for showing notifications at different positions ===

    /// <summary>顯示頂部中央通知 (Show top center notification)</summary>
    private void ShowTopNotification(object? sender, RoutedEventArgs e)
    {
        _topManager?.Show(new Notification(
            "Notification Top",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>顯示底部中央通知 (Show bottom center notification)</summary>
    private void ShowBottomNotification(object? sender, RoutedEventArgs e)
    {
        _bottomManager?.Show(new Notification(
            "Notification Bottom",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>顯示左上角通知 (Show top left notification)</summary>
    private void ShowTopLeftNotification(object? sender, RoutedEventArgs e)
    {
        _topLeftManager?.Show(new Notification(
            "Notification TopLeft",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>顯示右上角通知 (Show top right notification)</summary>
    private void ShowTopRightNotification(object? sender, RoutedEventArgs e)
    {
        _topRightManager?.Show(new Notification(
            "Notification TopRight",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>顯示左下角通知 (Show bottom left notification)</summary>
    private void ShowBottomLeftNotification(object? sender, RoutedEventArgs e)
    {
        _bottomLeftManager?.Show(new Notification(
            "Notification BottomLeft",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>顯示右下角通知 (Show bottom right notification)</summary>
    private void ShowBottomRightNotification(object? sender, RoutedEventArgs e)
    {
        _bottomRightManager?.Show(new Notification(
            "Notification BottomRight",
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>
    /// 顯示自訂圖示通知 - 使用自訂圖示替代預設的類型圖示
    /// Show custom icon notification - Use custom icon to replace default type icon
    /// </summary>
    private void ShowCustomIconNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            "Notification Title",
            "This is the content of the notification. This is the content of the notification. This is the content of the notification.",
            icon: new SettingOutlined()  // 使用設定圖示 (Use settings icon)
        ));
    }

    /// <summary>
    /// 顯示帶進度條的通知 - 在通知底部顯示倒數計時進度條
    /// Show notification with progress bar - Display countdown progress bar at bottom of notification
    /// 進度條可視化顯示通知自動關閉的剩餘時間
    /// Progress bar visualizes remaining time before notification auto-closes
    /// </summary>
    private void ShowProgressNotification(object? sender, RoutedEventArgs e)
    {
        _basicManager?.Show(new Notification(
            type: NotificationType.Information,
            title: "Notification Title",
            content:
            "This is the content of the notification. This is the content of the notification. This is the content of the notification.",
            showProgress: true  // 顯示進度條 (Show progress bar)
        ));
    }
}