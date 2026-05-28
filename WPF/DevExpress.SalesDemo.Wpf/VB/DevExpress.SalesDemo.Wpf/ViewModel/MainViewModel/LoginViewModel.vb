Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Wpf.Helpers
Imports DevExpress.Xpf.DemoBase.Utils.Security

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class LoginViewModel

        Public Shared Function Create() As LoginViewModel
            Return ViewModelSource.Create(Function() New LoginViewModel())
        End Function

        Public Shared Function Clone(ByVal vm As LoginViewModel) As LoginViewModel
            Dim res = Create()
            res.UserName = vm.UserName
            Return res
        End Function

        Private ReadOnly userNameData As SensitiveData = SensitiveData.CreateForCurrentUser()

        Private ReadOnly passwordData As SensitiveData = SensitiveData.CreateForCurrentUser()

        Protected Sub New()
            userNameData.Text = "John Smith"
            Icon = ResourceImageHelper.GetResourceImage("User.png")
        End Sub

        Public Overridable Property UserName As String
            Get
                Return userNameData.Text
            End Get

            Set(ByVal value As String)
                userNameData.Text = value
            End Set
        End Property

        Public Property Password As String
            Get
                Return passwordData.Text
            End Get

            Set(ByVal value As String)
                passwordData.Text = value
            End Set
        End Property

        Public Overridable Property Icon As BitmapImage
    End Class
End Namespace
