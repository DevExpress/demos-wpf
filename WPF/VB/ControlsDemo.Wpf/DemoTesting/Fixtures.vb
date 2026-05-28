Imports ControlsDemo.Modules
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports System
Imports System.Linq

Namespace ControlsDemo.Tests

    Public Class ControlsCheckAllDemosFixture
        Inherits CheckAllDemosFixture

#If NET
        Private skipMemoryLeaksCheckModules As Type() = New Type() {GetType(BreadcrumbSamplesModule)}

#Else
        Type[] skipMemoryLeaksCheckModules = new Type[] { typeof(BreadcrumbSamplesModule), typeof(TaskbarServices) };
#End If
        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return Not skipMemoryLeaksCheckModules.Contains(moduleType)
        End Function
    End Class
End Namespace
