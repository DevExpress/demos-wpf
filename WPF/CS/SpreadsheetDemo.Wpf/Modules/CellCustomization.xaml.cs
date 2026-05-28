using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class CellCustomization : SpreadsheetDemoModule {
        public CellCustomization() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home;
        }
    }

    public class CellTemplateSelector : DataTemplateSelector {
        List<string> customTemplateCells = new List<string>() { "F13", "F22", "F32" };

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            CellData data = item as CellData;
            return CanApplyCustomTemplate(data.Cell.GetReferenceA1()) ? GetCellTemplate(data.Cell, (SpreadsheetControl)container) : base.SelectTemplate(item, container);
        }

        private bool CanApplyCustomTemplate(string cellPosition) {
            return customTemplateCells.Contains(cellPosition);
        }

        private DataTemplate GetCellTemplate(Cell cell, SpreadsheetControl control) {
            double value = 0;
            double.TryParse(cell.DisplayText, NumberStyles.Float, control.Options.Culture, out value);
            string templateName = Math.Sign(value) == -1 ? "IncorrectTemplate" : "CorrectTemplate";
            return control.TryFindResource(templateName) as DataTemplate;
        }
    }
}
