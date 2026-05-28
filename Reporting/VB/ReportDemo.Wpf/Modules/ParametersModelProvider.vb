Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraReports.Parameters.ViewModels

Namespace ReportWpfDemo

    <TargetType(GetType(DocumentPreviewControl))>
    Public Class ParametersModelProvider
        Inherits ServiceBase

        Public ReadOnly Property ParametersModel As ParameterPanelViewModel
            Get
                Return If(IsAttached, TryCast(AssociatedObject, DocumentPreviewControl).[With](Function(x) x.ParameterPanelViewModel), Nothing)
            End Get
        End Property
    End Class
End Namespace
