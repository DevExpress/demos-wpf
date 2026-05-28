Imports System
Imports System.Linq
Imports System.Threading.Tasks
Imports AIDemo.Agents
Imports AIDemo.Agents.Weather
Imports Microsoft.Agents.AI
Imports Microsoft.Agents.AI.Hosting
Imports Microsoft.Agents.AI.Hosting.AGUI.AspNetCore
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Hosting.Server
Imports Microsoft.AspNetCore.Hosting.Server.Features
Imports Microsoft.Extensions.DependencyInjection

Namespace AIDemo.Services

    ''' <summary>
    ''' Self-hosts a minimal ASP.NET Core Kestrel server that exposes an AG-UI
    ''' endpoint for the Weather Agent in the WinForms process.
    ''' </summary>
    Public NotInheritable Class AGUIHost
        Implements IAsyncDisposable, IDisposable

        Private app As WebApplication

        Private baseUrlField As String

        Private Sub New()
        End Sub

        Public ReadOnly Property BaseUrl As String
            Get
                Return baseUrlField
            End Get
        End Property

        Public Shared Async Function StartAsync(ByVal agentFactory As AgentFactory, ByVal weatherTools As WeatherAgentTools) As Task(Of AGUIHost)
            Dim host = New AGUIHost()
            Dim builder = WebApplication.CreateBuilder()
            builder.Services.AddAGUI()
            builder.AddAIAgent("WeatherAgent", Function(sp, key) agentFactory.CreateWeatherAgent(weatherTools.GetWeatherTool(), key))
            builder.WebHost.UseUrls("http://127.0.0.1:0")
            host.app = builder.Build()
            host.app.MapAGUI("/api/weather-agent", host.app.Services.GetRequiredKeyedService(Of AIAgent)("WeatherAgent"))
            Await host.app.StartAsync().ConfigureAwait(False)
            Dim server = host.app.Services.GetRequiredService(Of IServer)()
            Dim addressesFeature = server.Features.[Get](Of IServerAddressesFeature)()
            host.baseUrlField = If(addressesFeature?.Addresses.FirstOrDefault()?.TrimEnd("/"c), "http://localhost:5099")
            Return host
        End Function

        Public Function GetWeatherAgentEndpoint() As String
            Return $"{baseUrlField}/api/weather-agent"
        End Function

        Private _disposed As Boolean

        Public Sub Dispose() Implements IDisposable.Dispose
            If _disposed Then Return
            TryCast(app, IDisposable)?.Dispose()
            _disposed = True
        End Sub

        Public Function DisposeAsync() As ValueTask Implements IAsyncDisposable.DisposeAsync
            If _disposed Then Return Nothing
            If app IsNot Nothing Then Return New ValueTask(DisposeAsyncCore())
            _disposed = True
            Return Nothing
        End Function

        Private Async Function DisposeAsyncCore() As Task
            Try
                Await app.DisposeAsync().ConfigureAwait(False)
            Finally
                _disposed = True
            End Try
        End Function
    End Class
End Namespace
