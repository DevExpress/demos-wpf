Imports System.Reflection
Imports DevExpress.Xpf.Grid.Automation

Namespace GridDemo

    Public Partial Class RowTemplate
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsRowTemplate
        End Sub

        Private Sub TableView_AutomationRequested(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim rowArgs As RowAutomationEventArgs = TryCast(e, RowAutomationEventArgs)
            If rowArgs IsNot Nothing Then
                Dim dataItem As Object = rowArgs.Row
                If dataItem IsNot Nothing Then
                    Dim fullName As Object = GetPropertyValue(dataItem, "FullName")
                    Dim title As Object = GetPropertyValue(dataItem, "Title")
                    Dim hireDate As Object = GetPropertyValue(dataItem, "HireDate")
                    Dim notes As Object = GetPropertyValue(dataItem, "Notes")
                    Dim address As Object = GetPropertyValue(dataItem, "Address")
                    rowArgs.AutomationValue = $"Name: {fullName}; " & $"Position: {title}; " & $"Hire Date: {CType(hireDate, Date?)?.ToLongDateString()}; " & $"About: {notes}; " & $"Address: {address}."
                End If
            End If
        End Sub

        Private Function GetPropertyValue(ByVal obj As Object, ByVal propertyName As String) As Object
            Dim [property] = obj.GetType().GetProperty(propertyName, BindingFlags.Public Or BindingFlags.Instance)
            Return [property]?.GetValue(obj)
        End Function
    End Class
End Namespace
