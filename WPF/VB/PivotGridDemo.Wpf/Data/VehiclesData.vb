Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Data.Utils
Imports DevExpress.Xpf.Core.Native
#If NET
Imports Microsoft.EntityFrameworkCore

#Else
using System.Data.Entity;
#End If
Namespace DevExpress.DemoData

    Public Class VehiclesData

        Public Shared Function GetData() As List(Of DevExpress.DemoData.VehiclesData.OrderItem)
            Return DevExpress.DemoData.VehiclesData.InitOrdersData(10000, 3 * 365)
        End Function

        Public Class OrderItem

            Friend Model As DevExpress.DemoData.VehiclesData.Model

            Public Sub New(ByVal model As DevExpress.DemoData.VehiclesData.Model, ByVal days As Integer, ByVal rnd As DevExpress.Data.Utils.NonCryptographicRandom, ByVal id As Integer)
                Me.Model = model
                Me.ModelPrice = model.Price
                Me.Trademark = model.Trademark
                Me.Name = model.Name
                Me.Modification = model.Modification
                Me.Category = model.Category
                Me.MPGCity = model.MPGCity
                Me.MPGHighway = model.MPGHighway
                Me.Doors = model.Doors
                Me.BodyStyle = model.BodyStyle.ToString()
                Me.Cylinders = model.Cylinders
                Me.Horsepower = model.Horsepower
                Me.Torque = model.Torque
                Me.TransmissionSpeeds = model.TransmissionSpeeds
                Me.TransmissionType = model.TransmissionType
                Me.InStock = model.InStock
                Me.SalesDate = System.DateTime.Now.AddDays(-rnd.[Next](days))
                Me.SalesDateOnly = Me.SalesDate.ToDateOnly()
                Me.Discount = System.Math.Round(0.05 * rnd.[Next](4), 2)
                Me.OrderID = id
            End Sub

            Public Property OrderID As Integer

            Public Property SalesDate As DateTime

            Public Property SalesDateOnly As DateOnly

            Public Property Discount As Double

            Public Property ModelPrice As Decimal

            Public Property Trademark As String

            Public Property Name As String

            Public Property Modification As String

            Public Property Category As Integer

            Public Property MPGCity As Integer?

            Public Property MPGHighway As Integer?

            Public Property Doors As Integer

            Public Property BodyStyle As String

            Public Property Cylinders As Integer

            Public Property Horsepower As String

            Public Property Torque As String

            Public Property TransmissionSpeeds As Integer

            Public Property TransmissionType As Integer

            Public Property InStock As Boolean
        End Class

        Public Class Model

            Public Property ID As Integer

            Public Property Trademark As String

            Public Property Name As String

            Public Property Modification As String

            Public Property Category As Integer

            Public Property Price As Decimal

            Public Property MPGCity As Integer?

            Public Property MPGHighway As Integer?

            Public Property Doors As Integer

            Public Property BodyStyle As String

            Public Property Cylinders As Integer

            Public Property Horsepower As String

            Public Property Torque As String

            Public Property TransmissionSpeeds As Integer

            Public Property TransmissionType As Integer

            Public Property Description As String

            Public Property DeliveryDate As DateTime

            Public Property InStock As Boolean
        End Class

        Public Shared Function InitOrdersData(ByVal itemCount As Integer, ByVal dataInterval As Integer) As List(Of DevExpress.DemoData.VehiclesData.OrderItem)
            Dim rnd As DevExpress.Data.Utils.NonCryptographicRandom = DevExpress.Data.Utils.NonCryptographicRandom.[Default]
            Dim listModels As System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.Model) = DevExpress.DemoData.VehiclesData.InitDataCore(1)
            Dim orders As System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.OrderItem) = New System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.OrderItem)()
            Dim averagePrice As Decimal = System.Linq.Enumerable.[Select](Of DevExpress.DemoData.VehiclesData.Model, Global.System.[Decimal])(listModels, CType((Function(x) CDec((x.Price))), System.Func(Of DevExpress.DemoData.VehiclesData.Model, System.[Decimal]))).Average()
            Dim averageOrders As Integer = itemCount \ listModels.Count
            For Each model As DevExpress.DemoData.VehiclesData.Model In listModels
                For i As Integer = 0 To averageOrders * averagePrice / model.Price - 1
                    orders.Add(New DevExpress.DemoData.VehiclesData.OrderItem(model, dataInterval, rnd, i + 1))
                Next
            Next

            Return orders
        End Function

        Private Shared Function InitDataCore(ByVal dataInterval As Integer) As List(Of DevExpress.DemoData.VehiclesData.Model)
            Dim rnd = DevExpress.Data.Utils.NonCryptographicRandom.[Default]
            Dim listModels = New System.Collections.Generic.List(Of DevExpress.DemoData.VehiclesData.Model)()
            Dim vehiclesContext = New DevExpress.DemoData.Models.Vehicles.VehiclesContext()
            vehiclesContext.Models.Load()
            vehiclesContext.BodyStyles.Load()
            vehiclesContext.Categories.Load()
            vehiclesContext.Trademarks.Load()
            vehiclesContext.TransmissionTypes.Load()
            System.Linq.Enumerable.ToList(Of DevExpress.DemoData.Models.Vehicles.Model)(vehiclesContext.Models.Local).ForEach(Sub(source) listModels.Add(New DevExpress.DemoData.VehiclesData.Model() With {.ID = CInt(source.ID), .Trademark = source.TrademarkName, .Name = source.Name, .Modification = source.Modification, .Category = CInt(source.CategoryID), .Price = source.Price, .MPGCity = source.MPGCity, .MPGHighway = source.MPGHighway, .Doors = source.Doors, .BodyStyle = source.BodyStyleName, .Cylinders = source.Cylinders, .Horsepower = source.Horsepower, .Torque = source.Torque, .TransmissionSpeeds = System.Convert.ToInt32(source.TransmissionSpeeds), .TransmissionType = CInt(source.TransmissionTypeID), .Description = source.Description, .DeliveryDate = System.DateTime.Now.AddDays(rnd.[Next](dataInterval)), .InStock = rnd.[Next](100) < 95}))
            Return listModels
        End Function
    End Class
End Namespace
