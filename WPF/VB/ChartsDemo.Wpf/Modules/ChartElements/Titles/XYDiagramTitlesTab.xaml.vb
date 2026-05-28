Imports DevExpress.XtraPrinting.Native
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Documents

Namespace ChartsDemo

    Public Partial Class XYDiagramTitlesTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnSelected()
            MyBase.OnSelected()
            MainWindow.CommonRibbon.Init(chart)
        End Sub

        Private Sub Hyperlink_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim source As Hyperlink = TryCast(sender, Hyperlink)
            If source IsNot Nothing Then
                Call ProcessLaunchHelper.StartConfirmed(source.NavigateUri.ToString())
            End If
        End Sub
    End Class
End Namespace
