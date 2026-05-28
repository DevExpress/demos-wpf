Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class PivotGridAndChart
        Inherits ReportModuleViewModelBase

        Public Overrides ReadOnly Property DefaultZoomMode As ZoomMode
            Get
                Return ZoomMode.PageLevel
            End Get
        End Property

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.PivotGridAndChart.Report()
        End Function
    End Class
End Namespace
