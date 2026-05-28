Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Windows
Imports DevExpress.Data
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace WindowsUIDemo

    <CodeFile("ViewModels/NotificationsViewModel.(cs)")>
    Public Partial Class Notifications
        Inherits WindowsUIDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Shared ReadOnly Property ApplicationID As String
            Get
                Return String.Format("Components_{0}_Demo_Center_{0}", AssemblyInfo.VersionShort.Replace(".", "_"))
            End Get
        End Property

        Public Shared ReadOnly Property ApplicationName As String
            Get
                Return "Notifications"
            End Get
        End Property

        Private ReadOnly Property ViewModel As NotificationsViewModel
            Get
                Return CType(DataContext, NotificationsViewModel)
            End Get
        End Property

        Private Property IsShortcutCreated As Boolean

        Private Sub OnModuleLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ActivatorLogger = ViewModel.LogService
            TryCreateApplicationShortcut()
        End Sub

        Private Sub OnModuleUnloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ActivatorLogger = Nothing
            TryRemoveApplicationShortcut()
        End Sub

        Private Sub TryCreateApplicationShortcut()
            Try
                IsShortcutCreated = True
                ShellHelper.TryCreateShortcut(ApplicationID, ApplicationName, Nothing, GetType(UIDemoNotificationActivator))
            Catch
                ActivatorLogger.LogLine("Cannot create application shortcut. Native" & Microsoft.VisualBasic.vbCrLf & "notifications are not available.")
                IsShortcutCreated = False
                useWin8Notifications.IsChecked = False
                useWin8Notifications.IsEnabled = False
            End Try
        End Sub

        Private Sub TryRemoveApplicationShortcut()
            If IsShortcutCreated Then ShellHelper.TryRemoveShortcut(ApplicationName)
        End Sub

        Private Shared ActivatorLogger As ILogService

        Public Shared Sub SendActivatorMessage(ByVal arguments As String)
            If ActivatorLogger IsNot Nothing Then
                ActivatorLogger.LogLine("Activator invoked! Notification id = " & arguments)
            End If
        End Sub
    End Class

    <Guid("45FD942D-1AD5-48E7-B139-4E1FB9F1F060"), ComVisible(True)>
    Public Class UIDemoNotificationActivator
        Inherits ToastNotificationActivator

        Public Overrides Sub OnActivate(ByVal arguments As String, ByVal data As Dictionary(Of String, String))
            Application.Current.Dispatcher.Invoke(Sub() Notifications.SendActivatorMessage(arguments))
        End Sub
    End Class
End Namespace
