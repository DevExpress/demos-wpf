Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization
Imports DevExpress.Utils

Namespace ChartsDemo

    <XmlRoot("OilPrices")>
    Public Class OilPrices
        Inherits List(Of OilPricesByCompany)

        Public Shared Function Load() As OilPrices
            Return SafeXml.Deserialize(Of OilPrices)(LoadFromResources("/Data/OilPrices.xml"))
        End Function

        Public Shared Function GetEuropeBrentPrices() As List(Of OilPriceByDate)
            Dim prices As OilPrices = Load()
            Return Enumerable.First(prices, Function(x) Equals(x.CompanyName, "Europe Brent")).Prices
        End Function
    End Class

    Public Class OilPricesByCompany

        Public Property CompanyName As String

        Public Property Prices As List(Of OilPriceByDate)
    End Class

    Public Class OilPriceByDate

        Public Property Timestamp As Date

        Public Property MinValue As Double

        Public Property MaxValue As Double
    End Class
End Namespace
