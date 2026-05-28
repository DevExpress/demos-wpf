Namespace AIDemo

    Public Partial Class SmartAutoCompleteModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
        End Sub
    End Class

    Public Class UserRoleItem

        Public Property UserRole As String

        Public Property DisplayText As String
    End Class
End Namespace
