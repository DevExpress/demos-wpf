Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class PdfVisualSignature
        Inherits ReportModuleViewModelBase

        Public Overrides ReadOnly Property DefaultZoomMode As ZoomMode
            Get
                Return ZoomMode.ActualSize
            End Get
        End Property

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.PdfVisualSignature.Report()
        End Function
    End Class
End Namespace
