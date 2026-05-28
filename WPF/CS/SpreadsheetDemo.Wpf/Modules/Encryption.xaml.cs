using System;
using System.Windows;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors;

namespace SpreadsheetDemo {
    public partial class Encryption : SpreadsheetDemoModule {

        public Encryption() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            InitializeEncryptionOptions();
        }

        void InitializeEncryptionOptions() {
            passwordEdit.Text = "test";

            string[] array = Enum.GetNames(typeof(EncryptionType));
            typeEdit.ItemsSource = array;
            typeEdit.SelectedItem = EncryptionType.Strong.ToString();
        }

        void PasswordChanged(object sender, EditValueChangedEventArgs e) {
            spreadsheetControl1.Document.DocumentSettings.Encryption.Password = passwordEdit.Text;
        }

        void TypeChanged(object sender, EditValueChangedEventArgs e) {
            spreadsheetControl1.Document.DocumentSettings.Encryption.Type = (EncryptionType)Enum.Parse(typeof(EncryptionType), typeEdit.Text);
        }

        void EncryptAndSave(object sender, RoutedEventArgs e) {
            string filter = "Excel Workbook files(*.xlsx)|*.xlsx|Excel Binary Workbook(*.xlsb)|*.xlsb|Excel 97-2003 Workbook files(*.xls)|*.xls";
            string fileName = FileSaveHelper.GetSaveFileName(filter, "Document.xlsx");
            if(String.IsNullOrEmpty(fileName))
                return;
            spreadsheetControl1.SaveDocument(fileName);
            if(openFileCheckEditBox.IsChecked.HasValue && openFileCheckEditBox.IsChecked.Value)
                FileOpenHelper.ShowFile(fileName);
        }
    }
}
