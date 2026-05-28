using DevExpress.Xpf.DemoCenterBase;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;

namespace RichEditDemo.Modules.MailMerge {
    public class MailMergeToNewControlCommand : MailMergeSaveDocumentAsCommand {
        readonly IRichEditControl sourceRichEditControl;
        readonly MailMergeOptions mailMergeOptions;

        public MailMergeToNewControlCommand(IRichEditControl sourceRichEditControl, MailMergeOptions mailMergeOptions) : base(sourceRichEditControl) {
            this.sourceRichEditControl = sourceRichEditControl;
            this.mailMergeOptions = mailMergeOptions;
        }
        protected override void ExecuteCore() {
            var options = mailMergeOptions ?? sourceRichEditControl.Document.CreateMailMergeOptions();
            var resultWindow = new MailMergeResultWindow();
            try {
                DemoRunner.ShowApplicationSplashScreen(initSplashScreenViewModel: (vm) => vm.Status = "Mail Merge in progress...", createWaitIndicator: true);
                sourceRichEditControl.Document.MailMerge(options, resultWindow.RichEditDocument);
            }
            finally {
                DemoRunner.CloseApplicationSplashScreen();
            }
            resultWindow.ShowDialog();
        }
    }
}
