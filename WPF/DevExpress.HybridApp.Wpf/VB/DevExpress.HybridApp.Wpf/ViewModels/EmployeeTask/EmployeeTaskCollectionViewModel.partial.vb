Imports System
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

#If NET
using System.Collections.Generic;
using System.Linq;
using DevExpress.AIIntegration.Extensions;
#End If
Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeTaskCollectionViewModel
        Implements ISupportFiltering(Of EmployeeTask), IFilterTreeViewModelContainer(Of EmployeeTask, Long)

        Public Sub ShowPrintPreview()
            Dim link = GetRequiredService(Of Common.View.IPrintableControlPreviewService)().GetLink()
            Me.GetRequiredService(Of IDocumentManagerService)("FrameDocumentUIService").CreateDocument("PrintableControlPrintPreview", PrintableControlPreviewViewModel.Create(link), Nothing, Me).Show()
        End Sub

        Public Overridable Property FilterTreeViewModel As FilterTreeViewModel(Of EmployeeTask, Long) Implements IFilterTreeViewModelContainer(Of EmployeeTask, Long).FilterTreeViewModel

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of EmployeeTask, Boolean)) Implements ISupportFiltering(Of EmployeeTask).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of EmployeeTask, Boolean)))
                FilterExpression = value
            End Set
        End Property

        Private Property FilterString As String Implements ISupportFilteringType.FilterString
#If NET
        IEnumerable<PromptToExpressionRequestBase.ColumnInfo> ISupportFiltering.GetMetadata() => Enumerable.Empty<PromptToExpressionRequestBase.ColumnInfo>();
#End If
#End Region
    End Class
End Namespace
