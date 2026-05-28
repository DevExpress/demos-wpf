Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports DevExpress.Utils

Namespace ChartsDemo

    <XmlRoot("TemperatureData")>
    Public Class TemperatureData
        Inherits List(Of TemperaturesInfo)

        Public Shared Function Load() As TemperatureData
            Return SafeXml.Deserialize(Of TemperatureData)(LoadFromResources("/Data/LondonTemperature.xml"))
        End Function
    End Class

    Public Class TemperaturesInfo

        Public Property Description As String

        Public Property Items As List(Of TemperatureInfo)
    End Class

    Public Class TemperatureInfo

        Public Property [Date] As Date

        Public Property Temperature As Integer
    End Class
End Namespace
