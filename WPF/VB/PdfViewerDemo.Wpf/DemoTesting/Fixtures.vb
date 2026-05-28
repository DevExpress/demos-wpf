Imports System
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace PdfViewerDemo.Tests

    'PdfViewerDemo.Tests.PdfViewerCheckAllDemosFixture
    Public Class PdfViewerCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function CheckMemoryLeaksWhenFinishingTesting() As Boolean
            Return False
        End Function

        Protected Overrides Function AllowSwitchToCodeTab(ByVal moduleType As Type) As Boolean
            Return False ' clickonce workaround
        End Function
    End Class

    Public Class PdfViewerDemoModulesAccessor
        Inherits DemoModulesAccessor(Of PdfViewerDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub
    End Class

    Public MustInherit Class BasePdfViewerDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private _ModuleAccessor As PdfViewerDemoModulesAccessor

        Protected Property ModuleAccessor As PdfViewerDemoModulesAccessor
            Get
                Return _ModuleAccessor
            End Get

            Private Set(ByVal value As PdfViewerDemoModulesAccessor)
                _ModuleAccessor = value
            End Set
        End Property

        Public Sub New()
            ModuleAccessor = GetModulesAccessor()
        End Sub

        Protected MustOverride Function GetModulesAccessor() As PdfViewerDemoModulesAccessor
    End Class
End Namespace
