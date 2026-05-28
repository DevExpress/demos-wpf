Imports DevExpress.Xpf.Core
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Data.Utils

Namespace CommonDemo.Helpers

    Public Module ImagesHelper

        Private _LondonPhotos As IEnumerable(Of System.Windows.Media.ImageSource), _NaturePhotos As IEnumerable(Of System.Windows.Media.ImageSource), _GroupIcon As ImageSource

        Public Property LondonPhotos As IEnumerable(Of ImageSource)
            Get
                Return _LondonPhotos
            End Get

            Private Set(ByVal value As IEnumerable(Of ImageSource))
                _LondonPhotos = value
            End Set
        End Property

        Public ReadOnly Property LondonPhoto1 As ImageSource
            Get
                Return LondonPhotos.ElementAt(0)
            End Get
        End Property

        Public ReadOnly Property LondonPhoto2 As ImageSource
            Get
                Return LondonPhotos.ElementAt(1)
            End Get
        End Property

        Public ReadOnly Property LondonPhoto3 As ImageSource
            Get
                Return LondonPhotos.ElementAt(2)
            End Get
        End Property

        Public ReadOnly Property LondonPhoto4 As ImageSource
            Get
                Return LondonPhotos.ElementAt(3)
            End Get
        End Property

        Public Function GetRandomLondonPhoto() As ImageSource
            Return LondonPhotos.ElementAt(NonCryptographicRandom.Default.Next(LondonPhotos.Count() - 1))
        End Function

        Public Property NaturePhotos As IEnumerable(Of ImageSource)
            Get
                Return _NaturePhotos
            End Get

            Private Set(ByVal value As IEnumerable(Of ImageSource))
                _NaturePhotos = value
            End Set
        End Property

        Public ReadOnly Property NaturePhoto1 As ImageSource
            Get
                Return NaturePhotos.ElementAt(0)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto2 As ImageSource
            Get
                Return NaturePhotos.ElementAt(1)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto3 As ImageSource
            Get
                Return NaturePhotos.ElementAt(2)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto4 As ImageSource
            Get
                Return NaturePhotos.ElementAt(3)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto5 As ImageSource
            Get
                Return NaturePhotos.ElementAt(4)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto6 As ImageSource
            Get
                Return NaturePhotos.ElementAt(5)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto7 As ImageSource
            Get
                Return NaturePhotos.ElementAt(6)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto8 As ImageSource
            Get
                Return NaturePhotos.ElementAt(7)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto9 As ImageSource
            Get
                Return NaturePhotos.ElementAt(8)
            End Get
        End Property

        Public ReadOnly Property NaturePhoto10 As ImageSource
            Get
                Return NaturePhotos.ElementAt(9)
            End Get
        End Property

        Public Function GetRandomNaturePhoto() As ImageSource
            Return NaturePhotos.ElementAt(NonCryptographicRandom.Default.Next(NaturePhotos.Count() - 1))
        End Function

        Public Property GroupIcon As ImageSource
            Get
                Return _GroupIcon
            End Get

            Private Set(ByVal value As ImageSource)
                _GroupIcon = value
            End Set
        End Property

        Public Function GetWebIcon(ByVal icon As String) As ImageSource
            Return New BitmapImage(New Uri("/ControlsDemo;component/Images/TabControl/" & icon, UriKind.Relative))
        End Function

        Public Function GetSvgImage(ByVal imagePath As String, ByVal imageSize As Size) As ImageSource
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(imagePath), .Size = imageSize}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function

        Sub New()
            Dim londonUriPath As String = "/ControlsDemo;component/Images/Photos/London/"
            Dim getLondonImage As Func(Of String, ImageSource) = Function(x) New BitmapImage(New Uri(londonUriPath & x, UriKind.Relative))
            Dim londonImages = New List(Of ImageSource) From {getLondonImage("(London)-St-Paul's-Cathedral.jpg"), getLondonImage("(London)-Tower-Bridge.jpg"), getLondonImage("(London)-Tower-of-London.jpg"), getLondonImage("(London)-Trafalgar-Square.jpg"), getLondonImage("(London)-Westminster-Abbey.jpg")}
            LondonPhotos = londonImages
            Dim natureUriPath As String = "/ControlsDemo;component/Images/Photos/Nature/"
            Dim getNatureImage As Func(Of String, ImageSource) = Function(x) New BitmapImage(New Uri(natureUriPath & x, UriKind.Relative))
            Dim naturePhotos = New List(Of ImageSource) From {getNatureImage("01.JPG"), getNatureImage("02.JPG"), getNatureImage("03.JPG"), getNatureImage("04.JPG"), getNatureImage("05.JPG"), getNatureImage("06.JPG"), getNatureImage("07.JPG"), getNatureImage("08.JPG"), getNatureImage("09.JPG"), getNatureImage("10.JPG")}
            ImagesHelper.NaturePhotos = naturePhotos
            GroupIcon = GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TabControl/GroupIcon.svg", New Size(24, 24))
        End Sub
    'static readonly Random rnd = new Random();
    End Module
End Namespace
