# GitHub Copilot 指令

## 優先指導原則

當為此儲存庫生成代碼時：

1. **版本相容性**：始終檢測並遵守此專案中使用的語言、框架和函式庫的確切版本
2. **上下文檔案**：優先考慮 .github/copilot 目錄中定義的模式和標準
3. **代碼庫模式**：當上下文檔案未提供具體指導時，掃描代碼庫以查找已建立的模式
4. **架構一致性**：維持分層架構風格和已建立的邊界
5. **代碼品質**：在所有生成的代碼中優先考慮可維護性、效能、安全性和可測試性

## 技術版本檢測

在生成代碼之前，掃描代碼庫以識別：

### 1. 語言版本
- **.NET SDK**: 8.0.300 (來自 global.json)
- **C# 語言版本**: latest (來自 Directory.Build.props)
- **框架目標**: .NET 8
- 啟用 Nullable 引用類型
- 啟用隱式 using
- 絕不使用超出檢測到的版本的語言功能

### 2. 框架版本
- **Avalonia UI**: 11.3.10 (來自 build/Version.props)
- **AtomUI**: 5.1.4-build.3
- **ReactiveUI.Avalonia**: 11.3.8
- **Svg.Controls.Avalonia**: 11.3.6.2
- **Avalonia.Controls.TreeDataGrid**: 11.1.1
- **System.Reactive**: 6.1.0
- 生成與這些特定版本相容的代碼
- 絕不建議檢測到的框架版本中不可用的功能

### 3. 測試框架
- **xUnit**: 2.8.0
- **NSubstitute**: 5.1.0 (用於模擬)
- **Shouldly**: 4.2.1 (用於斷言)
- **Microsoft.NET.Test.Sdk**: 17.10.0-release-24177-07

### 4. 其他關鍵函式庫
- **SkiaSharp.QrCode**: 0.7.0
- **CommunityToolkit.Mvvm**: 8.1.0
- **Microsoft.CodeAnalysis.CSharp**: 4.10.0-3.final (用於源生成器)

## 專案結構和組織

### 專案架構
```
AtomUI/
├── src/                          # 源代碼
│   ├── AtomUI.Core/             # 核心功能和動畫
│   ├── AtomUI.Controls.Shared/   # 共享控制項邏輯
│   ├── AtomUI.Desktop.Controls/ # 桌面控制項實作
│   ├── AtomUI.Icons.AntDesign/  # Ant Design 圖標
│   └── AtomUI.Generator/        # 源生成器
├── tests/                        # 測試專案
│   ├── AtomUI.TestBase/         # 測試基礎設施
│   └── AtomUI.Base.Tests/       # 單元測試
├── controlgallery/               # 範例和展示
│   └── AtomUIGallery/           # 控制項展示應用
└── build/                        # 構建配置
```

### 命名約定
- **命名空間**: 使用 `AtomUI` 作為根命名空間，遵循專案結構
- **類名**: PascalCase (例如：`Drawer`, `BaseTransitionUtils`)
- **方法名**: PascalCase (例如：`CreateTransition`, `GetAvaloniaVersion`)
- **屬性名**: PascalCase (例如：`Content`, `IsOpen`, `Placement`)
- **欄位名**: 私有欄位使用 camelCase，公共欄位使用 PascalCase
- **常量**: PascalCase (例如：`ContentProperty`)
- **區域屬性定義**: 使用中文註釋 `#region 公共属性定义` / `#endregion`

## 代碼品質標準

### 可維護性
- 編寫具有清晰命名的自我說明代碼
- 遵循代碼庫中顯而易見的命名和組織約定
- 保持函數專注於單一職責
- 限制函數複雜度和長度以匹配現有模式

### 效能
- 遵循現有的記憶體和資源管理模式
- 匹配處理計算密集型操作的現有模式
- 遵循非同步操作的既定模式
- 一致地應用與現有模式相符的快取

### 安全性
- 遵循輸入驗證的現有模式
- 應用代碼庫中使用的相同清理技術
- 使用與現有模式匹配的參數化查詢
- 遵循已建立的驗證和授權模式

### 可測試性
- 遵循可測試代碼的既定模式
- 匹配代碼庫中使用的依賴注入方法
- 應用管理依賴項的相同模式
- 遵循既定的模擬和測試替身模式

## .NET/Avalonia 指導原則

### C# 代碼風格
- 使用 `latest` C# 語言版本功能
- 啟用可空引用類型 (`Nullable enable`)
- 使用隱式 using 語句 (`ImplicitUsings enable`)
- 優先使用表達式主體成員（當適合時）
- 使用模式匹配和現代 C# 語法
- 使用 `var` 進行局部變數聲明（當類型明顯時）

### Avalonia 特定模式

#### 控制項定義
```csharp
// 遵循此模式定義 Avalonia 控制項
public class MyControl : Control
{
    #region 公共属性定义
    
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<MyControl, string>(nameof(Title));
    
    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    #endregion
}
```

#### 使用 StyledProperty
- 使用 `StyledProperty<T>` 定義可樣式化的屬性
- 使用 `AvaloniaProperty.Register<TOwner, TValue>` 註冊屬性
- 為雙向綁定適當使用 `BindingMode.TwoWay`
- 使用 `[Content]` 和 `[DependsOn]` 特性（當適用時）

#### 依賴注入和反應式
- 使用 Avalonia 的內建 DI 容器
- 遵循 ReactiveUI 模式進行 MVVM
- 使用 `IObservable<T>` 和 `System.Reactive` 進行反應式程式設計
- 使用 `IDisposable` 和 `CompositeDisposable` 進行資源管理

