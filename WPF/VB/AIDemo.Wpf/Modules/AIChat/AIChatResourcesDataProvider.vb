Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports DevExpress.Data.Utils
Imports DevExpress.Security
Imports DevExpress.AIIntegration.Blazor.Chat
Imports Microsoft.Extensions.AI

Namespace AIDemo

    Friend Module AIChatResourcesDataProvider

        Private ReadOnly resources As Dictionary(Of String, AIChatResource)

        Sub New()
            Dim accessLog = New AIChatResource("access.txt", "Access Log", AddressOf GetLogResource, "text/plain", "Web server access log for the past 24 hours.")
            Dim dashboardImage = New AIChatResource("dashboard.jpg", "Dashboard (Screenshot)", AddressOf GetImageResource, "image/jpeg", "Latest dashboard screenshot for real-time data analysis.")
            Dim chatDocumentation = New AIChatResource("dxaichat.md", "AI Chat Documentation", AddressOf GetDocsResource, "text/markdown", "Reference guide for AI Chat functionality and usage.")
            resources = New Dictionary(Of String, AIChatResource) From {{accessLog.Uri, accessLog}, {dashboardImage.Uri, dashboardImage}, {chatDocumentation.Uri, chatDocumentation}}
        End Sub

        Private Function GetFilePath(ByVal fileName As String) As String
            Dim resource As AIChatResource
            If resources.TryGetValue(fileName, resource) Then
                Dim root = SafeEnvironment.CurrentDirectory
                If String.IsNullOrEmpty(root) Then Throw New InvalidOperationException("Cannot find the current directory.")
                Dim path = IO.Path.Combine(root, "Resources", "AIChat", resource.Uri)
                If Not SafePath.IsRooted(root, path) Then Throw New InvalidOperationException(String.Format("Resource '{0}' path '{1}' is outside the allowed root directory '{2}'.", fileName, path, root))
                Return path
            End If

            Throw New InvalidOperationException(String.Format("Resource '{0}' not found.", fileName))
        End Function

        Private Async Function GetLogResource(ByVal resource As AIChatResource, ByVal cancellationToken As CancellationToken) As Task(Of IList(Of AIContent))
            Dim text = Await File.ReadAllTextAsync(GetFilePath(resource.Uri), cancellationToken)
            Return New List(Of AIContent) From {New TextContent(text)}
        End Function

        Private Async Function GetImageResource(ByVal resource As AIChatResource, ByVal cancellationToken As CancellationToken) As Task(Of ReadOnlyMemory(Of Byte))
            Return Await File.ReadAllBytesAsync(GetFilePath(resource.Uri), cancellationToken)
        End Function

        Private Async Function GetDocsResource(ByVal resource As AIChatResource, ByVal cancellationToken As CancellationToken) As Task(Of String)
            Return Await File.ReadAllTextAsync(GetFilePath(resource.Uri), cancellationToken)
        End Function

        Public Function GetData() As List(Of AIChatResource)
            Return resources.Values.ToList()
        End Function
    End Module
End Namespace
