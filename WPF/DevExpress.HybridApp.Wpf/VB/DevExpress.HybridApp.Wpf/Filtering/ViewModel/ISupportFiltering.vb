Imports System
Imports System.Linq.Expressions

#If NET
using DevExpress.AIIntegration.Extensions;
#End If
Namespace DevExpress.DevAV.ViewModels

    Public Interface ISupportFiltering(Of TEntity As Class)
        Inherits ISupportFilteringType

        Property FilterExpression As Expression(Of Func(Of TEntity, Boolean))

    End Interface

    Public Interface ISupportFilteringType

        Property FilterString As String

#If NET
        IEnumerable<PromptToExpressionRequestBase.ColumnInfo> GetMetadata();
#End If
    End Interface
End Namespace
