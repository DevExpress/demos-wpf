Imports System
Imports System.Windows.Markup
Imports System.Windows.Data
Imports System.Windows.Media
Imports DevExpress.Xpf.Editors
Imports System.Collections.Generic
Imports GridDemo
Imports DevExpress.Xpf.Core
Imports System.Globalization
Imports DevExpress.Data.Internal

Namespace EditorsDemo.Utils

    Public Class FormatWrapper

        Public Sub New()
        End Sub

        Public Sub New(ByVal name As String, ByVal format As String)
            FormatName = name
            FormatString = format
        End Sub

        Public Property FormatName As String

        Public Property FormatString As String
    End Class

    Public Class BaseKindHelper(Of T)

        Public Function GetEnumMemberList() As Array
            Return [Enum].GetValues(GetType(T))
        End Function
    End Class

    Public Module ImageHelper

        Public Function GetSvgImage(ByVal imageName As String) As ImageSource
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri($"pack://application:,,,/EditorsDemo;component/Images/{imageName}.svg"), .Size = New System.Windows.Size(16, 16)}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Module

    Public Class NullableToStringConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function

        Const NullString As String = "Null"

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(value Is Nothing, NullString, value.ToString())
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
#End Region
    End Class

    Public Class DecimalToConverter
        Inherits MarkupExtension
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = TryCast(parameter, Type)
            Return If(target Is Nothing, value, System.Convert.ChangeType(value, target, culture))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return System.Convert.ToDecimal(value)
        End Function

        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Return Me
        End Function
    End Class

    Public Class IConvertibleConverter
        Implements IValueConverter

        Public Property ToType As String

        Public Property FromType As String

#Region "IValueConverter Members"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim target As Type = SafeTypeResolver.GetType(ToType)
            Return If(target Is Nothing, value, System.Convert.ChangeType(value, target, culture))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim target As Type = SafeTypeResolver.GetType(FromType)
            Return If(target Is Nothing, value, System.Convert.ChangeType(value, target, culture))
        End Function
#End Region
    End Class

    Public Class CategoryDataToImageSourceConverter
        Inherits BytesToImageSourceConverter

        Private Shared ReadOnly cachedImages As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        Public Overrides Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object
            Dim categoryData As CategoryData = TryCast(value, CategoryData)
            If value Is Nothing OrElse categoryData Is Nothing Then Return Nothing
            Dim val As Object
            If cachedImages.TryGetValue(categoryData.Name, val) Then Return val
            Dim image As Object = MyBase.Convert(categoryData.Picture, targetType, parameter, culture)
            cachedImages.Add(categoryData.Name, image)
            Return image
        End Function
    End Class

    Public Class CategoryNameToImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim category = CStr(value)
            If String.IsNullOrEmpty(category) Then Return Nothing
            category = category.Replace(" ", "").Replace("/", "")
            Return GetSvgImage("Products/" & category)
        End Function

        Public Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
