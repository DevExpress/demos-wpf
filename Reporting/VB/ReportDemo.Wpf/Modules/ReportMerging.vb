Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class ReportMerging
        Inherits ReportModuleViewModelBase

        Public Overrides ReadOnly Property PageDisplayMode As PageDisplayMode
            Get
                Return PageDisplayMode.Columns
            End Get
        End Property

        Public Overrides ReadOnly Property PageColumnsCount As Integer
            Get
                Return 3
            End Get
        End Property

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.ReportMerging.MergedReport()
        End Function
    End Class
End Namespace
