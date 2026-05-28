Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports ControlsDemo.Helpers
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core

Namespace ControlsDemo.TabControl.ColorizedTabs

    Public Class MainViewModel

        Public Shared Function Create() As MainViewModel
            Return ViewModelSource.Create(Function() New MainViewModel())
        End Function

        Protected Sub New()
            If IsInDesignMode() Then Return
            InitDataSource()
        End Sub

        Public Overridable Property Employees As List(Of EmployeeWrapper)

        Private Sub InitDataSource()
            Employees = NWindContext.Create().Employees.ToList().[Select](Function(x) EmployeeWrapper.Create(x)).ToList()
            Dim i As Integer = 0
            For Each employee In Employees
                employee.AccentColor = ColorPalette.GetColor(i, 100)
                employee.AccentColor = ColorHelper.Brightness(employee.AccentColor, 0.3)
                employee.AccentColorOpacity = 180
                i += 1
            Next
        End Sub
    End Class

    Public Class EmployeeWrapper

        Private _Employee As Employee

        Public Shared Function Create(ByVal employee As Employee) As EmployeeWrapper
            Return ViewModelSource.Create(Function() New EmployeeWrapper(employee))
        End Function

        Protected Sub New(ByVal employee As Employee)
            Me.Employee = employee
        End Sub

        Public Property Employee As Employee
            Get
                Return _Employee
            End Get

            Private Set(ByVal value As Employee)
                _Employee = value
            End Set
        End Property

        <BindableProperty(OnPropertyChangedMethodName:="UpdateColors")>
        Public Overridable Property AccentColor As Color

        <BindableProperty(OnPropertyChangedMethodName:="UpdateColors")>
        Public Overridable Property AccentColorOpacity As Byte

        Protected Sub UpdateColors()
            Dim color = AccentColor
            color.A = AccentColorOpacity
            AccentColor = color
        End Sub
    End Class
End Namespace
