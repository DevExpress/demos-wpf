Imports System
Imports DevExpress.Xpf.DemoBase.DemoTesting

Namespace ReportWpfDemo.Tests

    Public Class ReportsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function AllowSwitchToCodeTab(ByVal moduleType As Type) As Boolean
            Return False
        End Function

        Protected Overrides Function SwitchAllThemes(ByVal moduleType As Type) As Boolean
            Return False
        End Function

        Protected Overrides Function TestingThemesCount() As Integer
            Return 5
        End Function

        Protected Overrides Function CanRunModule(ByVal moduleType As Type) As Boolean
            If moduleType Is GetType(LargeQuantityOfData) Then Return False
            Return MyBase.CanRunModule(moduleType)
        End Function
    End Class
End Namespace
