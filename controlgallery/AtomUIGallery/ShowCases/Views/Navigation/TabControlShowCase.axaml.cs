using AtomUI.Desktop.Controls;
using AtomUI.Icons.AntDesign;
using AtomUIGallery.ShowCases.ViewModels;
using Avalonia.Interactivity;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.ShowCases.Views;

/// <summary>
/// 自訂標籤項目資料類別 - 擴充基本標籤項目資料
/// Custom tab item data class - Extends base tab item data
/// 新增內容屬性以支援標籤頁的內容顯示
/// Add content property to support tab page content display
/// </summary>
public class MyTabItemData : TabItemData
{
    /// <summary>
    /// 標籤頁內容
    /// Tab page content
    /// </summary>
    public object? Content { get; init; }
}

/// <summary>
/// 標籤控制項展示案例 - 展示標籤頁和標籤條的各種樣式和功能
/// Tab control showcase - Demonstrates various styles and functionalities of tabs and tab strips
/// 包含位置切換、尺寸調整、動態新增標籤等功能
/// Includes position switching, size adjustment, dynamic tab addition, etc.
/// </summary>
public partial class TabControlShowCase : ReactiveUserControl<TabControlViewModel>
{
    /// <summary>
    /// 建構函式 - 初始化標籤控制項展示案例
    /// Constructor - Initialize tab control showcase
    /// </summary>
    public TabControlShowCase()
    {
        // 當控件被啟用時初始化事件處理器和資料
        // Initialize event handlers and data when control is activated
        this.WhenActivated(disposables =>
        {
            if (DataContext is TabControlViewModel viewModel)
            {
                // 綁定標籤條位置選項變更事件
                // Bind tab strip position option changed events
                PositionTabStripOptionGroup.OptionCheckedChanged     += viewModel.HandleTabStripPlacementOptionCheckedChanged;
                PositionCardTabStripOptionGroup.OptionCheckedChanged += viewModel.HandleCardTabStripPlacementOptionCheckedChanged;
                
                // 綁定標籤條尺寸類型選項變更事件
                // Bind tab strip size type option changed event
                SizeTypeTabStripOptionGroup.OptionCheckedChanged     += viewModel.HandleTabStripSizeTypeOptionCheckedChanged;
                
                // 綁定標籤條新增標籤請求事件
                // Bind tab strip add tab request event
                AddTabDemoStrip.AddTabRequest                        += HandleTabStripAddTabRequest;
                
                // 綁定標籤控制項位置選項變更事件
                // Bind tab control position option changed events
                PositionTabControlOptionGroup.OptionCheckedChanged     += viewModel.HandleTabControlPlacementOptionCheckedChanged;
                PositionCardTabControlOptionGroup.OptionCheckedChanged += viewModel.HandleCardTabControlPlacementOptionCheckedChanged;
                
                // 綁定標籤控制項尺寸類型選項變更事件
                // Bind tab control size type option changed event
                SizeTypeTabControlOptionGroup.OptionCheckedChanged     += viewModel.HandleTabControlSizeTypeOptionCheckedChanged;
                
                // 綁定標籤控制項新增標籤請求事件
                // Bind tab control add tab request event
                AddTabDemoTabControl.AddTabRequest                     += HandleTabControlAddTabRequest;
                
                // 建立標籤控制項的示範資料 - 第一個標籤
                // Create demo data for tab control - First tab
                viewModel.TabItemDataSource.Add(new MyTabItemData()
                {
                    Header  = "Tab 1",
                    Content = "Tab Content 1",
                    Icon    = new WechatFilled()
                });
                
                // 建立標籤控制項的示範資料 - 第二個標籤（可關閉）
                // Create demo data for tab control - Second tab (closable)
                viewModel.TabItemDataSource.Add(new MyTabItemData()
                {
                    Header  = "Tab 2",
                    Content = "Tab Content 2",
                    IsClosable = true,  // 允許關閉此標籤 (Allow closing this tab)
                    Icon = new LinuxOutlined()
                });
                
                // 建立標籤條的示範資料
                // Create demo data for tab strip
                viewModel.TabStripItemDataSource.Add(new TabItemData()
                {
                    Header = "Tab 1"
                });
                viewModel.TabStripItemDataSource.Add(new TabItemData()
                {
                    Header = "Tab 2"
                });
            }
        });
        
        // 初始化 XAML 定義的元件
        // Initialize XAML-defined components
        InitializeComponent();
    }
    
    /// <summary>
    /// 處理標籤條新增標籤請求
    /// Handle tab strip add tab request
    /// 動態新增一個可關閉的標籤項目到標籤條
    /// Dynamically add a closable tab item to the tab strip
    /// </summary>
    /// <param name="sender">事件來源 (Event source)</param>
    /// <param name="args">路由事件參數 (Routed event arguments)</param>
    private void HandleTabStripAddTabRequest(object? sender, RoutedEventArgs args)
    {
        // 取得當前標籤數量作為新標籤的索引
        // Get current tab count as index for new tab
        var index = AddTabDemoStrip.ItemCount;
        
        // 新增可關閉的標籤項目
        // Add closable tab item
        AddTabDemoStrip.Items.Add(new TabStripItem
        {
            Content    = $"new tab {index}",
            IsClosable = true  // 允許使用者關閉此標籤 (Allow user to close this tab)
        });
    }
    
    /// <summary>
    /// 處理標籤控制項新增標籤請求
    /// Handle tab control add tab request
    /// 動態新增一個包含標題和內容的可關閉標籤頁
    /// Dynamically add a closable tab page with header and content
    /// </summary>
    /// <param name="sender">事件來源 (Event source)</param>
    /// <param name="args">路由事件參數 (Routed event arguments)</param>
    private void HandleTabControlAddTabRequest(object? sender, RoutedEventArgs args)
    {
        // 取得當前標籤數量作為新標籤的索引
        // Get current tab count as index for new tab
        var index = AddTabDemoTabControl.ItemCount;
        
        // 新增包含標題和內容的標籤項目
        // Add tab item with header and content
        AddTabDemoTabControl.Items.Add(new TabItem
        {
            Header     = $"new tab {index}",
            Content    = $"new tab content {index}",
            IsClosable = true  // 允許使用者關閉此標籤 (Allow user to close this tab)
        });
    }
}