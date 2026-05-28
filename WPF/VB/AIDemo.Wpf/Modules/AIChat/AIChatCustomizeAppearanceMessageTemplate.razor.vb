Imports DevExpress.AIIntegration.Blazor.Chat
Imports Microsoft.AspNetCore.Components

Namespace AIDemo

    Public Partial Class AIChatCustomizeAppearanceMessageTemplate
        Inherits ComponentBase

        <Parameter>
        Public Property Context As BlazorChatMessage

        Private Function GetMessageClasses(ByVal message As BlazorChatMessage) As String
            If message.Role = ChatMessageRole.Assistant Then Return "demo-chat-message demo-assistant-message"
            If message.Role = ChatMessageRole.User Then Return "demo-chat-message demo-user-message"
            If message.Role = ChatMessageRole.Error Then Return "demo-chat-message demo-error-message"
            Return "demo-chat-message"
        End Function
    End Class
End Namespace
