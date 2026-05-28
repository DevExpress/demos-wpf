Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class VerticalAnchoring
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.AnchorVertical.Report()
        End Function
    End Class
End Namespace
