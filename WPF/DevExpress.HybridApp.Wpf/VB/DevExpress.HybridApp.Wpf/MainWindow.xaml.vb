Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.WindowsUI

Namespace DevExpress.DevAV

    Public Partial Class MainWindow
        Inherits ThemedWindow

        Public Sub New()
            App.Test(Me)
            Utils.About.UAlgo.Default.DoEventObject(Utils.About.UAlgo.kDemo, Utils.About.UAlgo.pWPF, Me)
            Me.InitializeComponent()
            If Height > SystemParameters.VirtualScreenHeight OrElse Width > SystemParameters.VirtualScreenWidth Then WindowState = WindowState.Maximized
            Call EventManager.RegisterClassHandler(GetType(AppBarButton), Controls.Primitives.ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf OnAppBarButtonClick), True)
        End Sub

        Private Sub OnAppBarButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim barButton = CType(sender, AppBarButton)
            Dim content = If(barButton.Label, barButton.Name)
            If content IsNot Nothing Then
                Call Log(String.Format("HybridApp: BarButtonClick: {0}", content.ToString()))
            End If
        End Sub
    End Class
End Namespace
