using Avalonia;
using Avalonia.Media;
using ReactiveUI.Avalonia;

namespace AtomUIGallery.Desktop;

/// <summary>
/// 程式入口類別 - 負責啟動 AtomUI Gallery 桌面應用程式
/// Program entry class - Responsible for launching the AtomUI Gallery desktop application
/// </summary>
internal class Program
{
    /// <summary>
    /// 應用程式主要進入點
    /// Main entry point of the application
    /// </summary>
    /// <param name="args">命令列參數 (Command line arguments)</param>
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            // 建立並啟動 Avalonia 應用程式
            // Build and start the Avalonia application
            BuildAvaloniaApp()
                // 配置字型備援選項，使用微軟雅黑作為備用字型
                // Configure font fallback options, using Microsoft YaHei as fallback font
                .With(new FontManagerOptions
                {
                    FontFallbacks = [new FontFallback
                    {
                        FontFamily = new FontFamily("Microsoft YaHei")
                    }]
                })
                // 以傳統桌面應用程式生命週期啟動
                // Start with classic desktop application lifetime
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            // 捕獲並記錄未處理的例外
            // Catch and log unhandled exceptions
            LogException(ex);
            throw;
        }
    }

    /// <summary>
    /// 記錄例外資訊到日誌檔案
    /// Log exception information to a log file
    /// </summary>
    /// <param name="ex">要記錄的例外物件 (Exception object to log)</param>
    private static void LogException(Exception ex)
    {
        // 取得應用程式資料目錄
        // Get application data directory
        var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
        // 建立日誌目錄路徑
        // Create log directory path
        var logDirectory = Path.Combine(homeDirectory, Path.Combine("AtomUIGallery", "AppCrashLogs"));
        Directory.CreateDirectory(logDirectory);
            
        // 產生帶有時間戳記的日誌檔案名稱
        // Generate log file name with timestamp
        var logFileName = $"CrashLog_{DateTime.Now:yyyyMMdd_HHmmss}.log";
        var logFilePath = Path.Combine(logDirectory, logFileName);
            
        // 將例外詳細資訊寫入日誌檔案
        // Write exception details to log file
        File.WriteAllText(logFilePath, 
            $"CrashTime: {DateTime.Now}\r\n" +
            $"Exception Type: {ex.GetType().Name}\r\n" +
            $"Exception Message: {ex.Message}\r\n" +
            $"Stack Info: \r\n{ex.StackTrace}");
    }

    /// <summary>
    /// 建立並配置 Avalonia 應用程式建構器
    /// Build and configure the Avalonia application builder
    /// </summary>
    /// <returns>配置完成的 AppBuilder (Configured AppBuilder)</returns>
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<GalleryApplication>()
                         // 使用 ReactiveUI 框架來支援 MVVM 模式
                         // Use ReactiveUI framework to support MVVM pattern
                         .UseReactiveUI()
                         // 自動偵測並配置目前執行的平台
                         // Automatically detect and configure the current platform
                         .UsePlatformDetect()
                         // .WithAlibabaSansFont()  // 可選：使用阿里巴巴普惠體字型
                         // 配置 Windows 32 位元平台選項
                         // Configure Win32 platform options
                         .With(new Win32PlatformOptions())
                         // 啟用追蹤日誌記錄
                         // Enable trace logging
                         .LogToTrace();
        
    }
}