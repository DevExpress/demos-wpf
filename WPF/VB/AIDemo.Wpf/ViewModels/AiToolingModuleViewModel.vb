Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports DevExpress.Data.Utils
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace AIDemo.ViewModels

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class AiToolingModuleViewModel

        Public ReadOnly Property Items As ObservableCollection(Of AIDemo.ViewModels.Order) = New System.Collections.ObjectModel.ObservableCollection(Of AIDemo.ViewModels.Order)(AIDemo.ViewModels.OrderDataHelper.GetData(10))
    End Class

    Public Class Order
        Inherits DevExpress.Mvvm.BindableBase

        Public Property OrderID As Integer

        Public Property CustomerName As String

        Public Property OrderDate As DateTime

        Public Property TotalAmount As Decimal

        Public Property ProductName As String

        Public Property Quantity As Integer

        Public Property Status As String

        Public Property Country As String
    End Class

    Public Class OrderDataHelper

        Private Shared ReadOnly Customers As String() = {"Alice", "Bob", "Charlie", "Diana", "Evan", "Fiona", "George", "Helen", "Ivan", "Julia", "Karl", "Laura", "Martin", "Nina", "Oscar"}

        Private Shared ReadOnly Products As String() = {"Laptop", "Mouse", "Monitor", "Keyboard", "Webcam", "Headset", "Docking Station", "USB Hub", "External SSD", "Graphic Tablet"}

        Private Shared ReadOnly Countries As String() = {"USA", "Canada", "UK", "Germany", "France", "Spain", "Italy", "Netherlands", "Australia", "Japan"}

        Private Shared ReadOnly Statuses As String() = {"Pending", "Processing", "Shipped", "Delivered", "Cancelled", "On Hold"}

        Private Shared ReadOnly BasePrices As System.Collections.Generic.Dictionary(Of String, Decimal) = New System.Collections.Generic.Dictionary(Of String, Decimal)(System.StringComparer.OrdinalIgnoreCase) From {{"Laptop", 950D}, {"Mouse", 20D}, {"Monitor", 180D}, {"Keyboard", 45D}, {"Webcam", 70D}, {"Headset", 85D}, {"Docking Station", 130D}, {"USB Hub", 25D}, {"External SSD", 150D}, {"Graphic Tablet", 210D}}

        Public Shared Function GetData(ByVal count As Integer) As BindingList(Of AIDemo.ViewModels.Order)
            Dim list = New System.ComponentModel.BindingList(Of AIDemo.ViewModels.Order)()
            Dim rnd = DevExpress.Data.Utils.NonCryptographicRandom.[Default]
            For i As Integer = 0 To count - 1
                Dim product = AIDemo.ViewModels.OrderDataHelper.Products(rnd.[Next](AIDemo.ViewModels.OrderDataHelper.Products.Length))
                Dim quantity As Integer = rnd.[Next](1, 8)
                Dim basePrice As Decimal = AIDemo.ViewModels.OrderDataHelper.BasePrices(product)
                Dim variance As Decimal = basePrice * CDec((rnd.NextDouble() * 0.2 - 0.1))
                Dim total As Decimal = System.Math.Round((basePrice + variance) * quantity, 2)
                Dim order = New AIDemo.ViewModels.Order With {.OrderID = i + 1, .CustomerName = AIDemo.ViewModels.OrderDataHelper.Customers(i Mod AIDemo.ViewModels.OrderDataHelper.Customers.Length), .ProductName = product, .Quantity = quantity, .Country = AIDemo.ViewModels.OrderDataHelper.Countries(rnd.[Next](AIDemo.ViewModels.OrderDataHelper.Countries.Length)), .Status = AIDemo.ViewModels.OrderDataHelper.Statuses(rnd.[Next](AIDemo.ViewModels.OrderDataHelper.Statuses.Length)), .OrderDate = System.DateTime.Today.AddDays(-rnd.[Next](0, 30)), .TotalAmount = total}
                list.Add(order)
            Next

            Return list
        End Function
    End Class
End Namespace
