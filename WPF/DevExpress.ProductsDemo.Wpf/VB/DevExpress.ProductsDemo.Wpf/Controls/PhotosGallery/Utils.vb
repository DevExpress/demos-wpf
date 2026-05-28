Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Xml.Linq
Imports DevExpress.Utils

Namespace ProductsDemo

    Public Class DemoValuesProvider

        Public Const DevExpressAzureKey As String = "CDOhtwNBSsbmBiN3rUEjmBJGHW2tRMbp5XVwu4J55VBZg8PdRe9MJQQJ99BEACYeBjFllM6LAAAgAZMP1cA4"
    End Class

    Public Module DataLoader

        Public Function LoadXmlFromResources(ByVal fileName As String) As XDocument
            Try
                fileName = "/DevExpress.ProductsDemo.Wpf;component" & fileName
                Dim uri As Uri = New Uri(fileName, UriKind.RelativeOrAbsolute)
                Dim info As StreamResourceInfo = Application.GetResourceStream(uri)
                Return SafeXml.CreateXDocument(info.Stream)
            Catch
                Return Nothing
            End Try
        End Function

        Public Function LoadXmlFromFile(ByVal fileName As String) As XDocument
            Try
                Dim xmlContent As String = File.ReadAllText(fileName)
                Return SafeXml.CreateXDocument(xmlContent)
            Catch
                Return Nothing
            End Try
        End Function
    End Module

    Public Class PhotoGalleryResources

        Public ReadOnly Property CityInformationControlSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property LabelControlImageSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property PlaceInfoControlPrevImageSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/PrevPlace.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property PlaceInfoControlNextImageSource As BitmapImage
            Get
                Return New BitmapImage(New Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/NextPlace.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public Sub New()
        End Sub
    End Class
End Namespace
