using System;
using DevExpress.Xpf.Bars;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo {
    public partial class DocumentProperties : RichEditDemoModule {
        public static readonly DocumentPropertyInfo[] Properties = new DocumentPropertyInfo[] {
            new DocumentPropertyInfo("Category", "DOCPROPERTY Category"),
            new DocumentPropertyInfo("Created", "CREATEDATE"),
            new DocumentPropertyInfo("Creator", "AUTHOR"),
            new DocumentPropertyInfo("Description", "COMMENTS"),
            new DocumentPropertyInfo("Keywords"),
            new DocumentPropertyInfo("LastModifiedBy", "LASTSAVEDBY"),
            new DocumentPropertyInfo("LastPrinted", "PRINTDATE"),
            new DocumentPropertyInfo("Modified", "SAVEDATE"),
            new DocumentPropertyInfo("Revision", "REVNUM"),
            new DocumentPropertyInfo("Subject"),
            new DocumentPropertyInfo("Title"),
        };

        public DocumentProperties() {
            InitializeComponent();

            MainWindow.CommonRibbon.Init(richEdit);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_Home = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_Review = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.RibbonGroup_MailMerge_WriteInsertFields = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_ViewMergedData = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_FirstRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_PreviousRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_NextRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_LastRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_MailMerge = false;

            Document.Fields.Update();
        }

        Document Document { get { return richEdit.Document; } }

        void OnDocumentPropertiesChanged(object sender, EventArgs e) {
            Document.Fields.Update();
        }
        void OnCalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e) {
            if(e.Arguments.Count == 0 || e.VariableName != "CustomProperty")
                return;

            string name = e.Arguments[0].Value;
            object customProperty = Document.CustomProperties[name];
            if(customProperty != null)
                e.Value = customProperty.ToString();
            e.Handled = true;
        }

        void OnPropertyItemClick(object sender, ItemClickEventArgs e) {
            var propertyInfo = (DocumentPropertyInfo)e.Item.DataContext;
            Document.BeginUpdate();
            Field field = Document.Fields.Create(richEdit.Document.CaretPosition, propertyInfo.Name);
            field.Update();
            Document.EndUpdate();
        }
    }
}
