Imports System
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Internal
Imports DevExpress.Xpf.DemoCenterBase
Imports DevExpress.Xpf.DemoBase

Namespace ProductsDemo

    Public Partial Class App
        Inherits Application

        Shared Sub New()
            ClearAutomationEventsHelper.IsEnabled = False
        End Sub

        Public Sub New()
            Call ExceptionHelper.Initialize()
            DevExpress.Images.ImagesAssemblyLoader.Load()
            AssemblyResolver.Subscribe()
            ApplicationThemeHelper.ApplicationThemeName = DemoStartupOptions.ActualDefaultTheme.Name
            Call DemoRunner.ShowApplicationSplashScreen()
            Call Theme.RegisterPredefinedPaletteThemes()
        End Sub

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
#If Not NET
            Call ServiceContainer.Default.RegisterService(New ApplicationJumpListService())
#End If
            MyBase.OnStartup(e)
        End Sub
    End Class
End Namespace

Namespace DevExpress.Internal.DemoLauncher

    Public Module DemoLauncherEntryPoint

        Public Function Run() As Tuple(Of Action, System.Threading.Tasks.Task(Of Window))
            Dim done = New Threading.Tasks.TaskCompletionSource(Of Window)()
            Dim lRun As Action = Sub()
                Dim app = New ProductsDemo.App()
                app.InitializeComponent()
                Call GetType(Application).GetField("_startupUri", BindingFlags.Instance Or BindingFlags.NonPublic).SetValue(app, Nothing)
                AddHandler app.Startup, Sub(s, e)
                    Dim window = New ProductsDemo.MainWindow()
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
