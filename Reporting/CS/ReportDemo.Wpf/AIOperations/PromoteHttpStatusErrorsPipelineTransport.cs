using System;
using System.ClientModel.Primitives;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;

namespace ReportWpfDemo.AIOperations {
    class PromoteHttpStatusErrorsPipelineTransport : HttpClientPipelineTransport {
        protected override PipelineMessage CreateMessageCore() {
            var message = base.CreateMessageCore();
            return new DemoPipelineMessage(message.Request);
        }
        protected override void OnReceivedResponse(PipelineMessage message, HttpResponseMessage httpResponse) {
            if(!httpResponse.IsSuccessStatusCode) {
                if((int)httpResponse.StatusCode == 429) {
                    string secondsString = httpResponse.Headers.GetValues("Retry-After").FirstOrDefault();
                    int seconds = int.Parse(secondsString);
                    string text = $"Our demo AI Service reached a temporary request limit. Your operation will resume automatically in {secondsString} seconds. You can press Cancel to abort the operation.";
                    var activeWindow = ActiveWindowHelper.GetActiveWindow();
                    activeWindow.Dispatcher.Invoke(() => { 
                        var result = ThemedMessageBox.Show("AI Service Request Limit", text, MessageBoxButton.OKCancel, MessageBoxResult.OK, new ThemedMessageBoxParameters(MessageBoxImage.Warning) { Owner = activeWindow, TimerTimeout = TimeSpan.FromSeconds(seconds) });
                        if(result == MessageBoxResult.Cancel)
                            (message as DemoPipelineMessage)?.Cancel();
                    });
                }
                else {
                    throw new HttpRequestException("HTTP request failed with status code: " + httpResponse.StatusCode);
                }
            }
            base.OnReceivedResponse(message, httpResponse);
        }

        class DemoPipelineMessage : PipelineMessage {
            readonly CancellationTokenSource cts = new CancellationTokenSource();
            public DemoPipelineMessage(PipelineRequest request) : base(request) {
                CancellationToken = cts.Token;
            }
            public void Cancel() => cts.Cancel();
        }
    }
}
