Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports DevExpress.Utils

Namespace ChartsDemo

    <XmlRoot("WindRoseData")>
    Public Class WindRoseData
        Inherits List(Of WindInfo)

        Public Shared Function Load() As WindRoseData
            Return SafeXml.Deserialize(Of WindRoseData)(LoadFromResources("/Data/WindRoseData.xml"))
        End Function
    End Class

    Public Class WindInfo

        Public Property Direction As String

        Public Property Speed As Integer
    End Class
End Namespace
