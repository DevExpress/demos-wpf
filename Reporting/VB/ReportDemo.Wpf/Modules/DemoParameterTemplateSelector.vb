Imports System.Collections.Generic
Imports System.Windows
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Printing.Parameters
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Parameters.ViewModels

Namespace ReportWpfDemo

    Public Class DemoParameterTemplateSelector
        Inherits ParameterTemplateSelector

        Private templatesField As Dictionary(Of Object, DataTemplate) = New Dictionary(Of Object, DataTemplate)()

        Public ReadOnly Property Templates As Dictionary(Of Object, DataTemplate)
            Get
                Return templatesField
            End Get
        End Property

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim parameter = TryCast(item, ParameterItemViewModel)
            If parameter Is Nothing Then Return Nothing
            If Templates.ContainsKey(parameter.Name) Then Return Templates(parameter.Name)
            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class

    Public Class DemoEditingFieldTemplateSelector
        Inherits EditingFieldTemplateSelector

        Private templatesField As Dictionary(Of Object, DataTemplate) = New Dictionary(Of Object, DataTemplate)()

        Public ReadOnly Property Templates As Dictionary(Of Object, DataTemplate)
            Get
                Return templatesField
            End Get
        End Property

        Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As DependencyObject) As DataTemplate
            Dim textEditingField = TryCast(item, TextEditingField)
            If textEditingField IsNot Nothing AndAlso Templates.ContainsKey(textEditingField.EditorName) Then Return Templates(textEditingField.EditorName)
            Return MyBase.SelectTemplate(item, container)
        End Function
    End Class
End Namespace
