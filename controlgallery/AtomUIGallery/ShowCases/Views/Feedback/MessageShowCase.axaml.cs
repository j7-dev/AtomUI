using AtomUI.Desktop.Controls;
using AtomUIGallery.ShowCases.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.ShowCases.Views;

/// <summary>
/// 訊息提示展示案例 - 展示全域訊息提示控件的各種類型和功能
/// Message showcase - Demonstrates various types and functionalities of global message toast component
/// 訊息提示用於在頁面頂部顯示短暫的通知訊息
/// Message toast is used to display brief notification messages at the top of the page
/// </summary>
public partial class MessageShowCase : ReactiveUserControl<MessageViewModel>
{
    /// <summary>
    /// 視窗訊息管理器 - 負責管理和顯示訊息提示
    /// Window message manager - Responsible for managing and displaying message toasts
    /// </summary>
    private WindowMessageManager? _messageManager;
    
    /// <summary>
    /// 建構函式 - 初始化訊息展示案例
    /// Constructor - Initialize message showcase
    /// </summary>
    public MessageShowCase()
    {
        this.WhenActivated(disposables => { });
        
        // 初始化 XAML 定義的元件
        // Initialize XAML-defined components
        InitializeComponent();
    }
    
    /// <summary>
    /// 當控件附加到視覺樹時呼叫
    /// Called when control is attached to visual tree
    /// 初始化訊息管理器並設定最大顯示數量
    /// Initialize message manager and set maximum display count
    /// </summary>
    /// <param name="e">視覺樹附加事件參數 (Visual tree attachment event arguments)</param>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        
        // 取得頂層視窗
        // Get top level window
        var topLevel = TopLevel.GetTopLevel(this);
        
        // 建立訊息管理器，設定最多同時顯示 10 個訊息
        // Create message manager, set maximum 10 messages displayed simultaneously
        _messageManager = new WindowMessageManager(topLevel)
        {
            MaxItems = 10
        };
    }

    /// <summary>
    /// 顯示簡單訊息 - 最基本的訊息提示
    /// Show simple message - Most basic message toast
    /// </summary>
    private void ShowSimpleMessage(object? sender, RoutedEventArgs e)
    {
        _messageManager?.Show(new Message(
            "Hello, AtomUI/Avalonia!"
        ));
    }

    /// <summary>
    /// 顯示資訊訊息 - 帶有藍色資訊圖示的訊息
    /// Show info message - Message with blue information icon
    /// </summary>
    private void ShowInfoMessage(object? sender, RoutedEventArgs e)
    {
        _messageManager?.Show(new Message(
            type: MessageType.Information,
            content: "This is a information message."
        ));
    }

    /// <summary>
    /// 顯示成功訊息 - 帶有綠色成功圖示的訊息
    /// Show success message - Message with green success icon
    /// 通常用於操作成功後的反饋
    /// Usually used for feedback after successful operation
    /// </summary>
    private void ShowSuccessMessage(object? sender, RoutedEventArgs e)
    {
        _messageManager?.Show(new Message(
            type: MessageType.Success,
            content: "This is a success message."
        ));
    }

    /// <summary>
    /// 顯示警告訊息 - 帶有黃色警告圖示的訊息
    /// Show warning message - Message with yellow warning icon
    /// 用於提醒使用者注意某些情況
    /// Used to remind users to pay attention to certain situations
    /// </summary>
    private void ShowWarningMessage(object? sender, RoutedEventArgs e)
    {
        _messageManager?.Show(new Message(
            type: MessageType.Warning,
            content: "This is a warning message."
        ));
    }

    /// <summary>
    /// 顯示錯誤訊息 - 帶有紅色錯誤圖示的訊息
    /// Show error message - Message with red error icon
    /// 用於通知使用者操作失敗或發生錯誤
    /// Used to notify users of operation failure or errors
    /// </summary>
    private void ShowErrorMessage(object? sender, RoutedEventArgs e)
    {
        _messageManager?.Show(new Message(
            type: MessageType.Error,
            content: "This is a error message."
        ));
    }

    /// <summary>
    /// 顯示載入中訊息 - 帶有旋轉載入圖示的訊息
    /// Show loading message - Message with spinning loading icon
    /// 用於顯示正在處理中的操作狀態
    /// Used to display status of operations in progress
    /// </summary>
    private void ShowLoadingMessage(object? sender, RoutedEventArgs e)
    {
        _messageManager?.Show(new Message(
            type: MessageType.Loading,
            content: "Action in progress..."
        ));
    }

    /// <summary>
    /// 顯示連續訊息 - 展示訊息的鏈式顯示效果
    /// Show sequential messages - Demonstrate chained message display effect
    /// 第一個訊息關閉後自動顯示下一個訊息，形成連續的通知流程
    /// Automatically show next message after first one closes, forming continuous notification flow
    /// </summary>
    private void ShowSequentialMessage(object? sender, RoutedEventArgs e)
    {
        // 第一個訊息：載入中（持續 2.5 秒）
        // First message: Loading (lasting 2.5 seconds)
        _messageManager?.Show(new Message(
            type: MessageType.Loading,
            content: "Action in progress...",
            expiration: TimeSpan.FromSeconds(2.5),
            onClose: () =>
            {
                // 第二個訊息：載入完成（持續 2.5 秒）
                // Second message: Loading finished (lasting 2.5 seconds)
                _messageManager?.Show(new Message(
                    type: MessageType.Success,
                    expiration: TimeSpan.FromSeconds(2.5),
                    content: "Loading finished",
                    onClose: () =>
                    {
                        // 第三個訊息：最終資訊提示
                        // Third message: Final information prompt
                        _messageManager?.Show(new Message(
                            type: MessageType.Information,
                            expiration: TimeSpan.FromSeconds(2.5),
                            content: "Loading finished"
                        ));
                    }
                ));
            }
        ));
    }
}