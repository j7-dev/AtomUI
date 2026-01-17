using AtomUI.Desktop.Controls;
using AtomUI.Theme;
using AtomUI.Theme.Language;
using Avalonia.Controls.ApplicationLifetimes;

namespace AtomUIGallery.Desktop;

/// <summary>
/// AtomUI Gallery 桌面應用程式類別 - 繼承自基礎應用程式類別
/// AtomUI Gallery desktop application class - Inherits from BaseGalleryApplication
/// 負責設定 AtomUI 主題系統和桌面控件
/// Responsible for configuring AtomUI theme system and desktop controls
/// </summary>
public class GalleryApplication : BaseGalleryApplication
{
    /// <summary>
    /// 初始化應用程式並配置 AtomUI 框架
    /// Initialize application and configure AtomUI framework
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        
        // 配置並啟用 AtomUI 框架
        // Configure and enable AtomUI framework
        this.UseAtomUI(builder =>
        {
            // 設定預設語言為簡體中文
            // Set default language variant to Simplified Chinese
            builder.WithDefaultLanguageVariant(LanguageVariant.zh_CN);
            
            // 設定預設主題（亮色主題）
            // Set default theme (light theme)
            builder.WithDefaultTheme(IThemeManager.DEFAULT_THEME_ID);
            
            // 啟用阿里巴巴普惠體字型
            // Enable Alibaba Sans font
            builder.UseAlibabaSansFont();
            
            // 啟用桌面端通用控件
            // Enable desktop common controls
            builder.UseDesktopControls();
            
            // 啟用 Gallery 專用控件
            // Enable gallery-specific controls
            builder.UseGalleryControls();
            
            // 啟用資料表格控件
            // Enable data grid control
            builder.UseDesktopDataGrid();
            
            // 啟用顏色選擇器控件
            // Enable color picker control
            builder.UseDesktopColorPicker();
        });
    }

    /// <summary>
    /// 建構函式 - 設定應用程式名稱
    /// Constructor - Set application name
    /// </summary>
    public GalleryApplication()
    {
        Name = "AtomUI Desktop Gallery";
    }
    
    /// <summary>
    /// 當 Avalonia 框架初始化完成時呼叫
    /// Called when Avalonia framework initialization is completed
    /// 根據不同的應用程式生命週期類型建立對應的視圖
    /// Create corresponding views based on different application lifetime types
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        switch (ApplicationLifetime)
        {
            // 傳統桌面應用程式生命週期
            // Classic desktop application lifetime
            case IClassicDesktopStyleApplicationLifetime desktop:
                // 建立並設定主視窗
                // Create and set main window
                desktop.MainWindow       = CreateWorkspaceWindow();
                desktop.MainWindow.Title = Name;
                break;
            // 單一視圖應用程式生命週期（目前未使用）
            // Single view application lifetime (currently not used)
            // case ISingleViewApplicationLifetime singleView:
            //     singleView.MainView = new MainView();
            //     break;
        }

        base.OnFrameworkInitializationCompleted();
    }
}