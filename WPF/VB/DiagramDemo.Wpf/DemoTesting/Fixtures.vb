Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace DiagramDemo.Tests

    Public Class DiagramCheckAllDemosFixture
        Inherits CheckAllDemosFixture

    End Class

    Public Class DiagramDemoModulesAccessor
        Inherits DemoModulesAccessor(Of DiagramDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BaseDiagramDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As DiagramDemoModulesAccessor

        Protected Property ModuleAccessor As DiagramDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As DiagramDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As DiagramDemoModulesAccessor
    End Class
End Namespace
