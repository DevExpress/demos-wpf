Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CustomerReportsViewModel.(cs)")>
    Public Partial Class CustomerReports
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler reportsList.EditValueChanged, AddressOf OnReportsListEditValueChanged
        End Sub

        Private Sub OnReportsListEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            Dim value = e.NewValue?.ToString()
            If String.IsNullOrEmpty(value) Then
                MainWindow.CommonRibbon.ExportPivotGrid = Nothing
                Return
            End If

            Dim pivot = TryCast(PivotGridHolder.FindName(value & "PivotGrid"), PivotGridControl)
            MainWindow.CommonRibbon.ExportPivotGrid = pivot
        End Sub
    End Class

    Public Enum CustomerReportType
        Customers
        <Display(Name:="Products (filtering)")>
        Products
        <Display(Name:="Top 2 Products")>
        Top2Products
        <Display(Name:="Top 10 Customers")>
        Top10Customers
    End Enum
End Namespace
