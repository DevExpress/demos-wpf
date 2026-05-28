Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class BarCodes
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.BarCodes.BarCodeTypesReport()
        End Function
    End Class
End Namespace
