Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class CrossBandControls
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.CrossBandControls.Report()
        End Function
    End Class
End Namespace
