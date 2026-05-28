using System;
using System.Reflection;
using DevExpress.Xpf.Grid.Automation;

namespace GridDemo {
    public partial class RowTemplate : GridDemoModule {
        public RowTemplate() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsRowTemplate;
        }

        private void TableView_AutomationRequested(object sender, System.Windows.RoutedEventArgs e) {
            RowAutomationEventArgs rowArgs = e as RowAutomationEventArgs;
            if(rowArgs != null) {
                object dataItem = rowArgs.Row;
                if(dataItem != null) {
                    object fullName = GetPropertyValue(dataItem, "FullName");
                    object title = GetPropertyValue(dataItem, "Title");
                    object hireDate = GetPropertyValue(dataItem, "HireDate");
                    object notes = GetPropertyValue(dataItem, "Notes");
                    object address = GetPropertyValue(dataItem, "Address");

                    rowArgs.AutomationValue =
                        $"Name: {fullName}; " +
                        $"Position: {title}; " +
                        $"Hire Date: {((DateTime?)hireDate)?.ToLongDateString()}; " +
                        $"About: {notes}; " +
                        $"Address: {address}.";
                }
            }
        }

        private object GetPropertyValue(object obj, string propertyName) {
            var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return property?.GetValue(obj);
        }
    }
}
