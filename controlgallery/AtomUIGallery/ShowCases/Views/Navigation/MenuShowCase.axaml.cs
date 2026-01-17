using AtomUI.Desktop.Controls;
using AtomUI.Desktop.Controls.Primitives;
using AtomUI.Icons.AntDesign;
using AtomUIGallery.ShowCases.ViewModels;
using Avalonia.Input;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.ShowCases.Views;

/// <summary>
/// 選單展示案例控件 - 展示各種選單樣式和功能
/// Menu showcase control - Demonstrates various menu styles and functionalities
/// 包含導覽選單、下拉選單、右鍵選單和浮出選單
/// Includes navigation menus, dropdown menus, context menus, and flyout menus
/// </summary>
public partial class MenuShowCase : ReactiveUserControl<MenuViewModel>
{
    /// <summary>
    /// 導覽選單預設選取項目
    /// Default selected item for navigation menu
    /// </summary>
    private NavMenuItemData? _navMenuDefaultSelectedItem;
    
    /// <summary>
    /// 建構函式 - 初始化選單展示案例
    /// Constructor - Initialize menu showcase
    /// </summary>
    public MenuShowCase()
    {
        // 當控件被啟用時初始化視圖模型和選單資料
        // Initialize view model and menu data when control is activated
        this.WhenActivated(disposables =>
        {
            if (DataContext is MenuViewModel viewModel)
            {
                // 綁定模式切換開關事件
                // Bind mode change switch event
                ChangeModeSwitch.IsCheckedChanged  += viewModel.HandleChangeModeCheckChanged;
                // 綁定樣式切換開關事件
                // Bind style change switch event
                ChangeStyleSwitch.IsCheckedChanged += viewModel.HandleChangeStyleCheckChanged;
                
                // 設定預設展開的選單路徑
                // Set default open menu paths
                var defaultOpenPaths = new List<TreeNodePath>();
                defaultOpenPaths.Add(new TreeNodePath("/3/SubGroup2"));
                viewModel.DefaultOpenPaths    = defaultOpenPaths;
                
                // 設定預設選取的選單項目路徑
                // Set default selected menu item path
                viewModel.DefaultSelectedPath = new TreeNodePath("/3/SubGroup1/Option1");
                
                // 初始化各種類型的選單
                // Initialize different types of menus
                InitNavMenuTreeNodes(viewModel);      // 導覽選單 (Navigation menu)
                InitMenuTreeNodes(viewModel);         // 下拉選單 (Dropdown menu)
                InitContextMenuItems(viewModel);      // 右鍵選單 (Context menu)
                InitMenuFlyoutMenuItems(viewModel);   // 浮出選單 (Flyout menu)
            }
        });
        
        // 初始化 XAML 定義的元件
        // Initialize XAML-defined components
        InitializeComponent();
    }

    /// <summary>
    /// 初始化右鍵選單項目
    /// Initialize context menu items
    /// 建立包含剪下、複製、刪除、貼上等常用編輯操作的右鍵選單
    /// Create context menu with common edit operations like cut, copy, delete, paste
    /// </summary>
    /// <param name="viewModel">選單視圖模型 (Menu view model)</param>
    private void InitContextMenuItems(MenuViewModel viewModel)
    {
        var nodes = new List<IMenuItemData>();
        
        // 剪下選項（Ctrl+X）
        // Cut option (Ctrl+X)
        nodes.Add(new MenuItemData()
        {
            Header       = "Cut",
            Icon         = new ScissorOutlined(),
            InputGesture = KeyGesture.Parse("Ctrl+X"),
        });
        
        // 複製選項（Ctrl+C）
        // Copy option (Ctrl+C)
        nodes.Add(new MenuItemData()
        {
            Header       = "Copy",
            Icon         = new CopyOutlined(),
            InputGesture = KeyGesture.Parse("Ctrl+C"),
        });
        
        // 刪除選項（Ctrl+D）
        // Delete option (Ctrl+D)
        nodes.Add(new MenuItemData()
        {
            Header       = "Delete",
            Icon         = new CopyOutlined(),
            InputGesture = KeyGesture.Parse("Ctrl+D"),
        });
        
        // 貼上選項（帶有子選單）
        // Paste option (with submenu)
        nodes.Add(new MenuItemData() {
                Header    = "Paste",
                Children = [
                    new MenuItemData()
                    {
                        Header       = "Paste",
                        Icon         = new FileDoneOutlined(),
                        InputGesture = KeyGesture.Parse("Ctrl+P")
                    },
                    new MenuItemData()
                    {
                        Header       = "Paste from History",
                        InputGesture = KeyGesture.Parse("Ctrl+Shift+V")
                    }
                ]
            }
        );
        
        viewModel.ContextMenuItems = nodes;
    }

