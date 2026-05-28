Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Reflection
Imports System.Xml.Serialization
Imports DevExpress.Utils
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace EditorsDemo

    <XmlRoot("Countries")>
    Public Class CountriesData
        Inherits List(Of Country)

        Private Shared dataSourceField As List(Of Country) = Nothing

        Public Shared ReadOnly Property DataSource As List(Of Country)
            Get
                If dataSourceField IsNot Nothing Then Return dataSourceField
                Dim assembly As Assembly = Assembly.GetExecutingAssembly()
                Dim stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("EditorsDemo.Data.", assembly) & "Countries.xml")
                dataSourceField = SafeXml.Deserialize(Of CountriesData)(stream)
                Return dataSourceField
            End Get
        End Property
    End Class

    Public Class Country

        Public ReadOnly Property ActualName As String
            Get
                Return If(NWindName, Name)
            End Get
        End Property

        Public Property Name As String

        Public Property NWindName As String

        Public Property Flag As Byte()
    End Class
End Namespace
