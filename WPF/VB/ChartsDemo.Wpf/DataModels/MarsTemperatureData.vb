Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization
Imports DevExpress.Utils

Namespace ChartsDemo

    <XmlRoot("MarsTemperatureData")>
    Public Class MarsTemperatureData
        Inherits List(Of MarsTemperature)

        Public Shared Function GetFullData() As MarsTemperatureData
            Return SafeXml.Deserialize(Of MarsTemperatureData)(LoadFromResources("/Data/MarsTemperatureData.xml"))
        End Function

        Public Shared Function GetShortData() As List(Of MarsTemperature)
            Dim fullData As MarsTemperatureData = GetFullData()
            Return fullData.Take(31).ToList()
        End Function
    End Class

    Public Class MarsTemperature

        Public Property Sol As Double

        Public Property Temperature As Double
    End Class
End Namespace
