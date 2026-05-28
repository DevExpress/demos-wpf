Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports System.Threading.Channels
Imports System.Threading.Tasks
Imports System.Runtime.ExceptionServices
Imports DevExpress.AIIntegration.Chat
Imports Microsoft.Agents.AI
Imports Microsoft.Agents.AI.Workflows
Imports Microsoft.Agents.AI.Workflows.InProc
Imports Microsoft.Extensions.AI

Namespace AIDemo.Agents.WorkflowInfra

    Public Enum WorkflowResponseProviderStartMode
        Run
        Open
        [Resume]
    End Enum

    Public NotInheritable Class WorkflowResponseProvider(Of TInput)
        Inherits DevExpress.AIIntegration.Chat.ChatResponseProvider

        Private ReadOnly workflow As Microsoft.Agents.AI.Workflows.Workflow

        Private ReadOnly inputFactory As System.Func(Of System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), TInput)

        Private ReadOnly executionEnvironment As Microsoft.Agents.AI.Workflows.InProc.InProcessExecutionEnvironment

        Private ReadOnly sessionId As String

        Private ReadOnly checkpointManager As Microsoft.Agents.AI.Workflows.CheckpointManager

        Private ReadOnly checkpointInfo As Microsoft.Agents.AI.Workflows.CheckpointInfo

        Private ReadOnly startMode As AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode

        Private ReadOnly emitEvents As Boolean

        Private activeStreamingRun As Microsoft.Agents.AI.Workflows.StreamingRun

        Private ReadOnly pendingRequests As System.Collections.Generic.Dictionary(Of String, Microsoft.Agents.AI.Workflows.ExternalRequest) = New System.Collections.Generic.Dictionary(Of String, Microsoft.Agents.AI.Workflows.ExternalRequest)(System.StringComparer.Ordinal)

        Public Sub New(ByVal workflow As Microsoft.Agents.AI.Workflows.Workflow, ByVal inputFactory As System.Func(Of System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), TInput))
            Me.New(workflow, inputFactory, AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.Run)
        End Sub

        Public Sub New(ByVal workflow As Microsoft.Agents.AI.Workflows.Workflow, ByVal inputFactory As System.Func(Of System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), TInput), ByVal startMode As AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode, ByVal Optional executionEnvironment As Microsoft.Agents.AI.Workflows.InProc.InProcessExecutionEnvironment = Nothing, ByVal Optional sessionId As String = Nothing, ByVal Optional checkpointManager As Microsoft.Agents.AI.Workflows.CheckpointManager = Nothing, ByVal Optional emitEvents As Boolean = True)
            If workflow Is Nothing Then Throw New System.ArgumentNullException(NameOf(workflow))
            If inputFactory Is Nothing Then Throw New System.ArgumentNullException(NameOf(inputFactory))
            Me.workflow = workflow
            Me.inputFactory = inputFactory
            If startMode = AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.[Resume] Then Throw New System.ArgumentException("Use a checkpoint-based constructor for resume mode.", NameOf(startMode))
            Me.executionEnvironment = If(executionEnvironment, Microsoft.Agents.AI.Workflows.InProcessExecution.[Default])
            Me.sessionId = sessionId
            Me.checkpointManager = checkpointManager
            Me.startMode = startMode
            Me.emitEvents = emitEvents
        End Sub

        Public Sub New(ByVal workflow As Microsoft.Agents.AI.Workflows.Workflow, ByVal checkpointInfo As Microsoft.Agents.AI.Workflows.CheckpointInfo, ByVal checkpointManager As Microsoft.Agents.AI.Workflows.CheckpointManager, ByVal Optional executionEnvironment As Microsoft.Agents.AI.Workflows.InProc.InProcessExecutionEnvironment = Nothing, ByVal Optional emitEvents As Boolean = True)
            If workflow Is Nothing Then Throw New System.ArgumentNullException(NameOf(workflow))
            If checkpointInfo Is Nothing Then Throw New System.ArgumentNullException(NameOf(checkpointInfo))
            If checkpointManager Is Nothing Then Throw New System.ArgumentNullException(NameOf(checkpointManager))
            Me.workflow = workflow
            Me.checkpointInfo = checkpointInfo
            Me.checkpointManager = checkpointManager
            Me.executionEnvironment = If(executionEnvironment, Microsoft.Agents.AI.Workflows.InProcessExecution.[Default])
            Me.startMode = AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.[Resume]
            Me.emitEvents = emitEvents
        End Sub

        Protected Overrides Function GetStreamingResponseCoreAsync(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal conversationId As String, ByVal cancellationToken As System.Threading.CancellationToken) As IAsyncEnumerable(Of Microsoft.Extensions.AI.ChatResponseUpdate)
            Return Me.GetStreamingResponseAsync(messages, cancellationToken)
        End Function

        Protected Overrides Function GetBufferedResponseCoreAsync(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal conversationId As String, ByVal cancellationToken As System.Threading.CancellationToken) As IAsyncEnumerable(Of Microsoft.Extensions.AI.ChatResponseUpdate)
            Dim linkedCts = System.Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            Dim channel = System.Threading.Channels.Channel.CreateBounded(Of Microsoft.Extensions.AI.ChatResponseUpdate)(New System.Threading.Channels.BoundedChannelOptions(16) With {.SingleWriter = True, .SingleReader = True, .FullMode = System.Threading.Channels.BoundedChannelFullMode.Wait})
            Dim produceTask As System.Threading.Tasks.Task = Me.ProduceBufferedResponseAsync(channel.Writer, messages, linkedCts.Token)
            produceTask.ContinueWith(Sub(task, state) CType(state, System.Threading.CancellationTokenSource).Dispose(), linkedCts, System.Threading.Tasks.TaskScheduler.[Default])
            Return channel.Reader.ReadAllAsync(linkedCts.Token)
        End Function

        Private Async Function ProduceBufferedResponseAsync(ByVal writer As System.Threading.Channels.ChannelWriter(Of Microsoft.Extensions.AI.ChatResponseUpdate), ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal cancellationToken As System.Threading.CancellationToken) As Task
            If Me.startMode = AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.Open Then
                Await Me.ProduceStreamingResponseAsync(CType((writer), System.Threading.Channels.ChannelWriter(Of Microsoft.Extensions.AI.ChatResponseUpdate)), CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                Return
            End If

            Dim finalError As System.Exception = Nothing
            Dim run As Microsoft.Agents.AI.Workflows.Run = Nothing
            Try
                run = Await Me.StartBufferedRunAsync(CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                For Each workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent In run.NewEvents
                    For Each update In Me.ConvertEvent(workflowEvent)
                        Await writer.WriteAsync(CType((update), Microsoft.Extensions.AI.ChatResponseUpdate), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                    Next
                Next
            Catch ex As System.Exception
                finalError = ex
            End Try

            If run IsNot Nothing Then
                Try
                    Await run.DisposeAsync().ConfigureAwait(False)
                Catch disposeError As System.Exception
                    If finalError Is Nothing Then finalError = disposeError
                Finally
                    writer.Complete(finalError)
                End Try
            Else
                writer.Complete(finalError)
            End If

            If finalError IsNot Nothing Then Call System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(CType((finalError), System.Exception)).[Throw]()
        End Function

        Private Function GetStreamingResponseAsync(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal cancellationToken As System.Threading.CancellationToken) As IAsyncEnumerable(Of Microsoft.Extensions.AI.ChatResponseUpdate)
            Dim linkedCts = System.Threading.CancellationTokenSource.CreateLinkedTokenSource(cancellationToken)
            Dim channel = System.Threading.Channels.Channel.CreateBounded(Of Microsoft.Extensions.AI.ChatResponseUpdate)(New System.Threading.Channels.BoundedChannelOptions(16) With {.SingleWriter = True, .SingleReader = True, .FullMode = System.Threading.Channels.BoundedChannelFullMode.Wait})
            Dim produceTask As System.Threading.Tasks.Task = Me.ProduceStreamingResponseAsync(channel.Writer, messages, linkedCts.Token)
            produceTask.ContinueWith(Sub(task, state) CType(state, System.Threading.CancellationTokenSource).Dispose(), linkedCts, System.Threading.Tasks.TaskScheduler.[Default])
            Return channel.Reader.ReadAllAsync(linkedCts.Token)
        End Function

        Private Async Function ProduceStreamingResponseAsync(ByVal writer As System.Threading.Channels.ChannelWriter(Of Microsoft.Extensions.AI.ChatResponseUpdate), ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal cancellationToken As System.Threading.CancellationToken) As Task
            Dim finalError As System.Exception = Nothing
            Dim run As Microsoft.Agents.AI.Workflows.StreamingRun = Nothing
            Try
                If Me.startMode = AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.Open Then
                    Dim openRun As Microsoft.Agents.AI.Workflows.StreamingRun = Await Me.StartStreamingRunAsync(CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                    Dim shouldKeepRun As Boolean = False
                    Dim eventEnumerator = openRun.WatchStreamAsync(CType((cancellationToken), System.Threading.CancellationToken)).GetAsyncEnumerator(cancellationToken)
                    Dim enumerationError As System.Exception = Nothing
                    Try
                        While Await eventEnumerator.MoveNextAsync().ConfigureAwait(False)
                            Dim workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent = eventEnumerator.Current
                            For Each update In Me.ConvertEvent(workflowEvent)
                                Await writer.WriteAsync(CType((update), Microsoft.Extensions.AI.ChatResponseUpdate), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                            Next

                            If TypeOf workflowEvent Is Microsoft.Agents.AI.Workflows.RequestInfoEvent Then
                                shouldKeepRun = True
                                Exit While
                            End If

                            If TypeOf workflowEvent Is Microsoft.Agents.AI.Workflows.WorkflowOutputEvent Then shouldKeepRun = False
                        End While
                    Catch ex As System.Exception
                        enumerationError = ex
                    End Try

                    Await eventEnumerator.DisposeAsync().ConfigureAwait(False)
                    If enumerationError IsNot Nothing Then Call System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(CType((enumerationError), System.Exception)).[Throw]()
                    If Not shouldKeepRun Then Await Me.ResetActiveStreamingRunAsync().ConfigureAwait(False)
                    Return
                End If

                run = Await Me.StartStreamingRunAsync(CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                Dim runEnumerator = run.WatchStreamAsync(CType((cancellationToken), System.Threading.CancellationToken)).GetAsyncEnumerator(cancellationToken)
                Dim runEnumerationError As System.Exception = Nothing
                Try
                    While Await runEnumerator.MoveNextAsync().ConfigureAwait(False)
                        Dim workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent = runEnumerator.Current
                        For Each update In Me.ConvertEvent(workflowEvent)
                            Await writer.WriteAsync(CType((update), Microsoft.Extensions.AI.ChatResponseUpdate), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                        Next
                    End While
                Catch ex As System.Exception
                    runEnumerationError = ex
                End Try

                Await runEnumerator.DisposeAsync().ConfigureAwait(False)
                If runEnumerationError IsNot Nothing Then Call System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(CType((runEnumerationError), System.Exception)).[Throw]()
            Catch ex As System.Exception
                finalError = ex
            End Try

            If run IsNot Nothing Then
                Try
                    Await run.DisposeAsync().ConfigureAwait(False)
                Catch disposeError As System.Exception
                    If finalError Is Nothing Then finalError = disposeError
                Finally
                    writer.Complete(finalError)
                End Try
            Else
                writer.Complete(finalError)
            End If

            If finalError IsNot Nothing Then Call System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(CType((finalError), System.Exception)).[Throw]()
        End Function

        Private Async Function StartStreamingRunAsync(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal cancellationToken As System.Threading.CancellationToken) As Task(Of Microsoft.Agents.AI.Workflows.StreamingRun)
            Dim env As Microsoft.Agents.AI.Workflows.InProc.InProcessExecutionEnvironment = Me.GetExecutionEnvironment()
            Select Case Me.startMode
                Case AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.Run
                    Dim streamingRun As Microsoft.Agents.AI.Workflows.StreamingRun = Await env.RunStreamingAsync(Of TInput)(CType((Me.workflow), Microsoft.Agents.AI.Workflows.Workflow), CType((Me.GetInput(CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)))), TInput), CStr((Me.sessionId)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                    Await Me.SendTurnTokenAsync(CType((streamingRun), Microsoft.Agents.AI.Workflows.StreamingRun)).ConfigureAwait(False)
                    Return streamingRun
                Case AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.Open
                    If Me.activeStreamingRun IsNot Nothing Then
                        Await Me.ContinueOpenRunAsync(CType((Me.activeStreamingRun), Microsoft.Agents.AI.Workflows.StreamingRun), CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage))).ConfigureAwait(False)
                        Return Me.activeStreamingRun
                    End If

                    Dim openedRun As Microsoft.Agents.AI.Workflows.StreamingRun = Await env.OpenStreamingAsync(CType((Me.workflow), Microsoft.Agents.AI.Workflows.Workflow), CStr((Me.sessionId)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                    Me.activeStreamingRun = openedRun
                    Await Me.SendInputAsync(CType((openedRun), Microsoft.Agents.AI.Workflows.StreamingRun), CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage))).ConfigureAwait(False)
                    Await Me.SendTurnTokenAsync(CType((openedRun), Microsoft.Agents.AI.Workflows.StreamingRun)).ConfigureAwait(False)
                    Return openedRun
                Case AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.[Resume]
                    Dim resumedRun As Microsoft.Agents.AI.Workflows.StreamingRun = Await env.ResumeStreamingAsync(CType((Me.workflow), Microsoft.Agents.AI.Workflows.Workflow), CType((Me.checkpointInfo), Microsoft.Agents.AI.Workflows.CheckpointInfo), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
                    Await Me.SendTurnTokenAsync(CType((resumedRun), Microsoft.Agents.AI.Workflows.StreamingRun)).ConfigureAwait(False)
                    Return resumedRun
                Case Else
                    Throw New System.InvalidOperationException($"Unsupported workflow start mode '{Me.startMode}'.")
            End Select
        End Function

        Private Async Function ContinueOpenRunAsync(ByVal run As Microsoft.Agents.AI.Workflows.StreamingRun, ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)) As Task
            If Await Me.SendApprovalResponsesAsync(CType((run), Microsoft.Agents.AI.Workflows.StreamingRun), CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage))).ConfigureAwait(False) Then Return
            If AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).HasNonSystemMessages(messages) Then
                Await Me.SendInputAsync(CType((run), Microsoft.Agents.AI.Workflows.StreamingRun), CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage))).ConfigureAwait(False)
                Await Me.SendTurnTokenAsync(CType((run), Microsoft.Agents.AI.Workflows.StreamingRun)).ConfigureAwait(False)
            End If
        End Function

        Private Async Function StartBufferedRunAsync(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage), ByVal cancellationToken As System.Threading.CancellationToken) As Task(Of Microsoft.Agents.AI.Workflows.Run)
            Dim env As Microsoft.Agents.AI.Workflows.InProc.InProcessExecutionEnvironment = Me.GetExecutionEnvironment()
            If Me.startMode = AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.Run Then Return Await env.RunAsync(Of TInput)(CType((Me.workflow), Microsoft.Agents.AI.Workflows.Workflow), CType((Me.GetInput(CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)))), TInput), CStr((Me.sessionId)), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
            If Me.startMode = AIDemo.Agents.WorkflowInfra.WorkflowResponseProviderStartMode.[Resume] Then Return Await env.ResumeAsync(CType((Me.workflow), Microsoft.Agents.AI.Workflows.Workflow), CType((Me.checkpointInfo), Microsoft.Agents.AI.Workflows.CheckpointInfo), CType((cancellationToken), System.Threading.CancellationToken)).ConfigureAwait(False)
            Throw New System.InvalidOperationException($"Buffered execution is not supported for workflow start mode '{Me.startMode}'.")
        End Function

        Private Function GetExecutionEnvironment() As InProcessExecutionEnvironment
            Dim env As Microsoft.Agents.AI.Workflows.InProc.InProcessExecutionEnvironment = If(Me.executionEnvironment, Microsoft.Agents.AI.Workflows.InProcessExecution.[Default])
            If Me.checkpointManager IsNot Nothing Then env = env.WithCheckpointing(Me.checkpointManager)
            Return env
        End Function

        Private Function GetInput(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)) As TInput
            If Me.inputFactory Is Nothing Then Throw New System.InvalidOperationException("An input factory is required for this workflow execution mode.")
            Return Me.inputFactory(messages)
        End Function

        Private Async Function SendInputAsync(ByVal run As Microsoft.Agents.AI.Workflows.StreamingRun, ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)) As Task
            Await run.TrySendMessageAsync(Of TInput)(CType((Me.GetInput(CType((messages), System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)))), TInput)).ConfigureAwait(False)
        End Function

        Private Async Function SendApprovalResponsesAsync(ByVal run As Microsoft.Agents.AI.Workflows.StreamingRun, ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)) As Task(Of Boolean)
            Dim sent As Boolean = False
            Dim request As Microsoft.Agents.AI.Workflows.ExternalRequest = Nothing
            For Each response As Microsoft.Extensions.AI.ToolApprovalResponseContent In AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).GetApprovalResponses(messages)
                If Not Me.pendingRequests.TryGetValue(response.RequestId, request) Then Continue For
                Me.pendingRequests.Remove(response.RequestId)
                Await run.SendResponseAsync(CType((request.CreateResponse(Of Microsoft.Extensions.AI.ToolApprovalResponseContent)(CType((response), Microsoft.Extensions.AI.ToolApprovalResponseContent))), Microsoft.Agents.AI.Workflows.ExternalResponse)).ConfigureAwait(False)
                sent = True
            Next

            Return sent
        End Function

        Private Async Function SendTurnTokenAsync(ByVal run As Microsoft.Agents.AI.Workflows.StreamingRun) As Task
            Await run.TrySendMessageAsync(Of Microsoft.Agents.AI.Workflows.TurnToken)(CType((New Microsoft.Agents.AI.Workflows.TurnToken(CType((Me.emitEvents), System.[Boolean]?))), Microsoft.Agents.AI.Workflows.TurnToken)).ConfigureAwait(False)
        End Function

        Private Iterator Function ConvertEvent(ByVal workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent) As IEnumerable(Of Microsoft.Extensions.AI.ChatResponseUpdate)
            Dim agentResponseUpdateEvent As Microsoft.Agents.AI.Workflows.AgentResponseUpdateEvent = Nothing
            If CSharpImpl.__Assign(agentResponseUpdateEvent, TryCast(workflowEvent, Microsoft.Agents.AI.Workflows.AgentResponseUpdateEvent)) IsNot Nothing Then
                Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateAgentUpdate(agentResponseUpdateEvent)
                Return
            End If

            Dim requestInfoEvent As Microsoft.Agents.AI.Workflows.RequestInfoEvent = Nothing
            If CSharpImpl.__Assign(requestInfoEvent, TryCast(workflowEvent, Microsoft.Agents.AI.Workflows.RequestInfoEvent)) IsNot Nothing Then
                Yield Me.CreateRequestUpdate(requestInfoEvent)
                Return
            End If

            Dim executorCompletedEvent As Microsoft.Agents.AI.Workflows.ExecutorCompletedEvent = Nothing
            If CSharpImpl.__Assign(executorCompletedEvent, TryCast(workflowEvent, Microsoft.Agents.AI.Workflows.ExecutorCompletedEvent)) IsNot Nothing Then
                Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateExecutorUpdate(executorCompletedEvent)
                Return
            End If

            Dim workflowOutputEvent As Microsoft.Agents.AI.Workflows.WorkflowOutputEvent = Nothing
            If CSharpImpl.__Assign(workflowOutputEvent, TryCast(workflowEvent, Microsoft.Agents.AI.Workflows.WorkflowOutputEvent)) IsNot Nothing Then
                Dim hasOutput As Boolean = False
                For Each update In AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).ConvertOutputData(workflowOutputEvent.Data, workflowOutputEvent)
                    hasOutput = True
                    Yield update
                Next

                If hasOutput Then Return
            End If

            Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateWorkflowEventUpdate(workflowEvent)
        End Function

        Private Function CreateRequestUpdate(ByVal requestInfoEvent As Microsoft.Agents.AI.Workflows.RequestInfoEvent) As ChatResponseUpdate
            Dim approvalRequest As Microsoft.Extensions.AI.ToolApprovalRequestContent = Nothing
            If requestInfoEvent.Request.TryGetDataAs(approvalRequest) Then
                Me.RegisterPendingRequest(requestInfoEvent.Request, approvalRequest.RequestId)
                Return New Microsoft.Extensions.AI.ChatResponseUpdate With {.Contents = New Microsoft.Extensions.AI.AIContent() {approvalRequest}, .RawRepresentation = requestInfoEvent, .Role = Microsoft.Extensions.AI.ChatRole.Assistant}
            End If

            Return AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateWorkflowEventUpdate(requestInfoEvent)
        End Function

        Private Sub RegisterPendingRequest(ByVal request As Microsoft.Agents.AI.Workflows.ExternalRequest, ByVal requestId As String)
            If String.IsNullOrWhiteSpace(requestId) Then Return
            Me.pendingRequests(requestId) = request
        End Sub

        Private Shared Iterator Function GetApprovalResponses(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)) As IEnumerable(Of Microsoft.Extensions.AI.ToolApprovalResponseContent)
            For Each message As Microsoft.Extensions.AI.ChatMessage In messages
                For Each response As Microsoft.Extensions.AI.ToolApprovalResponseContent In message.Contents.OfType(Of Microsoft.Extensions.AI.ToolApprovalResponseContent)()
                    Yield response
                Next
            Next
        End Function

        Private Shared Function HasNonSystemMessages(ByVal messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage)) As Boolean
            For Each message As Microsoft.Extensions.AI.ChatMessage In messages
                If message.Role <> Microsoft.Extensions.AI.ChatRole.System Then Return True
            Next

            Return False
        End Function

        Private Async Function ResetActiveStreamingRunAsync() As Task
            If Me.activeStreamingRun Is Nothing Then Return
            Await Me.activeStreamingRun.DisposeAsync().ConfigureAwait(False)
            Me.activeStreamingRun = Nothing
            Me.pendingRequests.Clear()
        End Function

        Private Shared Iterator Function ConvertOutputData(ByVal data As Object, ByVal workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent) As IEnumerable(Of Microsoft.Extensions.AI.ChatResponseUpdate)
            If data Is Nothing Then Return
            Dim message As Microsoft.Extensions.AI.ChatMessage = Nothing
            If CSharpImpl.__Assign(message, TryCast(data, Microsoft.Extensions.AI.ChatMessage)) IsNot Nothing Then
                Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateMessageUpdate(message, workflowEvent)
                Return
            End If

            Dim messages As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage) = Nothing
            If CSharpImpl.__Assign(messages, TryCast(data, System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.ChatMessage))) IsNot Nothing Then
                For Each messageItem As Microsoft.Extensions.AI.ChatMessage In messages
                    Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateMessageUpdate(messageItem, workflowEvent)
                Next

                Return
            End If

            Dim content As Microsoft.Extensions.AI.AIContent = Nothing
            If CSharpImpl.__Assign(content, TryCast(data, Microsoft.Extensions.AI.AIContent)) IsNot Nothing Then
                Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateContentUpdate(New Microsoft.Extensions.AI.AIContent() {content}, workflowEvent)
                Return
            End If

            Dim contents As System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.AIContent) = Nothing
            If CSharpImpl.__Assign(contents, TryCast(data, System.Collections.Generic.IEnumerable(Of Microsoft.Extensions.AI.AIContent))) IsNot Nothing Then
                Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateContentUpdate(contents.ToList(), workflowEvent)
                Return
            End If

            Dim text As String = Nothing
            If CSharpImpl.__Assign(text, TryCast(data, String)) IsNot Nothing Then
                Yield AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateTextUpdate(text, workflowEvent)
                Return
            End If
        End Function

        Private Shared Function CreateAgentUpdate(ByVal agentResponseUpdateEvent As Microsoft.Agents.AI.Workflows.AgentResponseUpdateEvent) As ChatResponseUpdate
            Dim source As Microsoft.Agents.AI.AgentResponseUpdate = agentResponseUpdateEvent.Update
            Return New Microsoft.Extensions.AI.ChatResponseUpdate With {.AuthorName = source.AuthorName, .ContinuationToken = source.ContinuationToken, .Contents = If(source.Contents IsNot Nothing AndAlso source.Contents.Count > 0, New System.Collections.Generic.List(Of Microsoft.Extensions.AI.AIContent)(source.Contents), AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateTextContents(source.Text)), .CreatedAt = source.CreatedAt, .MessageId = source.MessageId, .RawRepresentation = agentResponseUpdateEvent, .ResponseId = source.ResponseId, .Role = source.Role}
        End Function

        Private Shared Function CreateExecutorUpdate(ByVal executorCompletedEvent As Microsoft.Agents.AI.Workflows.ExecutorCompletedEvent) As ChatResponseUpdate
            Dim text As String = Nothing
            Return New Microsoft.Extensions.AI.ChatResponseUpdate(Nothing, If(CSharpImpl.__Assign(text, TryCast(executorCompletedEvent.Data, String)) IsNot Nothing, AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateTextContents(text), Nothing)) With {.AuthorName = executorCompletedEvent.ExecutorId, .MessageId = System.Guid.NewGuid().ToString(), .RawRepresentation = executorCompletedEvent}
        End Function

        Private Shared Function CreateMessageUpdate(ByVal message As Microsoft.Extensions.AI.ChatMessage, ByVal workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent) As ChatResponseUpdate
            Return New Microsoft.Extensions.AI.ChatResponseUpdate With {.AuthorName = message.AuthorName, .Contents = If(message.Contents IsNot Nothing AndAlso message.Contents.Count > 0, New System.Collections.Generic.List(Of Microsoft.Extensions.AI.AIContent)(message.Contents), AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateTextContents(message.Text)), .CreatedAt = message.CreatedAt, .MessageId = message.MessageId, .RawRepresentation = workflowEvent, .Role = message.Role}
        End Function

        Private Shared Function CreateContentUpdate(ByVal contents As System.Collections.Generic.IList(Of Microsoft.Extensions.AI.AIContent), ByVal workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent) As ChatResponseUpdate
            Return New Microsoft.Extensions.AI.ChatResponseUpdate With {.Contents = contents, .RawRepresentation = workflowEvent, .Role = Microsoft.Extensions.AI.ChatRole.Assistant}
        End Function

        Private Shared Function CreateTextUpdate(ByVal text As String, ByVal workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent) As ChatResponseUpdate
            Return New Microsoft.Extensions.AI.ChatResponseUpdate With {.Contents = AIDemo.Agents.WorkflowInfra.WorkflowResponseProvider(Of TInput).CreateTextContents(text), .RawRepresentation = workflowEvent, .Role = Microsoft.Extensions.AI.ChatRole.Assistant}
        End Function

        Private Shared Function CreateWorkflowEventUpdate(ByVal workflowEvent As Microsoft.Agents.AI.Workflows.WorkflowEvent) As ChatResponseUpdate
            Return New Microsoft.Extensions.AI.ChatResponseUpdate With {.Contents = New Microsoft.Extensions.AI.AIContent() {New AIDemo.Agents.WorkflowInfra.WorkflowEventContent(workflowEvent)}, .RawRepresentation = workflowEvent, .Role = Microsoft.Extensions.AI.ChatRole.Assistant}
        End Function

        Private Shared Function CreateTextContents(ByVal text As String) As IList(Of Microsoft.Extensions.AI.AIContent)
            If String.IsNullOrEmpty(text) Then Return System.Array.Empty(Of Microsoft.Extensions.AI.AIContent)()
            Return New Microsoft.Extensions.AI.AIContent() {New Microsoft.Extensions.AI.TextContent(text)}
        End Function

        Private Class CSharpImpl

            <System.Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
