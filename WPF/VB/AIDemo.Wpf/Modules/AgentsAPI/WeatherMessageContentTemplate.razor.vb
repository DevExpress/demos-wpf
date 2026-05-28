Imports System.Collections.Generic
Imports System.Text.Json
Imports Microsoft.AspNetCore.Components
Imports DevExpress.AIIntegration.Blazor.Chat
Imports AIDemo.Models

Namespace AIDemo

    Public Partial Class WeatherMessageContentTemplate
        Inherits ComponentBase

        <Parameter>
        Public Property Message As BlazorChatMessage

        Private Shared ReadOnly JsonOptions As JsonSerializerOptions = New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True}

        Private Function ToHtml(ByVal markdown As String) As MarkupString
            Dim html As String = Markdig.Markdown.ToHtml(markdown)
            Return New MarkupString(html)
        End Function

        Private Function GetWeatherFromFunctionCalls(ByVal functionCalls As List(Of AIChatFunctionCall)) As WeatherWidgetData
            If functionCalls Is Nothing OrElse functionCalls.Count = 0 Then Return Nothing
            Dim weatherCall = functionCalls.Find(Function(fc) fc.Request IsNot Nothing AndAlso Equals(fc.Request.Name, "GetWeather") AndAlso fc.Result IsNot Nothing AndAlso fc.Result.Result IsNot Nothing)
            If weatherCall Is Nothing Then Return Nothing
            Try
                Dim json = weatherCall.Result.Result.ToString()
                If String.IsNullOrWhiteSpace(json) Then Return Nothing
                Dim data = JsonSerializer.Deserialize(Of WeatherWidgetData)(json, JsonOptions)
                Return If(data IsNot Nothing AndAlso Not Equals(data.CityName, Nothing), data, Nothing)
            Catch
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
