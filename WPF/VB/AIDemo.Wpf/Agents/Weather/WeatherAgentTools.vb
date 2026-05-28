Imports System
Imports System.ComponentModel
Imports System.Text.Json
Imports System.Threading.Tasks
Imports AIDemo.Services
Imports Microsoft.Extensions.AI

Namespace AIDemo.Agents.Weather

    Public Class WeatherAgentTools

        Private ReadOnly weatherService As WeatherService

        Public Sub New(ByVal weatherService As WeatherService)
            Me.weatherService = weatherService
        End Sub

        Public Function GetWeatherTool() As AIFunction
            Return AIFunctionFactory.Create(CType(AddressOf GetWeather, Func(Of String, Task(Of String))), "GetWeather", "Returns the current weather and 5-day forecast for a specified city as JSON.")
        End Function

        <Description("Gets current weather and 5-day forecast for a specified city. Returns a beautifully formatted weather widget with temperature, humidity, wind speed, and daily forecasts.")>
        Private Async Function GetWeather(<Description("The name of the city to get weather for (e.g., 'London', 'New York', 'Tokyo')")> ByVal cityName As String) As Task(Of String)
            Dim weather = Await weatherService.GetWeatherAsync(cityName).ConfigureAwait(False)
            If weather Is Nothing Then Return $"Sorry, I couldn't fetch weather data for '{cityName}'. Please check the city name and try again."
            Return JsonSerializer.Serialize(weather, New JsonSerializerOptions With {.WriteIndented = True})
        End Function
    End Class
End Namespace
