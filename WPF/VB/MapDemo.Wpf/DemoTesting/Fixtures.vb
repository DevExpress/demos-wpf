Imports System
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace MapDemo.Tests

    Public Class MapCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToTheTheme(ByVal moduleType As Type, ByVal theme As ITheme) As Boolean
            Return False
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return False
        End Function
    End Class
End Namespace
