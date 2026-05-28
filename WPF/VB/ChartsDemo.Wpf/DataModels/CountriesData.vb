Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports DevExpress.Utils

Namespace ChartsDemo

    <XmlRoot("CountriesInfo")>
    Public Class CountriesInfo
        Inherits List(Of CountryStatisticInfo)

        Public Shared Function Load() As CountriesInfo
            Return SafeXml.Deserialize(Of CountriesInfo)(LoadFromResources("/Data/Top10LargestCountriesInfo.xml"))
        End Function
    End Class

    <XmlType("CountryInfo")>
    Public Class CountryStatisticInfo

        Public Property Name As String

        <XmlArray("Statistic")>
        <XmlArrayItem("PopulationStatisticByYear")>
        Public Property PopulationDynamic As List(Of PopulationStatisticByYear)

        Public Property AreaSqrKilometers As Double

        Public ReadOnly Property AreaMSqrKilometers As Double
            Get
                Return AreaSqrKilometers / 1000000
            End Get
        End Property
    End Class

    Public Class PopulationStatisticByYear

        Public Property Year As Integer

        Public Property Population As Double

        Public Property UrbanPercent As Double

        Public ReadOnly Property PopulationMillionsOfPeople As Double
            Get
                Return Population / 1000000
            End Get
        End Property

        Public ReadOnly Property RuralPercent As Double
            Get
                Return 100 - UrbanPercent
            End Get
        End Property
    End Class
End Namespace
