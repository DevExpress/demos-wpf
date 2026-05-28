using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.PropertyGrid;

namespace PropertyGridDemo {
    public partial class CollectionEditingModule : PropertyGridDemoModule {
        public CollectionEditingModule() {
            InitializeComponent();
        }
        void OnVisualizerPanelLoaded(object sender, RoutedEventArgs e) {
            grid.ItemsSource = new List<object> { root };
        }

        private void OnVisualizerPanelChanged(object sender, EventArgs e) {
            if(grid != null)
                grid.RefreshData();
        }

        private void pGrid_CustomExpand(object sender, CustomExpandEventArgs args) {
            if(args.IsInitializing) {
                args.Row.Children.FirstOrDefault();
            }
        }

        private void pGrid_CollectionButtonsVisibility(object sender, CollectionButtonsVisibilityEventArgs e) {
            if(e.ButtonKind == CollectionButtonKind.Add) {
                var collection = (UIElementCollection)e.Value;
                e.IsVisible = collection.Count <= 5;
            }
            if(e.ButtonKind == CollectionButtonKind.Remove) {
                var button = e.Value as Button;
                if(button != null && string.Equals(button.Content as string, "Button", StringComparison.Ordinal))
                    e.IsVisible = false;
            }
        }
        private void pGrid_CollectionButtonClick(object sender, CollectionButtonClickEventArgs e) {
            if(e.ButtonKind == CollectionButtonKind.Add) {
                var collection = (UIElementCollection)e.Value;
                e.DefaultAction.Invoke(new Button { Content = "Button " + collection.OfType<Button>().Count() });
                e.Handled = true;
            }
        }

        private void XamlInitializer_Initialize(object sender, InstanceInitializeEventArgs e) {
            e.Instance.HorizontalAlignment = HorizontalAlignment.Center;
            e.Instance.Margin = new Thickness(5);
            if(e.Type == typeof(Button) || e.Type == typeof(CheckBox))
                e.Instance.Content = e.Name;
            if(e.Type == typeof(TextBox))
                e.Instance.Text = e.Name;
        }
    }

    public class VisualItemsSelector : IChildNodesSelector {
        public IInstanceInitializer Initializer { get; set; }
        IEnumerable IChildNodesSelector.SelectChildren(object item) {
            List<FrameworkElement> list = new List<FrameworkElement>();
            FrameworkElement fe = (FrameworkElement)item;
            if(Initializer.Types.Any(typeInfo => item.GetType() == typeInfo.Type))
                return null;
            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(fe); i++)
                list.Add((FrameworkElement)VisualTreeHelper.GetChild(fe, i));
            return list;
        }
    }
    public class ItemsSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new List<object> { value };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class GetNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null ? value.GetType().Name : string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
