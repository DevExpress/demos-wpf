Imports System
Imports System.Collections.Generic
Imports DevExpress.AIIntegration.Blazor.Chat
Imports Microsoft.AspNetCore.Components

Namespace AIDemo

    Public Partial Class AIChatFileAttachmentsMessageTemplate
        Inherits ComponentBase

        <Parameter>
        Public Property Context As BlazorChatMessage

        <Parameter>
        Public Property ImageExtensions As List(Of String)

        <Parameter>
        Public Property FileIconBase64 As String

        Private Function GetImageSrc(ByVal file As AIChatUploadFileInfo) As String
            Dim base64 = Convert.ToBase64String(file.Data.ToArray())
            Return String.Format("data:{0};base64,{1}", file.Type, base64)
        End Function

        Private Function GetMessageClasses(ByVal message As BlazorChatMessage) As String
            If message.Role = ChatMessageRole.User Then Return "dxbl-chatui-message dxbl-chatui-message-user"
            If message.Role = ChatMessageRole.System Then Return "dxbl-chatui-message dxbl-chatui-message-system"
            If message.Role = ChatMessageRole.Assistant Then Return "dxbl-chatui-message dxbl-chatui-message-assistant"
            If message.Role = ChatMessageRole.Tool Then Return "dxbl-chatui-message dxbl-chatui-message-assistant"
            If message.Role = ChatMessageRole.Error Then Return "dxbl-chatui-message dxbl-chatui-message-error"
            Return "dxbl-chatui-message"
        End Function
    End Class
End Namespace
