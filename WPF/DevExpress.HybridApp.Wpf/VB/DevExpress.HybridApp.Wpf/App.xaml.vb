Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Threading
Imports System.Windows
Imports System.Windows.Media.Animation
Imports DevExpress.Data.Internal
Imports DevExpress.Data.Utils
Imports DevExpress.Images
Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoCenterBase
Imports DevExpress.Xpf.Editors

Namespace DevExpress.DevAV

    Public Partial Class App
        Inherits Application

        Private Shared singleInstanceApplicationGuard As IDisposable

        Shared Sub New()
            CompatibilitySettings.UseLegacyColumnFilterPopup = True
            ClearAutomationEventsHelper.IsEnabled = False
        End Sub

        Public Sub New()
            ApplicationThemeHelper.ApplicationThemeName = Theme.HybridApp.Name
        End Sub

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Call ExceptionHelper.Initialize()
            ClearAutomationEventsHelper.IsEnabled = False
            DevAVDataDirectoryHelper.LocalPrefix = "WpfHybridApp"
#If Not NET
            Call ServiceContainer.Default.RegisterService(New ApplicationJumpListService())
#End If
            Call ImagesAssemblyLoader.Load()
            Call Timeline.DesiredFrameRateProperty.OverrideMetadata(GetType(Timeline), New FrameworkPropertyMetadata(200))
            MyBase.OnStartup(e)
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            Dim [exit] As Boolean
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfHybridApp", [exit])
            If [exit] Then
                Shutdown()
                Return
            End If

            Call SetCultureInfo()
            Call SetLocalization()
            Call DemoRunner.ShowApplicationSplashScreen()
        End Sub

        Private Shared Sub SetCultureInfo()
            Dim demoCI As CultureInfo = CType(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
            demoCI.NumberFormat.CurrencySymbol = "$"
            demoCI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentCulture = demoCI
            Dim demoUI As CultureInfo = CType(Thread.CurrentThread.CurrentUICulture.Clone(), CultureInfo)
            demoUI.NumberFormat.CurrencySymbol = "$"
            demoUI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentUICulture = demoUI
        End Sub

        Private Shared Sub SetLocalization()
            EditorLocalizer.Active = New HybridAppEditorLocalizer()
        End Sub

#Region "DemoTester"
        Public Shared Sub Test(ByVal window As ThemedWindow)
            Dim testerPath As String = Nothing
            Dim allowDemoTest As Boolean = False
#If NET
                "NETCoreDesktop";
#Else
            Dim frameworkPrefix = "Framework4"
#End If
            testerPath = String.Format("c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort)
            Dim args = SafeEnvironment.GetCommandLineArgs()
            allowDemoTest = args.Length > 0 AndAlso args.Contains("/testallexcept:")
            If File.Exists(testerPath) AndAlso allowDemoTest Then
                ExceptionHelper.IsEnabled = False
                Dim assembly = SafeTypeResolver.GetOrLoadAssemblyFrom(testerPath)
                Dim type = SafeTypeResolver.GetKnownType(assembly, "DevExpress.Reallife.Tester.HybridAppDemoTester")
                type.GetMethod("Create")?.Invoke(Nothing, New Object() {window, True})
            End If
        End Sub
#End Region
    End Class

    Public Class HybridAppEditorLocalizer
        Inherits EditorLocalizer

        Protected Overrides Sub PopulateStringTable()
            MyBase.PopulateStringTable()
            AddString(EditorStringId.LookUpSearch, "Enter text to search...")
        End Sub
    End Class
End Namespace

Namespace DevExpress.Internal.DemoLauncher

    Public Module DemoLauncherEntryPoint

        Public Function Run() As Tuple(Of Action, Tasks.Task(Of Window))
            Dim done = New Tasks.TaskCompletionSource(Of Window)()
            Dim lRun As Action = Sub()
                DemoRunner.BypassSealedCheck = True
                Dim app = New DevAV.App()
                DemoRunner.BypassSealedCheck = False
                app.InitializeComponent()
                Call GetType(Application).GetField("_startupUri", BindingFlags.Instance Or BindingFlags.NonPublic).SetValue(app, Nothing)
                AddHandler app.Startup, Sub(s, e)
                    Dim window = New DevAV.MainWindow()
                    window.Show()
                    app.MainWindow = window
                    done.SetResult(window)
                End Sub
                app.Run()
            End Sub
            Return Tuple.Create(lRun, done.Task)
        End Function
    End Module
End Namespace
