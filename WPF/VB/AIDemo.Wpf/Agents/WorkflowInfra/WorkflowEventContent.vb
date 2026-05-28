Imports System
Imports Microsoft.Agents.AI.Workflows
Imports Microsoft.Extensions.AI

Namespace AIDemo.Agents.WorkflowInfra

    Public NotInheritable Class WorkflowEventContent
        Inherits AIContent

        Public Sub New(ByVal workflowEvent As WorkflowEvent)
            Me.WorkflowEvent = If(workflowEvent, CSharpImpl.__Throw(Of WorkflowEvent)(New ArgumentNullException(NameOf(workflowEvent))))
            RawRepresentation = workflowEvent
        End Sub

        Public ReadOnly Property WorkflowEvent As WorkflowEvent

        Public ReadOnly Property EventType As String
            Get
                Return WorkflowEvent.GetType().Name
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return EventType
        End Function

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal throw statements")>
            Shared Function __Throw(Of T)(ByVal e As Exception) As T
                Throw e
            End Function
        End Class
    End Class
End Namespace
