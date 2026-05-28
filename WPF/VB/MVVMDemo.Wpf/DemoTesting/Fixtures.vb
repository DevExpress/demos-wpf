Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace MVVMDemo.Tests

    Public Class MVVMCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class

    Public Class MVVMDemoModulesAccessor
        Inherits DemoModulesAccessor(Of MVVMDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseMVVMDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As MVVMDemoModulesAccessor

        Protected Property ModuleAccessor As MVVMDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As MVVMDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As MVVMDemoModulesAccessor
    End Class
End Namespace
