Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.XtraReports.UI

Namespace ReportWpfDemo

    Public Class ReportDesignerViewModel
        Inherits ImmutableObject

        Private _Report As XtraReport, _ReportSavedCommand As ICommand, _Saved As Boolean

        Public Property Report As XtraReport
            Get
                Return _Report
            End Get

            Private Set(ByVal value As XtraReport)
                _Report = value
            End Set
        End Property

        Public Property ReportSavedCommand As ICommand
            Get
                Return _ReportSavedCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ReportSavedCommand = value
            End Set
        End Property

        Public Property Saved As Boolean
            Get
                Return _Saved
            End Get

            Private Set(ByVal value As Boolean)
                _Saved = value
            End Set
        End Property

        Public Sub New(ByVal report As XtraReport)
            Guard.ArgumentNotNull(report, "report")
            ReportSavedCommand = New DelegateCommand(AddressOf OnReportSaved)
            Me.Report = report
        End Sub

        Private Sub OnReportSaved()
            Saved = True
        End Sub
    End Class
End Namespace
