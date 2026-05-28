using System;
using System.Globalization;
using System.Windows.Controls;
using DevExpress.Xpf.Ribbon;

namespace ProductsDemo.Modules {
    public partial class SpreadsheetModule : UserControl {
        const string FileName = "LoanCalculator.xlsx";

        public SpreadsheetModule() {
            InitializeComponent();
            string filePath = Utils.GetRelativePath(FileName);
            if(String.IsNullOrEmpty(filePath))
                return;
            this.spreadsheetControl.LoadDocument(filePath);
        }
    }


}
