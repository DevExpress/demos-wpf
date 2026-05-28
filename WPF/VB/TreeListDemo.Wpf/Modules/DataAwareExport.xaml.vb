Imports System.Windows
Imports GridDemo

Namespace TreeListDemo

    Public Partial Class DataAwareExport
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.InitRibbon(treeList)
        End Sub

        Private Sub ExportButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ExportToXlsx(view)
        End Sub
    End Class
End Namespace
