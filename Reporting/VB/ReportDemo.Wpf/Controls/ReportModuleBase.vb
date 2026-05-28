Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Threading
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Utils.About
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DocumentViewer

Namespace ReportWpfDemo

    Public Class ReportModuleBase
        Inherits DemoModule

        Shared Sub New()
            Call PatchDemoReportsConnectionStrings()
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.ConvertBindingsToExpressions = DevExpress.XtraReports.UI.PromptBoolean.False
            DevExpress.Xpf.Reports.UserDesigner.Extensions.ReportDesignerRibbonDemoButtonHelper.Load()
            Dim dispatcher = Threading.Dispatcher.CurrentDispatcher
            If dispatcher IsNot Nothing Then
                AddHandler dispatcher.UnhandledException, AddressOf Dispatcher_UnhandledException
            End If
        End Sub

        Private Shared Sub PatchDemoReportsConnectionStrings()
            Dim path As String = DataFilesHelper.FindFile("nwind.db", DataFilesHelper.DataPath)
            Dim directory As String = IO.Path.GetDirectoryName(path)
            XtraReportsDemos.ConnectionHelper.SetDataDirectory(directory)
        End Sub

        Private Shared Sub Dispatcher_UnhandledException(ByVal sender As Object, ByVal e As DispatcherUnhandledExceptionEventArgs)
            e.Handled = True
            If Not System.ComponentModel.DesignerProperties.GetIsInDesignMode(New DependencyObject()) Then
                Dim exception = e.Exception.GetBaseException()
                Call MessageBox.Show(exception.ToString(), "Demo Application Error", MessageBoxButton.OK, MessageBoxImage.Warning)
                UAlgo.Default.DoEventException(exception)
            End If
        End Sub

        Protected ReadOnly Property ReportModuleViewModel As ReportModuleViewModelBase
            Get
                Return TryCast(DataContext, ReportModuleViewModelBase)
            End Get
        End Property

        Public Sub New()
            Call Interaction.GetBehaviors(Me).Add(CreateWindowService())
            Call Interaction.GetBehaviors(Me).Add(CreateDispatchFocusBehavior())
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler Loaded, AddressOf OnLoaded
            If System.ComponentModel.DesignerProperties.GetIsInDesignMode(Me) Then Return
        'ReportModuleViewModel.InitializeReport();
        End Sub

        Protected Overrides Sub Hide()
            MyBase.Hide()
            Dim viewModel = ReportModuleViewModel
            If viewModel IsNot Nothing AndAlso viewModel.Report IsNot Nothing Then
                viewModel.Report.StopPageBuilding()
            End If
        End Sub

        Private Shared Function CreateWindowService() As WindowService
            Dim style = New Style(GetType(ThemedWindow))
            style.Setters.Add(New Setter(Window.WindowStateProperty, WindowState.Maximized))
            style.Setters.Add(New Setter(ThemedWindow.ShowIconProperty, False))
            style.Setters.Add(New Setter(Window.TitleProperty, "Report Designer"))
            style.Setters.Add(New Setter(ThemedWindow.UseLayoutRoundingProperty, True))
            style.Setters.Add(New Setter(Window.ShowInTaskbarProperty, False))
            Return New WindowService With {.WindowShowMode = WindowShowMode.Dialog, .WindowType = GetType(ThemedWindow), .WindowStyle = style}
        End Function

        Private Shared Function CreateDispatchFocusBehavior() As DispatchFocusBehavior
            Dim behavior = New DispatchFocusBehavior()
            Call BindingOperations.SetBinding(behavior, DispatchFocusBehavior.ElementProperty, New Binding() With {.ElementName = "viewer"})
            Return behavior
        End Function
    End Class
End Namespace
