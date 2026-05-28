Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class VehicleInspectionReport
        Inherits ReportModuleViewModelBase

        Public Overrides ReadOnly Property DefaultZoomMode As ZoomMode
            Get
                Return ZoomMode.PageLevel
            End Get
        End Property

        Public Overrides ReadOnly Property PageDisplayMode As PageDisplayMode
            Get
                Return PageDisplayMode.Columns
            End Get
        End Property

        Public Overrides ReadOnly Property PageColumnsCount As Integer
            Get
                Return 2
            End Get
        End Property

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.VehicleInspectionReport.Report()
        End Function
    End Class
End Namespace
