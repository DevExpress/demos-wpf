Imports System
Imports System.ClientModel.Primitives
Imports System.Linq
Imports System.Net.Http
Imports System.Threading
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace ReportWpfDemo.AIOperations

    Friend Class PromoteHttpStatusErrorsPipelineTransport
        Inherits HttpClientPipelineTransport

        Protected Overrides Function CreateMessageCore() As PipelineMessage
            Dim message = MyBase.CreateMessageCore()
            Return New DemoPipelineMessage(message.Request)
        End Function

        Protected Overrides Sub OnReceivedResponse(ByVal message As PipelineMessage, ByVal httpResponse As HttpResponseMessage)
            If Not httpResponse.IsSuccessStatusCode Then
                If CInt(httpResponse.StatusCode) = 429 Then
                    Dim secondsString As String = httpResponse.Headers.GetValues("Retry-After").FirstOrDefault()
                    Dim seconds As Integer = Integer.Parse(secondsString)
                    Dim text As String = $"Our demo AI Service reached a temporary request limit. Your operation will resume automatically in {secondsString} seconds. You can press Cancel to abort the operation."
                    Dim activeWindow = ActiveWindowHelper.GetActiveWindow()
                    activeWindow.Dispatcher.Invoke(Sub()
                        Dim result = ThemedMessageBox.Show("AI Service Request Limit", text, MessageBoxButton.OKCancel, MessageBoxResult.OK, New ThemedMessageBoxParameters(MessageBoxImage.Warning) With {.Owner = activeWindow, .TimerTimeout = TimeSpan.FromSeconds(seconds)})
                        If result = MessageBoxResult.Cancel Then TryCast(message, DemoPipelineMessage)?.Cancel()
                    End Sub)
                Else
                    Throw New HttpRequestException("HTTP request failed with status code: " & httpResponse.StatusCode)
                End If
            End If

            MyBase.OnReceivedResponse(message, httpResponse)
        End Sub

        Private Class DemoPipelineMessage
            Inherits PipelineMessage

            Private ReadOnly cts As CancellationTokenSource = New CancellationTokenSource()

            Public Sub New(ByVal request As PipelineRequest)
                MyBase.New(request)
                CancellationToken = cts.Token
            End Sub

            Public Sub Cancel()
                cts.Cancel()
            End Sub
        End Class
    End Class
End Namespace
