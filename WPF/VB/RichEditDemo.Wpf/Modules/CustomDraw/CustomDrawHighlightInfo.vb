Imports System.Windows.Media
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo

    Public Enum CustomDrawHighlightType
        Fill
        Outline
    End Enum

    Public Class CustomDrawHighlightInfo

        Private _HighlightRange As FixedRange, _HighlightType As CustomDrawHighlightType, _HighlightColor As Color

        Public Property HighlightRange As FixedRange
            Get
                Return _HighlightRange
            End Get

            Private Set(ByVal value As FixedRange)
                _HighlightRange = value
            End Set
        End Property

        Public Property HighlightType As CustomDrawHighlightType
            Get
                Return _HighlightType
            End Get

            Private Set(ByVal value As CustomDrawHighlightType)
                _HighlightType = value
            End Set
        End Property

        Public Property HighlightColor As Color
            Get
                Return _HighlightColor
            End Get

            Private Set(ByVal value As Color)
                _HighlightColor = value
            End Set
        End Property

        Public Sub New(ByVal highlightedRange As FixedRange, ByVal highlightType As CustomDrawHighlightType, ByVal highlightColor As Color)
            HighlightRange = highlightedRange
            Me.HighlightType = highlightType
            Me.HighlightColor = highlightColor
        End Sub
    End Class
End Namespace
