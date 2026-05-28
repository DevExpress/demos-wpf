using System;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.Data.Utils;

namespace TreeListDemo {
    public partial class ClipboardFormats : TreeListDemoModule {
        protected override bool ShowBorder { get { return true; } }
        public ClipboardFormats() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
            view.SelectCells(1, treeList.Columns.First(), 15, treeList.Columns.Last());
            CopyAndPasteClipboardData();
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            CopyAndPasteClipboardData();
        }
        void CopyAndPasteClipboardData() {
            view.CopySelectedCellsToClipboard();
            PasteClipboardData();
        }
        void PasteClipboardData() {
            PasteHTMLFormat();
            PasteXLSFormat();
            PasteRTFFormat();
        }
        void PasteRTFFormat() {
            richEditControl.Document.RtfText = string.Empty;
            richEditControl.Document.Text = string.Empty;
            string rtfText = SafeClipboardWpf.Instance.GetData(DataFormats.Rtf) as string;

            richEditControl.Document.AppendRtfText(rtfText);
        }
        void PasteXLSFormat() {
            spreadsheetControl.ActiveWorksheet.Clear(spreadsheetControl.ActiveWorksheet.GetDataRange());
            Stream stream = SafeClipboardWpf.Instance.GetData("Biff8") as Stream;

            spreadsheetControl.LoadDocument(stream, DevExpress.Spreadsheet.DocumentFormat.Xls);
            spreadsheetControl.ActiveWorksheet.DefaultColumnWidthInPixels = (int)spreadsheetControl.ActualWidth / treeList.Columns.Count;
        }
        void PasteHTMLFormat() {
            string html = SafeClipboardWpf.Instance.GetData(DataFormats.Html) as string;

            if(string.IsNullOrEmpty(html))
                webBrowser.NavigateToString(" ");
            else {
                html = html.Remove(0, html.Substring(0, html.IndexOf("<html", StringComparison.OrdinalIgnoreCase)).Length);
                webBrowser.NavigateToString(html);
            }
        }
        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            webBrowser.Visibility = Visibility.Visible;
        }
        protected override void HidePopupContent() {
            webBrowser.Visibility = Visibility.Collapsed;
            base.HidePopupContent();
        }
        protected override void Clear() {
            base.Clear();
            webBrowser.Dispose();
        }
    }
}