### LINQ 使用模式
- 使用方法語法而非查詢語法
- 優先使用 LINQ 進行集合操作
- 示例：`items.FirstOrDefault(x => x.Key == key)`

### 非同步/等待模式
- 使用 `async`/`await` 進行非同步操作
- 非同步方法名稱以 `Async` 結尾
- 始終在可能時返回 `Task` 或 `Task<T>`
- 使用 `ConfigureAwait(false)`（當在函式庫代碼中時）

### 錯誤處理
- 使用特定的例外類型
- 創建自訂例外類別（在 `Exceptions` 命名空間中）
- 提供有意義的錯誤訊息
- 記錄例外和關鍵錯誤

### 集合類型
- 優先使用 `IList<T>`、`IEnumerable<T>` 作為參數
- 使用 `List<T>` 進行實作
- 使用 `IReadOnlyList<T>` 作為只讀返回類型

## 測試方法

### 單元測試結構
```csharp
public class MyClassTests : IClassFixture<MyFixture>
{
    private MyFixture _fixture;
    private ITestOutputHelper _output;
    
    public MyClassTests(MyFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [Fact]
    public void TestMethodName()
    {
        // Arrange
        var expected = "value";
        
        // Act
        var actual = MethodUnderTest();
        
        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void TestWithData(string input)
    {
        // Test implementation
    }
    
    public static TheoryData<string> TestData()
    {
        var data = new TheoryData<string>();
        data.Add("value1");
        data.Add("value2");
        return data;
    }
}
```

### 測試命名約定
- 測試類名：`[ClassName]Tests`
- 測試方法名：`Test[MethodName]` 或描述性名稱
- Fixture 類名：`[ClassName]Fixture`

### 斷言風格
- 使用 Shouldly 進行流暢的斷言
- 示例：`result.ShouldBeEquivalentTo(expected)`
- 示例：`value.ShouldBe(10)`
- 示例：`list.Count.ShouldBeEquivalentTo(5)`

### 模擬
- 使用 NSubstitute 進行模擬
- 創建替代品：`var mock = Substitute.For<IInterface>()`
- 設置返回值：`mock.Method().Returns(value)`
- 驗證調用：`mock.Received().Method()`

## 文檔要求

### 文檔風格
- 遵循代碼庫中找到的確切文檔格式
- 對公共 API 使用 XML 文檔註釋
- 記錄參數、返回值和例外（採用相同風格）
- 匹配類級文檔風格和內容

### XML 文檔註釋示例
```csharp
/// <summary>
/// 表示一個抽屜控制項，可從螢幕邊緣滑入。
/// </summary>
public class Drawer : Control
{
    /// <summary>
    /// 取得或設定抽屜的內容。
    /// </summary>
    public object? Content { get; set; }
}
```

## 版本控制指導原則

### 語義版本控制
- 遵循語義版本控制 (MAJOR.MINOR.PATCH-BUILD)
- 當前版本：5.1.4-build.3
- 匹配記錄重大變更的現有模式
- 遵循棄用通知的相同方法

### 提交訊息
- 使用清晰、描述性的提交訊息
- 遵循專案的現有提交訊息風格
- 在適用時引用問題編號

## Avalonia XAML 模式

### XAML 檔案組織
- 使用 `.axaml` 副檔名作為 Avalonia XAML
- 遵循代碼後置檔案的命名約定（`.axaml.cs`）
- 使用適當的命名空間聲明

### 樣式和主題
- 遵循 Ant Design 設計語言
- 使用 AtomUI 的主題系統進行自訂
- 匹配現有控制項的樣式模式
- 使用設計令牌進行一致的樣式

## 一般最佳實踐

- 遵循命名約定，完全如它們在現有代碼中出現的那樣
- 匹配來自類似檔案的代碼組織模式
- 應用與現有模式一致的錯誤處理
- 遵循與代碼庫中看到的相同的測試方法
- 匹配現有代碼的日誌模式
- 使用與代碼庫中看到的相同的配置方法

## 專案特定指導

### 設計原則
- 實作 Ant Design 的設計語言和互動模式
- 提供開箱即用的高品質 Avalonia 元件
- 支援跨平台一致性（Windows、macOS、Linux）
- 基於 Avalonia 的樣式系統實作主題自訂

### 架構邊界
- **AtomUI.Core**: 核心功能、動畫、主題系統
- **AtomUI.Controls.Shared**: 跨平台共享控制項邏輯
- **AtomUI.Desktop.Controls**: 桌面特定控制項實作
- **AtomUI.Icons.AntDesign**: Ant Design 圖標系統
- 尊重這些邊界，不要在不適當的專案中放置代碼

### 構建和測試
- 使用 `dotnet build` 構建專案
- 使用 `dotnet test` 運行測試
- 遵循中央包管理（`Directory.Packages.props`）
- 尊重 `Directory.Build.props` 中的全域屬性

### 重要注意事項
- 在生成任何代碼之前徹底掃描代碼庫
- 毫無例外地尊重現有的架構邊界
- 匹配周圍代碼的風格和模式
- 如有疑問，優先考慮與現有代碼的一致性，而不是外部最佳實踐
- 專案支持中文（簡體和繁體）和英文文檔

## 參考檔案

在代碼庫中尋找這些關鍵檔案以獲得額外的上下文：
- `global.json` - SDK 版本
- `Directory.Build.props` - 全域專案屬性
- `Directory.Packages.props` - 包版本管理
- `build/Version.props` - 版本資訊
- 現有控制項實作（在 `src/AtomUI.Desktop.Controls/` 中）
- 測試示例（在 `tests/` 中）
- 控制項展示範例（在 `controlgallery/AtomUIGallery/` 中）
