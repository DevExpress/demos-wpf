Imports System
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports AIDemo.Models
Imports DevExpress.Data.Utils

Namespace AIDemo.Services

    Public Class WeatherService

        Private Shared ReadOnly Descriptions As String() = {"clear sky", "few clouds", "scattered clouds", "broken clouds", "shower rain", "rain", "thunderstorm", "snow", "mist"}

        Private Shared ReadOnly Icons As String() = {"01d", "02d", "03d", "04d", "09d", "10d", "11d", "13d", "50d"}

        Private Shared ReadOnly Countries As String() = {"GB", "US", "JP", "FR", "DE", "AU", "CA", "IT", "ES", "BR"}

        Public Function GetWeatherAsync(ByVal cityName As String) As Task(Of AIDemo.Models.WeatherWidgetData)
            Dim rng = DevExpress.Data.Utils.NonCryptographicRandom.System
            Dim descIndex As Integer = rng.[Next](AIDemo.Services.WeatherService.Descriptions.Length)
            Dim temp As Double = System.Math.Round(rng.NextDouble() * 40 - 5, 1)
            Dim now = System.DateTime.Now
            Dim forecast = New System.Collections.Generic.List(Of AIDemo.Models.DailyForecast)()
            For i As Integer = 1 To 5
                Dim fi As Integer = rng.[Next](AIDemo.Services.WeatherService.Descriptions.Length)
                Dim hi As Double = System.Math.Round(temp + rng.NextDouble() * 8, 1)
                Dim lo As Double = System.Math.Round(temp - rng.NextDouble() * 8, 1)
                forecast.Add(New AIDemo.Models.DailyForecast With {.[Date] = now.AddDays(i), .MaxTemp = hi, .MinTemp = lo, .Description = AIDemo.Services.WeatherService.Descriptions(fi), .Icon = AIDemo.Services.WeatherService.Icons(fi), .Humidity = rng.[Next](30, 95)})
            Next

            Dim result = New AIDemo.Models.WeatherWidgetData With {.CityName = cityName, .Country = AIDemo.Services.WeatherService.Countries(rng.[Next](AIDemo.Services.WeatherService.Countries.Length)), .Temperature = temp, .FeelsLike = System.Math.Round(temp + rng.NextDouble() * 4 - 2, 1), .Description = AIDemo.Services.WeatherService.Descriptions(descIndex), .Icon = AIDemo.Services.WeatherService.Icons(descIndex), .Humidity = rng.[Next](30, 95), .WindSpeed = System.Math.Round(rng.NextDouble() * 15, 1), .Pressure = rng.[Next](990, 1040), .Sunrise = now.[Date].AddHours(5 + rng.NextDouble() * 2), .Sunset = now.[Date].AddHours(18 + rng.NextDouble() * 2), .FiveDayForecast = forecast}
            Return System.Threading.Tasks.Task.FromResult(result)
        End Function
    End Class
End Namespace
