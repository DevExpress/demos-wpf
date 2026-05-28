Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports System.Linq
Imports System.Windows.Controls

Namespace BarsDemo.Tests

    ' command line params
    ' BarsDemo.Tests.BarsCheckAllDemosFixture - MemoryLeaks
    Public Class BarsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Private skipMemoryLeaksCheckModules As Type() = New Type() {}

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
    End Class

    Public Class BarsDemoModulesAccessor
        Inherits DemoModulesAccessor(Of BarsDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub

        Public ReadOnly Property Manager As BarManager
            Get
                Return DemoModule.Manager
            End Get
        End Property
    End Class
End Namespace
