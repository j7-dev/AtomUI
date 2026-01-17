using AtomUIGallery.ShowCases.ViewModels;
using AtomUIGallery.ShowCases.Views;
using ReactiveUI;
using Splat;


namespace AtomUIGallery.ShowCases;

/// <summary>
/// 展示案例註冊器 - 負責將所有控件展示案例註冊到 ReactiveUI 的依賴注入容器
/// Showcase register - Responsible for registering all control showcase cases to ReactiveUI's dependency injection container
/// 使用 ReactiveUI 的 Locator 模式將 ViewModel 與 View 進行配對
/// Uses ReactiveUI's Locator pattern to pair ViewModels with Views
/// </summary>
internal static class ShowCaseRegister
{
    /// <summary>
    /// 主要註冊方法 - 註冊所有展示案例
    /// Main registration method - Register all showcase cases
    /// 將展示案例按類型分組註冊：通用、版面、資料顯示、資料輸入、回饋、導覽
    /// Register showcase cases grouped by type: General, Layout, Data Display, Data Entry, Feedback, Navigation
    /// </summary>
    public static void Register()
    {
        RegisterGeneralCases();       // 註冊通用控件 (Register general controls)
        RegisterLayoutCases();        // 註冊版面配置控件 (Register layout controls)
        RegisterDataDisplayCases();   // 註冊資料顯示控件 (Register data display controls)
        RegisterDataEntryCases();     // 註冊資料輸入控件 (Register data entry controls)
        RegisterFeedbackCases();      // 註冊回饋控件 (Register feedback controls)
        RegisterNavigationCases();    // 註冊導覽控件 (Register navigation controls)
    }

    /// <summary>
    /// 註冊通用類別的控件展示案例
    /// Register general category control showcase cases
    /// 包含按鈕、圖示、主題自訂等基礎控件
    /// Includes buttons, icons, theme customization and other basic controls
    /// </summary>
    private static void RegisterGeneralCases()
    {
        // 關於我們頁面 (About us page)
        Locator.CurrentMutable.Register(() => new AboutUsPage(), typeof(IViewFor<AboutUsViewModel>));
        // 按鈕展示 (Button showcase)
        Locator.CurrentMutable.Register(() => new ButtonShowCase(), typeof(IViewFor<ButtonViewModel>));
        // 主題自訂展示 (Theme customization showcase)
        Locator.CurrentMutable.Register(() => new CustomizeThemeShowCase(), typeof(IViewFor<CustomizeThemeViewModel>));
        // 圖示展示 (Icon showcase)
        Locator.CurrentMutable.Register(() => new IconShowCase(), typeof(IViewFor<IconViewModel>));
        // 作業系統資訊頁面 (OS information page)
        Locator.CurrentMutable.Register(() => new OsInfoPage(), typeof(IViewFor<OsInfoViewModel>));
        // 調色盤展示 (Palette showcase)
        Locator.CurrentMutable.Register(() => new PaletteShowCase(), typeof(IViewFor<PaletteViewModel>));
        // 分隔線展示 (Separator showcase)
        Locator.CurrentMutable.Register(() => new SeparatorShowCase(), typeof(IViewFor<SeparatorViewModel>));
        // 分割按鈕展示 (Split button showcase)
        Locator.CurrentMutable.Register(() => new SplitButtonShowCase(), typeof(IViewFor<SplitButtonViewModel>));
    }
    
    /// <summary>
    /// 註冊版面配置類別的控件展示案例
    /// Register layout category control showcase cases
    /// 包含各種容器和版面配置控件
    /// Includes various containers and layout controls
    /// </summary>
    private static void RegisterLayoutCases()
    {
        // 盒子面板展示 (Box panel showcase)
        Locator.CurrentMutable.Register(() => new BoxPanelShowCase(), typeof(IViewFor<BoxPanelViewModel>));
        // 彈性面板展示 (Flex panel showcase)
        Locator.CurrentMutable.Register(() => new FlexPanelShowCase(), typeof(IViewFor<FlexPanelViewModel>));
        // 網格展示 (Grid showcase)
        Locator.CurrentMutable.Register(() => new GridShowCase(), typeof(IViewFor<GridViewModel>));
        // 分割器展示 (Splitter showcase)
        Locator.CurrentMutable.Register(() => new SplitterShowCase(), typeof(IViewFor<SplitterViewModel>));
    }
    
