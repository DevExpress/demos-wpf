Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Agents.AI
Imports Microsoft.Agents.AI.Workflows
Imports Microsoft.Extensions.AI

Namespace AIDemo.Agents.Publishing

    Public NotInheritable Class PublishingGroupChatManager
        Inherits GroupChatManager

        Private ReadOnly agents As IReadOnlyList(Of AIAgent)

        Public Sub New(ByVal agents As IReadOnlyList(Of AIAgent))
            Me.agents = If(agents, CSharpImpl.__Throw(Of IReadOnlyList(Of AIAgent))(New ArgumentNullException(NameOf(agents))))
        End Sub

        Protected Overrides Function SelectNextAgentAsync(ByVal history As IReadOnlyList(Of ChatMessage), ByVal Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of AIAgent)
            If history.Count = 0 Then Throw New InvalidOperationException("Conversation is empty; cannot select next speaker.")
            If IterationCount = 0 Then Return New ValueTask(Of AIAgent)(agents.First(Function(a) Equals(a.Name, "ContentReviewer")))
            Return New ValueTask(Of AIAgent)(agents.First(Function(a) Equals(a.Name, "Publisher")))
        End Function

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal throw statements")>
            Shared Function __Throw(Of T)(ByVal e As Exception) As T
                Throw e
            End Function
        End Class
    End Class
End Namespace
