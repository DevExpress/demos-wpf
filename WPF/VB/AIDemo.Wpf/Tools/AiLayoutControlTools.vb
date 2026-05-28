Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Wpf
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.LayoutControl

Namespace AIDemo.Tools

    Public Module AiLayoutControlTools

        <AIIntegrationTool("LayoutControl_Populate")>
        <Description("Populate a LayoutControl's fields from a JSON object. Returns a text with the operation result.")>
        Public Async Function Populate(<AIIntegrationToolTarget("Target LayoutControl to fill.")> ByVal layoutControl As LayoutControl, <Description("Field values as a JSON object, for example: { ""FirstName"": ""John"", ""LastName"": ""Doe"" }")> ByVal data As String) As Task(Of String)
            ArgumentNullException.ThrowIfNull(layoutControl)
            Dim behaviors = Interaction.GetBehaviors(layoutControl)
            Dim smartPasteBehavior = behaviors.OfType(Of SmartPasteBehavior)().FirstOrDefault()
            Try
                Await smartPasteBehavior.SmartPasteAsync(data)
                Return "Fields populated in the LayoutControl."
            Catch
                Return "Error: failed to populate fields in the LayoutControl."
            End Try
        End Function
    End Module
End Namespace
