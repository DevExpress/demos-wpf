Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace AIDemo

    Public Partial Class App
        Inherits Application

        Shared Sub New()
            DemoShellApp.Init(enablePGO:=False)
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
