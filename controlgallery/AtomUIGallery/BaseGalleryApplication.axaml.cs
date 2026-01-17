using AtomUIGallery.ShowCases;
using AtomUIGallery.Workspace.Views;
using Avalonia;

namespace AtomUIGallery;

/// <summary>
/// 基礎 Gallery 應用程式類別 - 提供所有 Gallery 應用程式的共用功能
/// Base gallery application class - Provides common functionality for all gallery applications
/// 包含工作區視窗建立和服務註冊
/// Includes workspace window creation and service registration
/// </summary>
public partial class BaseGalleryApplication : Application
{
    /// <summary>
    /// 建立主要工作區視窗
    /// Create the main workspace window
    /// </summary>
    /// <returns>工作區視窗實例 (Workspace window instance)</returns>
    protected WorkspaceWindow CreateWorkspaceWindow()
    {
        return new WorkspaceWindow();
    }

    /// <summary>
    /// 註冊應用程式服務
    /// Register application services
    /// 在此方法中註冊所有展示案例控件
    /// Register all showcase controls in this method
    /// </summary>
    public override void RegisterServices()
    {
        base.RegisterServices();
        
        // 註冊所有展示案例控件到系統
        // Register all showcase controls to the system
        ShowCaseRegister.Register();
    }
}