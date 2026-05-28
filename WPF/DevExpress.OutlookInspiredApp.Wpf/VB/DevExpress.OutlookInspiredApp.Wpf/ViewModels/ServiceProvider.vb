Imports System
Imports System.Windows.Markup
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.Native
Imports Microsoft.Extensions.DependencyInjection

Namespace DevExpress.DevAV

    Public Class ServiceLocator

        Public Shared ReadOnly Instance As IServiceProvider

        Shared Sub New()
            Dim services = New ServiceCollection().AddSingleton(Of ProductCollectionViewModel)(Function(x) CType(ViewModelSourceHelper.Create(GetType(ProductCollectionViewModel)), ProductCollectionViewModel)).AddSingleton(Of CustomerCollectionViewModel)(Function(x) CType(ViewModelSourceHelper.Create(GetType(CustomerCollectionViewModel)), CustomerCollectionViewModel)).AddSingleton(Of OrderCollectionViewModel)(Function(x) CType(ViewModelSourceHelper.Create(GetType(OrderCollectionViewModel)), OrderCollectionViewModel)).AddSingleton(Of EmployeeCollectionViewModel)(Function(x) CType(ViewModelSourceHelper.Create(GetType(EmployeeCollectionViewModel)), EmployeeCollectionViewModel))
            Instance = services.BuildServiceProvider()
        End Sub
    End Class

    Public Class ViewModelProviderExtension
        Inherits MarkupExtension

        Public Property Type As Type

        Public Sub New()
        End Sub

        Public Sub New(ByVal type As Type)
            Me.Type = type
        End Sub

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return If(Type IsNot Nothing, ServiceLocator.Instance.GetService(Type), Nothing)
        End Function
    End Class
End Namespace
