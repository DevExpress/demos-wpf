Imports DevExpress.Xpf.Grid.Automation

Namespace GridDemo

    Public Partial Class Sparklines
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
        End Sub

        Private Sub TableView_AutomationRequested(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim cellArgs As CellAutomationEventArgs = TryCast(e, CellAutomationEventArgs)
            If cellArgs IsNot Nothing AndAlso Equals(cellArgs.Column.FieldName, "ChartSource") Then
                cellArgs.AutomationValue = "Line chart"
            End If
        End Sub
    End Class
End Namespace
