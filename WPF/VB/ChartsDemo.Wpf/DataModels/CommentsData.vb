Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports DevExpress.Utils

Namespace ChartsDemo

    <XmlRoot("Comments")>
    Public Class CommentsData
        Inherits List(Of CommentsInfo)

        Public Shared Function Load() As CommentsData
            Return SafeXml.Deserialize(Of CommentsData)(LoadFromResources("/Data/CommentsData.xml"))
        End Function
    End Class

    Public Class CommentsInfo

        Public Property Category As String

        Public Property Items As List(Of CommentInfo)
    End Class

    Public Class CommentInfo

        Public Property [Date] As Date

        Public Property CommentCount As Integer
    End Class
End Namespace
