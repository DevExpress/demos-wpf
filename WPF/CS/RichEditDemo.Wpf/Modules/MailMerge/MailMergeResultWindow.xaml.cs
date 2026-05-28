using DevExpress.Xpf.Core;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo.Modules.MailMerge {
    
    
    
    public partial class MailMergeResultWindow : ThemedWindow {
        public Document RichEditDocument { get {  return RichEditControl.Document; } }
        public MailMergeResultWindow() {
            InitializeComponent();
        }
    }
}
