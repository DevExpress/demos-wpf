Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class CrossBandContent
        Inherits ReportModuleViewModelBase

        Public Overrides ReadOnly Property ShowPageMargins As Boolean
            Get
                Return False
            End Get
        End Property

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.CrossBandContent.Report()
        End Function
    End Class
End Namespace
