Imports System.Windows.Controls

Namespace PivotGridDemo

    Public Partial Class ExcelStyleFiltering
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportPivotGrid = pivot
            pivot.CollapseAllColumns()
        End Sub
    End Class
End Namespace
