Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Data

Namespace GridDemo

    Public Structure Range

        Public Property Text As String

        Public Property Min As Integer

        Public Property Max As Integer

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Structure

    Public Class ProductIdToProductNameConverter
        Implements IValueConverter

        Private Shared productsField As Dictionary(Of Long, Product)

        Private Shared ReadOnly Property Products As Dictionary(Of Long, Product)
            Get
                Return If(productsField, Function()
                    productsField = NWindContext.Create().Products.ToDictionary(Function(p) p.ProductID)
                    Return productsField
                End Function())
            End Get
        End Property

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim id As Long = CLng(value)
            Dim product As Product = Nothing
            Products.TryGetValue(id, product)
            Return If(product?.ProductName, String.Empty)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
