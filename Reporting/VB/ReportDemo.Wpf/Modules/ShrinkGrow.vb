Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class ShrinkGrow
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.ShrinkGrow.Report()
        End Function
    End Class
End Namespace