    /// <summary>
    /// 註冊資料顯示類別的控件展示案例
    /// Register data display category control showcase cases
    /// 包含表格、卡片、標籤等資料呈現控件
    /// Includes tables, cards, tags and other data presentation controls
    /// </summary>
    private static void RegisterDataDisplayCases()
    {
        // 頭像展示 (Avatar showcase)
        Locator.CurrentMutable.Register(() => new AvatarShowCase(), typeof(IViewFor<AvatarViewModel>));
        // 徽章展示 (Badge showcase)
        Locator.CurrentMutable.Register(() => new BadgeShowCase(), typeof(IViewFor<BadgeViewModel>));
        // 行事曆展示 (Calendar showcase)
        Locator.CurrentMutable.Register(() => new CalendarShowCase(), typeof(IViewFor<CalendarViewModel>));
        // 摺疊面板展示 (Collapse showcase)
        Locator.CurrentMutable.Register(() => new CollapseShowCase(), typeof(IViewFor<CollapseViewModel>));
        // 卡片展示 (Card showcase)
        Locator.CurrentMutable.Register(() => new CardShowCase(), typeof(IViewFor<CardViewModel>));
        // 輪播圖展示 (Carousel showcase)
        Locator.CurrentMutable.Register(() => new CarouselShowCase(), typeof(IViewFor<CarouselViewModel>));
        // 資料表格展示 (Data grid showcase)
        Locator.CurrentMutable.Register(() => new DataGridShowCase(), typeof(IViewFor<DataGridViewModel>));
        // 描述清單展示 (Descriptions showcase)
        Locator.CurrentMutable.Register(() => new DescriptionsShowCase(), typeof(IViewFor<DescriptionsViewModel>));
        // 空狀態展示 (Empty showcase)
        Locator.CurrentMutable.Register(() => new EmptyShowCase(), typeof(IViewFor<EmptyViewModel>));
        // 圖片預覽器展示 (Image previewer showcase)
        Locator.CurrentMutable.Register(() => new ImagePreviewerShowCase(), typeof(IViewFor<ImagePreviewerViewModel>));
        // 展開器展示 (Expander showcase)
        Locator.CurrentMutable.Register(() => new ExpanderShowCase(), typeof(IViewFor<ExpanderViewModel>));
        // 群組框展示 (Group box showcase)
        Locator.CurrentMutable.Register(() => new GroupBoxShowCase(), typeof(IViewFor<GroupBoxViewModel>));
        // 資訊浮出展示 (Info flyout showcase)
        Locator.CurrentMutable.Register(() => new InfoFlyoutShowCase(), typeof(IViewFor<InfoFlyoutViewModel>));
        // 清單展示 (List showcase)
        Locator.CurrentMutable.Register(() => new ListShowCase(), typeof(IViewFor<ListViewModel>));
        // QR 碼展示 (QR code showcase)
        Locator.CurrentMutable.Register(() => new QRCodeShowCase(), typeof(IViewFor<QRCodeViewModel>));
        // 分段控制展示 (Segmented showcase)
        Locator.CurrentMutable.Register(() => new SegmentedShowCase(), typeof(IViewFor<SegmentedViewModel>));
        // 統計數值展示 (Statistic showcase)
        Locator.CurrentMutable.Register(() => new StatisticShowCase(), typeof(IViewFor<StatisticViewModel>));
        // 標籤展示 (Tag showcase)
        Locator.CurrentMutable.Register(() => new TagShowCase(), typeof(IViewFor<TagViewModel>));
        // 時間軸展示 (Timeline showcase)
        Locator.CurrentMutable.Register(() => new TimelineShowCase(), typeof(IViewFor<TimelineViewModel>));
        // 工具提示展示 (Tooltip showcase)
        Locator.CurrentMutable.Register(() => new TooltipShowCase(), typeof(IViewFor<TooltipViewModel>));
        // 樹狀檢視展示 (Tree view showcase)
        Locator.CurrentMutable.Register(() => new TreeViewShowCase(), typeof(IViewFor<TreeViewViewModel>));
    }
    
    /// <summary>
    /// 註冊資料輸入類別的控件展示案例
    /// Register data entry category control showcase cases
    /// 包含文字框、選擇器、滑桿等使用者輸入控件
    /// Includes text boxes, selectors, sliders and other user input controls
    /// </summary>
    private static void RegisterDataEntryCases()
    {
        // 核取方塊展示 (Checkbox showcase)
        Locator.CurrentMutable.Register(() => new CheckBoxShowCase(), typeof(IViewFor<CheckBoxViewModel>));
        // 顏色選擇器展示 (Color picker showcase)
        Locator.CurrentMutable.Register(() => new ColorPickerShowCase(), typeof(IViewFor<ColorPickerViewModel>));
        // 日期選擇器展示 (Date picker showcase)
        Locator.CurrentMutable.Register(() => new DatePickerShowCase(), typeof(IViewFor<DatePickerViewModel>));
        // 文字輸入框展示 (Line edit showcase)
        Locator.CurrentMutable.Register(() => new LineEditShowCase(), typeof(IViewFor<LineEditViewModel>));
        // 數字上下選擇器展示 (Number up-down showcase)
        Locator.CurrentMutable.Register(() => new NumberUpDownShowCase(), typeof(IViewFor<NumberUpDownViewModel>));
        // 選項按鈕展示 (Radio button showcase)
        Locator.CurrentMutable.Register(() => new RadioButtonShowCase(), typeof(IViewFor<RadioButtonViewModel>));
        // 評分展示 (Rate showcase)
        Locator.CurrentMutable.Register(() => new RateShowCase(), typeof(IViewFor<RateViewModel>));
        // 選擇器展示 (Select showcase)
        Locator.CurrentMutable.Register(() => new SelectShowCase(), typeof(IViewFor<SelectViewModel>));
        // 滑桿展示 (Slider showcase)
        Locator.CurrentMutable.Register(() => new SliderShowCase(), typeof(IViewFor<SliderViewModel>));
        // 時間選擇器展示 (Time picker showcase)
        Locator.CurrentMutable.Register(() => new TimePickerShowCase(), typeof(IViewFor<TimePickerViewModel>));
        // 切換開關展示 (Toggle switch showcase)
        Locator.CurrentMutable.Register(() => new ToggleSwitchShowCase(), typeof(IViewFor<ToggleSwitchViewModel>));
        // 上傳檔案展示 (Upload showcase)
        Locator.CurrentMutable.Register(() => new UploadShowCase(), typeof(IViewFor<UploadViewModel>));
    }
    
