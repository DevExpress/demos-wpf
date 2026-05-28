Namespace AIDemo

    Public Partial Class AIChatResourcesModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            aiChatControl.ChatResources = GetData()
        End Sub
    End Class
End Namespace
