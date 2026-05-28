Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Automation
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Extensions
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.DataAccess.ExpressionEditor
Imports DevExpress.Xpf.Grid

Namespace AIDemo.Tools

    Public Module AIGridTools

        <AIIntegrationTool>
        <Description("Retrieve information about the columns in the specified Grid.")>
        Public Function RetrieveColumnsInfo(<AIIntegrationToolTarget("Target Grid from which to retrieve column information.")> ByVal gridControl As GridControl) As Object()
            Return gridControl.Columns.Where(Function(c) c.Visible).[Select](Function(c) New With {.Caption = c.HeaderCaption, c.FieldName, .Description = AutomationProperties.GetHelpText(c)}).ToArray()
        End Function

        <AIIntegrationTool>
        <Description("Sort the Grid by a specified column in ascending or descending order. Returns a confirmation message or an actionable error.")>
        Public Function SortByColumn(<AIIntegrationToolTarget("Target Grid to sort.")> ByVal gridControl As GridControl, <Description("The name of the column to sort by. Use the column caption or field name.")> ByVal columnName As String, <Description("Sort direction: true for ascending, false for descending.")> ByVal isAscending As Boolean) As String
            Dim column = FindColumn(gridControl, columnName)
            If column Is Nothing Then Return $"Error: Column '{columnName}' not found. Please check the column name and try again."
            gridControl.SortBy(column, If(isAscending, ColumnSortOrder.Ascending, ColumnSortOrder.Descending))
            Return $"Sorting applied to '{column.FieldName}' in {If(isAscending, "ascending", "descending")} order."
        End Function

        <AIIntegrationTool>
        <Description("Remove sorting from the specified Grid column. Returns a confirmation message or an actionable error.")>
        Public Function RemoveColumnSorting(<AIIntegrationToolTarget("Target Grid to remove sorting from.")> ByVal gridControl As GridControl, <Description("The name of the column to remove sorting from. Use the column caption or field name.")> ByVal columnName As String) As String
            Dim column = FindColumn(gridControl, columnName)
            If column Is Nothing Then Return $"Error: Column '{columnName}' not found. Please check the column name and try again."
            column.SortOrder = ColumnSortOrder.None
            Return $"Sorting removed from column '{column.FieldName}'."
        End Function

        <AIIntegrationTool>
        <Description("Groups the Grid data by the specified column. Optionally specify the grouping order. Example: 'Group the grid by Country.' Returns a confirmation or actionable error.")>
        Public Function GroupByColumn(<AIIntegrationToolTarget("Target Grid to group.")> ByVal gridControl As GridControl, <Description("The name of the column to group by. Use the column caption or field name.")> ByVal columnName As String, <Description("The grouping order index (optional, default is 0). Lower index means higher grouping priority.")> ByVal Optional groupIndex As Integer = 0) As String
            Dim column = FindColumn(gridControl, columnName)
            If column Is Nothing Then Return $"Error: Column '{columnName}' not found. Please check the column name and try again."
            gridControl.GroupBy(column, groupIndex)
            Return $"Grouping applied successfully on column '{columnName}' at index {groupIndex}."
        End Function

        <AIIntegrationTool>
        <Description("Removes all grouping from the Grid. Returns a confirmation message.")>
        Public Function ClearGrouping(<AIIntegrationToolTarget("Target Grid to clear grouping from.")> ByVal gridControl As GridControl) As String
            gridControl.ClearGrouping()
            Return "All grouping cleared from the grid view."
        End Function

        <AIIntegrationTool>
        <Description("Create and apply a grid filter from a natural-language prompt. Converts the prompt to a filter expression and applies it. Returns a summary of the filter applied or actionable error.")>
        Public Async Function SetFilter(<AIIntegrationToolTarget("Target Grid to apply the filter to.")> ByVal gridControl As GridControl, <Description("Natural-language description of the filter. Examples: ""Show orders from 2024"", ""price > 100 and status = 'Paid'"".")> ByVal userPrompt As String) As Task(Of String)
            If String.IsNullOrWhiteSpace(userPrompt) Then Return "Error: filter prompt is empty."
            Dim functions = ExpressionEditorContextHelper.GetFunctions().ToRequestFunctionInfo()
            Dim columns = gridControl.Columns.[Select](Function(c) New PromptToExpressionRequestBase.ColumnInfo(c.FieldName, CStr(c.HeaderCaption), If(AutomationProperties.GetHelpText(c), CStr(c.HeaderCaption)), c.FieldType, New List(Of PromptToExpressionRequestBase.ColumnInfo)())).ToList()
            Dim request = New PromptToFilterRequest(userPrompt, String.Empty, columns, functions)
            Dim result = Await AIExtensionsContainerDesktop.Default.PromptToExpressionAsync(request)
            If String.IsNullOrEmpty(result.Response) Then Return "Error: failed to generate a filter expression."
            Try
                CriteriaOperator.Parse(result.Response)
            Catch ex As Exception
                Return $"Error: generated filter expression '{result.Response}' is invalid. {ex.Message}"
            End Try

            gridControl.FilterString = result.Response
            Return $"Filter applied: {result.Response})"
        End Function

        <AIIntegrationTool>
        <Description("Clears all filters from the Grid. Returns a confirmation message.")>
        Public Function ClearFilter(<AIIntegrationToolTarget("Target Grid to clear filters from.")> ByVal gridControl As GridControl) As String
            gridControl.FilterString = String.Empty
            Return "All filters cleared from the grid view."
        End Function

        Private Function FindColumn(ByVal gridControl As GridControl, ByVal columnName As String) As GridColumn
            Return gridControl.Columns.FirstOrDefault(Function(c) String.Equals(c.Name, columnName, StringComparison.OrdinalIgnoreCase) OrElse String.Equals(c.FieldName, columnName, StringComparison.OrdinalIgnoreCase) OrElse String.Equals(CStr(c.HeaderCaption), columnName, StringComparison.OrdinalIgnoreCase))
        End Function

        Private Function GetColumnInfo(ByVal columns As GridColumnCollection) As IList(Of PromptToExpressionRequestBase.ColumnInfo)
            Return columns.[Select](Function(c) New PromptToExpressionRequestBase.ColumnInfo(c.FieldName, CStr(c.HeaderCaption), CStr(c.HeaderCaption), c.FieldType, New List(Of PromptToExpressionRequestBase.ColumnInfo)())).ToList()
        End Function
    End Module
End Namespace
