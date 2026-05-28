Imports System.Windows.Controls
Imports DevExpress.Data.Filtering

Namespace GridDemo.Filtering

    Public Partial Class CalendarElement
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
            grid.FilterCriteria = New OperandProperty("OrderDate") >= Date.Today.AddDays(-5) And New OperandProperty("OrderDate") < Date.Today.AddDays(1)
        End Sub
    End Class
End Namespace
