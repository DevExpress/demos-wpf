using System.Collections.Generic;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class CharacterComb : ReportModuleViewModelBase {
        static CharacterComb() {
            EditingFieldExtensions.Instance.RegisterEditorInfo("Name", "Custom", "Name");
            EditingFieldExtensions.Instance.RegisterEditorInfo("NumbersAndUppercaseLetters", "Custom", "Numbers and Uppercase letters");
            EditingFieldExtensions.Instance.RegisterEditorInfo("UppercaseText", "Custom", "Uppercase Text");
            EditingFieldExtensions.Instance.RegisterEditorInfo("NumbersAndUppercaseLatinLetters", "Custom", "Numbers and Uppercase Latin letters");
            EditingFieldExtensions.Instance.RegisterEditorInfo("Day", PreviewStringId.EditingFieldEditorCategories_DateTime.GetString());
            EditingFieldExtensions.Instance.RegisterEditorInfo("Month", PreviewStringId.EditingFieldEditorCategories_DateTime.GetString());
            EditingFieldExtensions.Instance.RegisterEditorInfo("Year", PreviewStringId.EditingFieldEditorCategories_DateTime.GetString());

            var images = GetImages();
            EditingFieldExtensions.Instance.RegisterImageCollectionEditorInfo("Nationality", images, true, false, "Nationality");
        }

        public override ZoomMode DefaultZoomMode { get { return ZoomMode.ActualSize; } }
        public override bool ShowPageMargins { get { return false; } }
        public override bool HighlightEditingFields { get { return true; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.CharacterComb.Report();
        }

        static IDictionary<string, Image> GetImages() {
            var asm = typeof(CharacterComb).Assembly;
            Dictionary<string, Image> images = new Dictionary<string, Image>();
            string[] names = new string[] { "United_States_of_America", "United_Kingdom", "Australia", "Germany", "France", "Russia", "China", "Japan", "India", "Italy" };
            foreach(string name in names) {
                Image image = ResourceImageHelper.CreateImageFromResources(string.Format("ReportWpfDemo.Resources.Flags.{0}.png", name), asm);
                if(image != null)
                    images.Add(name.Replace("_", " "), image);
            }
            return images;
        }
    }
}
