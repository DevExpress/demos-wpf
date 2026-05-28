Imports System
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace TreeMapDemo.Tests

    Public Class TreeMapCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As ITheme) As Boolean
            Return False
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return False 'TODO True
        End Function
    End Class
End Namespace
