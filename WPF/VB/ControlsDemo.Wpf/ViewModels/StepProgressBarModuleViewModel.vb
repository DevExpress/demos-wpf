Imports System.Collections.ObjectModel
Imports System.Windows.Media
Imports CommonDemo.Helpers
Imports DevExpress.Mvvm

Namespace ControlsDemo

    Public Class StepProgressBarModuleViewModel
        Inherits BindableBase

        Public Property OrderTrackSteps As ObservableCollection(Of OrderStep)

        Public Sub New()
            OrderTrackSteps = New ObservableCollection(Of OrderStep)()
            OrderTrackSteps.Add(New OrderStep() With {.NotStartedIndicatorContent = "1", .NotStartedOrderInfo = New OrderInfo() With {.Status = "Order"}, .IndeterminateOrderInfo = New OrderInfo() With {.Status = "Ordering"}, .CompletedOrderInfo = New OrderInfo() With {.Status = "Ordered", .StatusDescription = "Your order was placed", .[Date] = "Sunday, October 1, 2023"}})
            OrderTrackSteps.Add(New OrderStep() With {.NotStartedIndicatorContent = "2", .NotStartedOrderInfo = New OrderInfo() With {.Status = "Approvement"}, .IndeterminateOrderInfo = New OrderInfo() With {.Status = "Under review"}, .CompletedOrderInfo = New OrderInfo() With {.Status = "Approved", .StatusDescription = "Your order was approved", .[Date] = "Sunday, October 1, 2023"}})
            OrderTrackSteps.Add(New OrderStep() With {.NotStartedIndicatorContent = "3", .NotStartedOrderInfo = New OrderInfo() With {.Status = "Pack"}, .IndeterminateOrderInfo = New OrderInfo() With {.Status = "Packing"}, .CompletedOrderInfo = New OrderInfo() With {.Status = "Packed", .StatusDescription = "Your order was picked up by our courier", .[Date] = "Tuesday, October 3, 2023"}})
            OrderTrackSteps.Add(New OrderStep() With {.NotStartedIndicatorContent = "4", .NotStartedOrderInfo = New OrderInfo() With {.Status = "Shipment"}, .IndeterminateOrderInfo = New OrderInfo() With {.Status = "Shipping"}, .CompletedOrderInfo = New OrderInfo() With {.Status = "Shipped", .StatusDescription = "Your order was shipped", .[Date] = "Tuesday, October 3, 2023"}})
            OrderTrackSteps.Add(New OrderStep() With {.NotStartedIndicatorContent = "5", .NotStartedOrderInfo = New OrderInfo() With {.Status = "Delivery"}, .IndeterminateOrderInfo = New OrderInfo() With {.Status = "Delivering"}, .CompletedOrderInfo = New OrderInfo() With {.Status = "Delivered", .StatusDescription = "Your order was delivered", .[Date] = "Tuesday, October 3, 2023"}})
        End Sub
    End Class

    Public Class OrderStep

        Public Property NotStartedIndicatorContent As String

        Public Property CompletedIndicatorImage As ImageSource

        Public Property NotStartedOrderInfo As OrderInfo

        Public Property IndeterminateOrderInfo As OrderInfo

        Public Property CompletedOrderInfo As OrderInfo
    End Class

    Public Class OrderInfo

        Public Property Status As String

        Public Property StatusDescription As String

        Public Property [Date] As String
    End Class
End Namespace