    /// <summary>
    /// 初始化下拉選單樹狀節點
    /// Initialize dropdown menu tree nodes
    /// 建立包含檔案、編輯等選項的主選單結構
    /// Create main menu structure with File, Edit and other options
    /// </summary>
    /// <param name="viewModel">選單視圖模型 (Menu view model)</param>
    private void InitMenuTreeNodes(MenuViewModel viewModel)
    {
        var nodes = new List<IMenuItemData>();
        
        // 「檔案」選單項目
        // "File" menu item
        nodes.Add(new MenuItemData()
        {
            Header  = "File",
            Children = [new MenuItemData()
            {
                Header       = "New Text File",
                InputGesture = KeyGesture.Parse("Ctrl+N")
            },
            new MenuItemData()
            {
                Header       = "New File",
                InputGesture = KeyGesture.Parse("Ctrl+Alt+N")
            },
            new MenuItemData()
            {
                Header       = "New Window",
                InputGesture = KeyGesture.Parse("Ctrl+Shift+N")
            }]
        });
        
        // 「編輯」選單項目（含分隔線）
        // "Edit" menu item (with separator)
        nodes.Add(new MenuItemData() {
                Header    = "Edit",
                Children = [
                    new MenuItemData()
                    {
                        Header       = "Undo",
                        InputGesture = KeyGesture.Parse("Ctrl+Shift+Z")
                    },
                    new MenuSeparatorData(),  // 選單分隔線 (Menu separator)
                    new MenuItemData()
                    {
                        Header       = "Cut",
                        InputGesture = KeyGesture.Parse("Ctrl+X")
                    }
                ]
            }
        );
        
        // 停用的選單項目（展示停用狀態）
        // Disabled menu item (demonstrate disabled state)
        nodes.Add(new MenuItemData() {
                Header = "Disabled Item",
                IsEnabled = false
            }
        );
        
        viewModel.MenuItems = nodes;
    }

    /// <summary>
    /// 初始化導覽選單樹狀節點
    /// Initialize navigation menu tree nodes
    /// 建立多層級的側邊導覽選單結構
    /// Create multi-level side navigation menu structure
    /// </summary>
    /// <param name="viewModel">選單視圖模型 (Menu view model)</param>
    private void InitNavMenuTreeNodes(MenuViewModel viewModel)
    {
        // 建立並設定預設選取的選單項目
        // Create and set default selected menu item
        _navMenuDefaultSelectedItem = new NavMenuItemData()
        {
            Header  = "Option 4",
            ItemKey = "Option4",
            Icon = new TwitterOutlined()
        };
        
        var nodes = new List<INavMenuItemData>();
        
        // 第一層選單項目
        // First level menu items
        nodes.Add(new NavMenuItemData()
        {
            Header  = "Navigation One",
            Icon    = new MailOutlined(),
            ItemKey = "1"
        });
        nodes.Add(new NavMenuItemData()
        {
            Header  = "Navigation Two",
            Icon    = new AppstoreOutlined(),
            ItemKey = "2"
        });
        
        // 帶有子選單的選單項目（多層級）
        // Menu item with submenu (multi-level)
        nodes.Add(new NavMenuItemData()
        {
            Header  = "Navigation Three - Submenu",
            Icon    = new SettingOutlined(),
            ItemKey = "3",
            Children = [new NavMenuItemData()
            {
                Header  = "Item 1",
                ItemKey = "SubGroup1",
                Children = [new NavMenuItemData()
                {
                    Header  = "Option 1",
                    ItemKey = "Option1",
                }, new NavMenuItemData()
                {
                    Header  = "Option 2",
                    ItemKey = "Option2",
                }]
            },new NavMenuItemData()
            {
                Header  = "Item 2",
                ItemKey = "SubGroup2",
                Children = [new NavMenuItemData()
                    {
                        Header  = "Option 3",
                        ItemKey = "Option3",
                    }, 
                    _navMenuDefaultSelectedItem
                ]
            }]
        });
        
        nodes.Add(new NavMenuItemData()
        {
            Header  = "Navigation Four",
            ItemKey = "4"
        });
        
        viewModel.NavMenuItems              = nodes;
        // 設定預設選取的項目
        // Set default selected item
        ItemsSourceDemoNavMenu.SelectedItem = _navMenuDefaultSelectedItem;
    }

    /// <summary>
    /// 初始化浮出選單項目
    /// Initialize menu flyout items
    /// 建立浮出式選單的內容結構
    /// Create content structure for flyout menu
    /// </summary>
    /// <param name="viewModel">選單視圖模型 (Menu view model)</param>
    private void InitMenuFlyoutMenuItems(MenuViewModel viewModel)
    {
        var nodes = new List<IMenuItemData>();
        
        // 剪下（Ctrl+X）
        // Cut (Ctrl+X)
        nodes.Add(new MenuItemData()
        {
            Header       = "Cut",
            InputGesture = KeyGesture.Parse("Ctrl+X"),
            Icon = new ScissorOutlined(),
        });
        
        // 複製（Ctrl+C）
        // Copy (Ctrl+C)
        nodes.Add(new MenuItemData() {
                Header       = "Copy",
                InputGesture = KeyGesture.Parse("Ctrl+C"),
                Icon         = new CopyOutlined(),
            }
        );
        
        // 刪除（Ctrl+D）
        // Delete (Ctrl+D)
        nodes.Add(new MenuItemData() {
                Header       = "Delete",
                InputGesture = KeyGesture.Parse("Ctrl+D"),
                Icon         = new DeleteOutlined(),
            }
        );
        
        // 貼上（帶有子選單和分隔線）
        // Paste (with submenu and separator)
        nodes.Add(new MenuItemData() {
                Header    = "Paste",
                Children = [
                    new MenuItemData()
                    {
                        Header       = "Paste",
                        InputGesture = KeyGesture.Parse("Ctrl+P"),
                        Icon         = new FileDoneOutlined(),
                    },
                    new MenuSeparatorData(),
                    new MenuItemData()
                    {
                        Header       = "Paste from History",
                        InputGesture = KeyGesture.Parse("Ctrl+Shift+V"),
                    }
                ]
            }
        );
    
        viewModel.MenuFlyoutItems = nodes;
    }
}