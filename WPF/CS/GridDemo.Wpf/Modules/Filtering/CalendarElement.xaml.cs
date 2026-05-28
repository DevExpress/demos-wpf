using System;
using System.Windows.Controls;
using DevExpress.Data.Filtering;

namespace GridDemo.Filtering {
    public partial class CalendarElement : UserControl {
        public CalendarElement() {
            InitializeComponent();
            grid.FilterCriteria = new OperandProperty("OrderDate") >= DateTime.Today.AddDays(-5)
                & new OperandProperty("OrderDate") < DateTime.Today.AddDays(1);
        }
    }
}
