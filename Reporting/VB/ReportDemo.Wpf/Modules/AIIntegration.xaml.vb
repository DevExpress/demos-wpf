Imports DevExpress.XtraReports
Imports ReportWpfDemo.AIOperations

Namespace ReportWpfDemo

    Public Partial Class AIIntegration
        Inherits ReportModuleBase

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resources.EndpointWarning
        End Sub
    End Class

    Public Class AIIntegrationModuleViewModel
        Inherits ReportModuleViewModelBase

        Shared Sub New()
            Call RegisterIfNeeded()
        End Sub

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.NorthwindTraders.InvoiceReport()
        End Function
    End Class
End Namespace
