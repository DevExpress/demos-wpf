Imports System
Imports System.Collections.Generic
Imports System.IO
Imports Microsoft.Extensions.AI
Imports DevExpress.AIIntegration.Blazor.Chat
Imports DevExpress.AIIntegration.Blazor.Chat.Internal
Imports DevExpress.AIIntegration.Blazor.Chat.WebView
Imports DevExpress.Xpf.Editors

Namespace AIDemo

    Public Partial Class AIChatFileAttachmentsModule

        Const ImageName As String = "Eiffel-Tower.jpg"

        Const FileIconName As String = "file.svg"

        Private Shared ReadOnly fileExtensions As Dictionary(Of FileAttachmentType, List(Of String)) = New Dictionary(Of FileAttachmentType, List(Of String)) From {{FileAttachmentType.Images, New List(Of String) From {".jpg", ".png", ".jpeg"}}, {FileAttachmentType.Documents, New List(Of String) From {".pdf", ".docx"}}, {FileAttachmentType.Tables, New List(Of String) From {".xlsx", ".csv"}}}

        Private Shared ReadOnly mimeTypes As Dictionary(Of FileAttachmentType, List(Of String)) = New Dictionary(Of FileAttachmentType, List(Of String)) From {{FileAttachmentType.Images, New List(Of String) From {"image/jpeg", "image/png"}}, {FileAttachmentType.Documents, New List(Of String) From {"application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"}}, {FileAttachmentType.Tables, New List(Of String) From {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "text/csv"}}}

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            aiChatControl.MessageTemplate = Function(message) Sub(builder)
                builder.OpenComponent(Of AIChatFileAttachmentsMessageTemplate)(0)
                builder.AddAttribute(1, NameOf(AIChatFileAttachmentsMessageTemplate.Context), message)
                builder.AddAttribute(2, NameOf(AIChatFileAttachmentsMessageTemplate.ImageExtensions), fileExtensions(FileAttachmentType.Images))
                builder.AddAttribute(3, NameOf(AIChatFileAttachmentsMessageTemplate.FileIconBase64), GetFileIconBase64())
                builder.CloseComponent()
            End Sub
            UpdateFileUploadSettings()
        End Sub

        Private Sub OnSelectedFileTypeChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            UpdateFileUploadSettings()
        End Sub

        Private Sub OnAIChatControlInitialized(ByVal sender As Object, ByVal e As AIChatControlInitializedEventArgs)
            aiChatControl.LoadMessages({New BlazorChatMessage(ChatRole.User, "Analyze the image file and summarize its content.", CreateSampleFiles()), New BlazorChatMessage(ChatRole.Assistant, "The image features the Eiffel Tower, a renowned landmark located in Paris, France. The tower is shown from a low angle, emphasizing its height and intricate iron lattice structure. The sky is lit with dramatic clouds and warm hues of sunset, creating a visually striking backdrop for the iconic monument. The overall scene conveys a sense of grandeur and beauty, highlighting the Eiffel Tower as a symbol of romance and architectural achievement.")})
        End Sub

        Private Sub UpdateFileUploadSettings()
            Dim fileType = SelectedFileType
            Dim settings = New DemoFileUploadSettings()
            settings.MaxFileSize = 10000000
            settings.MaxFileCount = 3
            settings.AllowedFileExtensions.AddRange(fileExtensions(fileType))
            settings.FileTypeFilter.AddRange(mimeTypes(fileType))
            aiChatControl.FileUploadSettings = settings
        End Sub

        Private Function CreateSampleFiles() As List(Of AIChatUploadFileInfo)
            Dim files = New List(Of AIChatUploadFileInfo)()
            Try
                Dim path = GetResourcePath(ImageName)
                If Not File.Exists(path) Then Return files
                Dim fileBytes = File.ReadAllBytes(path)
                files.Add(New AIChatUploadFileInfo(ImageName, "image/jpeg", fileBytes.Length, New ReadOnlyMemory(Of Byte)(fileBytes)))
            Catch
            End Try

            Return files
        End Function

        Private ReadOnly Property SelectedFileType As FileAttachmentType
            Get
                If TypeOf selectedFileTypeEdit.EditValue Is FileAttachmentType Then Return CType(selectedFileTypeEdit.EditValue, FileAttachmentType)
                Return FileAttachmentType.Images
            End Get
        End Property

        Private Shared Function GetFileIconBase64() As String
            Try
                Dim path = GetResourcePath(FileIconName)
                If Not File.Exists(path) Then Return String.Empty
                Dim fileBytes = File.ReadAllBytes(path)
                Return "data:image/svg+xml;base64," & Convert.ToBase64String(fileBytes)
            Catch
                Return String.Empty
            End Try
        End Function

        Private Shared Function GetResourcePath(ByVal fileName As String) As String
            Return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "AIChat", fileName)
        End Function
    End Class

    Public Enum FileAttachmentType
        Images
        Documents
        Tables
    End Enum

    Public Class DemoFileUploadSettings
        Implements IFileUploadSettings

        Public Property MaxFileSize As Integer Implements IFileUploadSettings.MaxFileSize

        Public Property MaxFileCount As Integer Implements IFileUploadSettings.MaxFileCount

        Public ReadOnly Property AllowedFileExtensions As List(Of String) = New List(Of String)() Implements IFileUploadSettings.AllowedFileExtensions

        Public ReadOnly Property FileTypeFilter As List(Of String) = New List(Of String)() Implements IFileUploadSettings.FileTypeFilter
    End Class
End Namespace
