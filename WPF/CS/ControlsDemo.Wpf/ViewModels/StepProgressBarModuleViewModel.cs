using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using CommonDemo.Helpers;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;

namespace ControlsDemo {
    public class StepProgressBarModuleViewModel : BindableBase {
        public ObservableCollection<OrderStep> OrderTrackSteps { get; set; }
        public StepProgressBarModuleViewModel() {
            OrderTrackSteps = new ObservableCollection<OrderStep>();
            OrderTrackSteps.Add(new OrderStep() {
                NotStartedIndicatorContent = "1",
                NotStartedOrderInfo = new OrderInfo() { Status = "Order" },
                IndeterminateOrderInfo = new OrderInfo() { Status = "Ordering" },
                CompletedOrderInfo = new OrderInfo() { Status = "Ordered", StatusDescription = "Your order was placed", Date = "Sunday, October 1, 2023" }
            });
            OrderTrackSteps.Add(new OrderStep() {
                NotStartedIndicatorContent = "2",
                NotStartedOrderInfo = new OrderInfo() { Status = "Approvement" },
                IndeterminateOrderInfo = new OrderInfo() { Status = "Under review" },
                CompletedOrderInfo = new OrderInfo() { Status = "Approved", StatusDescription = "Your order was approved", Date = "Sunday, October 1, 2023" }
            });
            OrderTrackSteps.Add(new OrderStep() {
                NotStartedIndicatorContent = "3",
                NotStartedOrderInfo = new OrderInfo() { Status = "Pack" },
                IndeterminateOrderInfo = new OrderInfo() { Status = "Packing" },
                CompletedOrderInfo = new OrderInfo() { Status = "Packed", StatusDescription = "Your order was picked up by our courier", Date = "Tuesday, October 3, 2023" }
            });
            OrderTrackSteps.Add(new OrderStep() {
                NotStartedIndicatorContent = "4",
                NotStartedOrderInfo = new OrderInfo() { Status = "Shipment" },
                IndeterminateOrderInfo = new OrderInfo() { Status = "Shipping" },
                CompletedOrderInfo = new OrderInfo() { Status = "Shipped", StatusDescription = "Your order was shipped", Date = "Tuesday, October 3, 2023" }
            });
            OrderTrackSteps.Add(new OrderStep() {
                NotStartedIndicatorContent = "5",
                NotStartedOrderInfo = new OrderInfo() { Status = "Delivery" },
                IndeterminateOrderInfo = new OrderInfo() { Status = "Delivering" },
                CompletedOrderInfo = new OrderInfo() { Status = "Delivered", StatusDescription = "Your order was delivered", Date = "Tuesday, October 3, 2023" }
            });
        }
    }
    public class OrderStep {
        public string NotStartedIndicatorContent { get; set; }
        public ImageSource CompletedIndicatorImage { get; set; }
        public OrderInfo NotStartedOrderInfo { get; set; }
        public OrderInfo IndeterminateOrderInfo { get; set; }
        public OrderInfo CompletedOrderInfo { get; set; }

    }
    public class OrderInfo {
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string Date { get; set; }
    }

}
