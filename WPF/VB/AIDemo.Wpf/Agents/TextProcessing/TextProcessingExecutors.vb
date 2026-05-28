Imports System
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.Agents.AI.Workflows

Namespace AIDemo.Agents.TextProcessing

    Public NotInheritable Class UppercaseExecutor
        Inherits Executor(Of String, String)

        Public Sub New()
            MyBase.New("UppercaseExecutor")
        End Sub

        Public Overrides Function HandleAsync(ByVal message As String, ByVal context As IWorkflowContext, ByVal Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of String)
            Return ValueTask.FromResult(message.ToUpperInvariant())
        End Function
    End Class

    Public NotInheritable Class ReverseTextExecutor
        Inherits Executor(Of String, String)

        Public Sub New()
            MyBase.New("ReverseTextExecutor")
        End Sub

        Public Overrides Function HandleAsync(ByVal message As String, ByVal context As IWorkflowContext, ByVal Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of String)
            Return ValueTask.FromResult(String.Concat(message.Reverse()))
        End Function
    End Class
End Namespace
