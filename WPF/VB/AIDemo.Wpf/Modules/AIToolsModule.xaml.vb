Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.AIIntegration.Blazor.Chat
Imports DevExpress.AIIntegration.Blazor.Chat.WebView
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Grid
Imports Microsoft.Extensions.AI

Namespace AIDemo

    Public Partial Class AIToolsModule

        Public Const ToolsChatResponseProviderServiceKey As String = "ToolsChatResponseProvider"

        Const SystemMessage As String = "You are an assistant integrated into an application. Your role is to perform all user-requested UI operations exclusively through the provided tools.

Rules:
1. Always use tools to interact with the UI. Do not simulate actions or produce imagined results.
2. If a tool requires a tool-target identifier, obtain it only by calling the GetToolTargets tool. Never invent, guess, or construct identifiers; always use one returned by GetToolTargets.
3. Provide valid parameter values for every tool call, using context or additional tool calls when needed.
4. If the user requests an action that cannot be performed through the available tools, clearly state that it is not supported."

        Private layoutPromptSuggestions As ObservableCollection(Of IPromptSuggestion) = New ObservableCollection(Of IPromptSuggestion) From {New PromptSuggestion(title:="Smart Paste", text:="Auto-populate the DevExpress Layout Control form", prompt:="Populate this form with sample data: " & Microsoft.VisualBasic.vbCrLf & "Payment: Amount - $123.00" & Microsoft.VisualBasic.vbCrLf & "Statement Date: 10/15/2024" & Microsoft.VisualBasic.vbCrLf & "Account Number: 133244556" & Microsoft.VisualBasic.vbCrLf & "Name: John Smith" & Microsoft.VisualBasic.vbCrLf & "Contact: (123) 456-7890" & Microsoft.VisualBasic.vbCrLf & "Email: john@myemail.com" & Microsoft.VisualBasic.vbCrLf & "Address:" & Microsoft.VisualBasic.vbCrLf & "- 123 Elm St Apt 4B" & Microsoft.VisualBasic.vbCrLf & "- New York, NY 10001", sendOnClick:=True), New PromptSuggestion(title:="Application Theme", text:="Switch to dark mode", prompt:="Change the application theme to dark mode", sendOnClick:=True), New PromptSuggestion(title:="Application Layout", text:="Switch to full-window mode", prompt:="Switch the application to full-window mode", sendOnClick:=True)}

        Private gridPromptSuggestions As ObservableCollection(Of IPromptSuggestion) = New ObservableCollection(Of IPromptSuggestion) From {New PromptSuggestion(title:="Sort Grid Column", text:="Sort the grid against a specific column in ascending order.", prompt:="Sort the grid by Country in ascending order.", sendOnClick:=True), New PromptSuggestion(title:="Group Grid Data", text:="Group grid data against a specific column.", prompt:="Group grid data by Country.", sendOnClick:=True), New PromptSuggestion(title:="Clear Grouping", text:="Remove all data groupings from the grid.", prompt:="Clear all data groupings in the grid.", sendOnClick:=True), New PromptSuggestion(title:="Filter Grid Data", text:="Filter the grid by order total amount.", prompt:="Filter the grid and display orders where total amount is greater than 500.", sendOnClick:=True), New PromptSuggestion(title:="Clear Filters", text:="Remove all data filters from the grid.", prompt:="Clear all data filters from the grid.", sendOnClick:=True), New PromptSuggestion(title:="Application Theme", text:="Switch to dark mode", prompt:="Change the application theme to dark mode", sendOnClick:=True), New PromptSuggestion(title:="Application Layout", text:="Switch to full-screen mode.", prompt:="Switch the application to full-window mode.", sendOnClick:=True)}

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub SetSuggestions(ByVal tabItem As DXTabItem)
            AiChatControl.PromptSuggestions = If(TypeOf tabItem.Content Is GridControl, gridPromptSuggestions, layoutPromptSuggestions)
        End Sub

        Private Sub TabControl_OnSelectionChanged(ByVal sender As Object, ByVal e As TabControlSelectionChangedEventArgs)
            If e.NewSelectedItem Is Nothing OrElse AiChatControl Is Nothing Then Return
            Dim tabItem = TryCast(e.NewSelectedItem, DXTabItem)
            If tabItem.Content Is Nothing Then Return
            SetSuggestions(tabItem)
        End Sub

        ' The ResponseReceived event occurs when the AIChatControl receives a response from the AI service.
        ' Handle this event to modify metadata about invoked tools/functions.
        Private Sub OnResponseReceived(ByVal sender As Object, ByVal e As AIChatControlResponseReceivedEventArgs)
            If e.Message.FunctionCalls.Count > 0 Then
                ' Remove the information about the GetToolTargets function used internally by the LLM to select a tool target.
                If e.Message.FunctionCalls.RemoveAll(Function(fcall) Equals(fcall.Request.Name, "GetToolTargets")) > 0 Then
                    ' Remove internal '*_identifier' arguments from function calls.
                    ' These arguments specify tool target selection information and are not intended for user display.
                    e.Message.FunctionCalls.ForEach(Sub(fcall)
                        Dim argumentsToRemove = fcall.Request.Arguments.Keys.Where(Function(k) k.EndsWith("_identifier")).ToList()
                        For Each argument In argumentsToRemove
                            fcall.Request.Arguments.Remove(argument)
                        Next
                    End Sub)
                End If
            End If
        End Sub

        Private Sub AiChatControl_Initialized(ByVal sender As Object, ByVal e As AIChatControlInitializedEventArgs)
            SetSuggestions(TryCast(tabControl.SelectedItem, DXTabItem))
            AiChatControl.LoadMessages({New BlazorChatMessage(ChatRole.System, SystemMessage)})
        End Sub
    End Class
End Namespace
