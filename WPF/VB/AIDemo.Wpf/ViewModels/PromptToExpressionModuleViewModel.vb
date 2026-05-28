Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Data.Utils
Imports DevExpress.Mvvm.DataAnnotations

Namespace AIDemo.ViewModels

    <POCOViewModel>
    Public Class PromptToExpressionModuleViewModel

        Public ReadOnly Property Items As IList(Of ShippingRecord) = GetData(30)
    End Class

    Public Class ShippingRecord

        Public Property TrackingID As Integer

        <DisplayName("Initial Delivery Date")>
        Public Property EstimatedDeliveryDate As Date

        <DisplayName("Updated Delivery Date")>
        Public Property UpdatedDeliveryDate As Date

        <DataType(DataType.Currency)>
        <DisplayName("Order Cost ($)")>
        Public Property OrderCost As Single

        <DataType(DataType.Currency)>
        <DisplayName("Shipping Cost ($)")>
        Public Property ShippingCost As Single

        <DataType(DataType.Currency)>
        <DisplayName("Discount ($)")>
        Public Property Discount As Single
    End Class

    Public Module PromptToExpressionDataHelper

        Public Function GetData(ByVal count As Integer) As IList(Of ShippingRecord)
            Dim records = New List(Of ShippingRecord)(count)
            Dim rnd = NonCryptographicRandom.Default
            Dim dateTime As Date = Date.Today
            For i As Integer = 0 To count - 1
                Dim n As Integer = rnd.Next(10)
                Dim doubleOrderCost As Double = rnd.NextDouble()
                Dim shippingCost As Double = rnd.NextDouble()
                Dim record = New ShippingRecord() With {.TrackingID = i + 1000, .EstimatedDeliveryDate = dateTime.AddDays(-(n Mod 3)), .UpdatedDeliveryDate = dateTime.AddDays(n Mod 9 - 2), .OrderCost = CSng(doubleOrderCost * 500 + 50), .ShippingCost = CSng(shippingCost * 50 + 10), .Discount = If(i Mod 3 = 0, 10, 0)}
                records.Add(record)
            Next

            Return records
        End Function
    End Module
End Namespace
