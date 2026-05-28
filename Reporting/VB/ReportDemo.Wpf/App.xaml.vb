Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace WpfDemo

    Public Partial Class App
        Inherits Application

        Shared Sub New()
            Call DemoShellApp.Init()
        End Sub

#If DEBUG
        Public ReadOnly Property IsDebug As Boolean
            Get
                Return True
            End Get
        End Property
#End If
    End Class
End Namespace
