Imports System.Collections.Generic
Imports System.Text.Json.Serialization

Namespace AIDemo.Models

    Public Class WeatherWidgetData

        Public Property CityName As String

        Public Property Country As String

        Public Property Temperature As Double

        Public Property FeelsLike As Double

        Public Property Description As String

        Public Property Icon As String

        Public Property Humidity As Integer

        Public Property WindSpeed As Double

        Public Property Pressure As Integer

        Public Property Sunrise As Date

        Public Property Sunset As Date

        Public Property FiveDayForecast As List(Of DailyForecast) = New List(Of DailyForecast)()
    End Class

    Public Class DailyForecast

        Public Property [Date] As Date

        Public Property MaxTemp As Double

        Public Property MinTemp As Double

        Public Property Description As String

        Public Property Icon As String

        Public Property Humidity As Integer
    End Class

    Public Class WeatherData

        <JsonPropertyName("coord")>
        Public Property Coordinates As Coordinates

        <JsonPropertyName("weather")>
        Public Property Weather As List(Of WeatherInfo)

        <JsonPropertyName("main")>
        Public Property Main As MainWeatherData

        <JsonPropertyName("wind")>
        Public Property Wind As WindData

        <JsonPropertyName("clouds")>
        Public Property Clouds As CloudsData

        <JsonPropertyName("dt")>
        Public Property Timestamp As Long

        <JsonPropertyName("sys")>
        Public Property Sys As SysData

        <JsonPropertyName("timezone")>
        Public Property Timezone As Integer

        <JsonPropertyName("name")>
        Public Property CityName As String
    End Class

    Public Class Coordinates

        <JsonPropertyName("lon")>
        Public Property Longitude As Double

        <JsonPropertyName("lat")>
        Public Property Latitude As Double
    End Class

    Public Class WeatherInfo

        <JsonPropertyName("id")>
        Public Property Id As Integer

        <JsonPropertyName("main")>
        Public Property Main As String

        <JsonPropertyName("description")>
        Public Property Description As String

        <JsonPropertyName("icon")>
        Public Property Icon As String
    End Class

    Public Class MainWeatherData

        <JsonPropertyName("temp")>
        Public Property Temperature As Double

        <JsonPropertyName("feels_like")>
        Public Property FeelsLike As Double

        <JsonPropertyName("temp_min")>
        Public Property MinTemperature As Double

        <JsonPropertyName("temp_max")>
        Public Property MaxTemperature As Double

        <JsonPropertyName("pressure")>
        Public Property Pressure As Integer

        <JsonPropertyName("humidity")>
        Public Property Humidity As Integer
    End Class

    Public Class WindData

        <JsonPropertyName("speed")>
        Public Property Speed As Double

        <JsonPropertyName("deg")>
        Public Property Degree As Integer
    End Class

    Public Class CloudsData

        <JsonPropertyName("all")>
        Public Property All As Integer
    End Class

    Public Class SysData

        <JsonPropertyName("country")>
        Public Property Country As String

        <JsonPropertyName("sunrise")>
        Public Property Sunrise As Long

        <JsonPropertyName("sunset")>
        Public Property Sunset As Long
    End Class

    Public Class ForecastData

        <JsonPropertyName("list")>
        Public Property List As List(Of ForecastItem)

        <JsonPropertyName("city")>
        Public Property City As CityData
    End Class

    Public Class ForecastItem

        <JsonPropertyName("dt")>
        Public Property Timestamp As Long

        <JsonPropertyName("main")>
        Public Property Main As MainWeatherData

        <JsonPropertyName("weather")>
        Public Property Weather As List(Of WeatherInfo)

        <JsonPropertyName("clouds")>
        Public Property Clouds As CloudsData

        <JsonPropertyName("wind")>
        Public Property Wind As WindData

        <JsonPropertyName("dt_txt")>
        Public Property DateTimeText As String
    End Class

    Public Class CityData

        <JsonPropertyName("name")>
        Public Property Name As String

        <JsonPropertyName("country")>
        Public Property Country As String

        <JsonPropertyName("timezone")>
        Public Property Timezone As Integer

        <JsonPropertyName("sunrise")>
        Public Property Sunrise As Long

        <JsonPropertyName("sunset")>
        Public Property Sunset As Long
    End Class
End Namespace