    /// <summary>
    /// 註冊回饋類別的控件展示案例
    /// Register feedback category control showcase cases
    /// 包含警告、訊息、通知等使用者回饋控件
    /// Includes alerts, messages, notifications and other user feedback controls
    /// </summary>
    private static void RegisterFeedbackCases()
    {
        // 警告提示展示 (Alert showcase)
        Locator.CurrentMutable.Register(() => new AlertShowCase(), typeof(IViewFor<AlertViewModel>));
        // 抽屜展示 (Drawer showcase)
        Locator.CurrentMutable.Register(() => new DrawerShowCase(), typeof(IViewFor<DrawerViewModel>));
        // 載入中旋轉展示 (Spin showcase)
        Locator.CurrentMutable.Register(() => new SpinShowCase(), typeof(IViewFor<SpinViewModel>));
        // 全域訊息展示 (Message showcase)
        Locator.CurrentMutable.Register(() => new MessageShowCase(), typeof(IViewFor<MessageViewModel>));
        // 模態對話框展示 (Modal showcase)
        Locator.CurrentMutable.Register(() => new ModalShowCase(), typeof(IViewFor<ModalViewModel>));
        // 通知提醒展示 (Notification showcase)
        Locator.CurrentMutable.Register(() => new NotificationShowCase(), typeof(IViewFor<NotificationViewModel>));
        // 氣泡確認框展示 (Popup confirm showcase)
        Locator.CurrentMutable.Register(() => new PopupConfirmShowCase(), typeof(IViewFor<PopupConfirmViewModel>));
        // 進度條展示 (Progress bar showcase)
        Locator.CurrentMutable.Register(() => new ProgressBarShowCase(), typeof(IViewFor<ProgressBarViewModel>));
        // 結果頁面展示 (Result showcase)
        Locator.CurrentMutable.Register(() => new ResultShowCase(), typeof(IViewFor<ResultViewModel>));
        // 骨架屏展示 (Skeleton showcase)
        Locator.CurrentMutable.Register(() => new SkeletonShowCase(), typeof(IViewFor<SkeletonViewModel>));
        // 浮水印展示 (Watermark showcase)
        Locator.CurrentMutable.Register(() => new WatermarkShowCase(), typeof(IViewFor<WatermarkViewModel>));
    }
    
    /// <summary>
    /// 註冊導覽類別的控件展示案例
    /// Register navigation category control showcase cases
    /// 包含選單、分頁、步驟條等導覽控件
    /// Includes menus, pagination, steps and other navigation controls
    /// </summary>
    private static void RegisterNavigationCases()
    {
        // 麵包屑導覽展示 (Breadcrumb showcase)
        Locator.CurrentMutable.Register(() => new BreadcrumbShowCase(), typeof(IViewFor<BreadcrumbViewModel>));
        // 按鈕旋轉器展示 (Button spinner showcase)
        Locator.CurrentMutable.Register(() => new ButtonSpinnerShowCase(), typeof(IViewFor<ButtonSpinnerViewModel>));
        // 下拉式選單展示 (ComboBox showcase)
        Locator.CurrentMutable.Register(() => new ComboBoxShowCase(), typeof(IViewFor<ComboBoxViewModel>));
        // 下拉式按鈕展示 (Dropdown button showcase)
        Locator.CurrentMutable.Register(() => new DropdownButtonShowCase(), typeof(IViewFor<DropdownButtonViewModel>));
        // 選單展示 (Menu showcase)
        Locator.CurrentMutable.Register(() => new MenuShowCase(), typeof(IViewFor<MenuViewModel>));
        // 分頁展示 (Pagination showcase)
        Locator.CurrentMutable.Register(() => new PaginationShowCase(), typeof(IViewFor<PaginationViewModel>));
        // 步驟條展示 (Steps showcase)
        Locator.CurrentMutable.Register(() => new StepsShowCase(), typeof(IViewFor<StepsViewModel>));
        // 標籤控制項展示 (Tab control showcase)
        Locator.CurrentMutable.Register(() => new TabControlShowCase(), typeof(IViewFor<TabControlViewModel>));
    }
}
