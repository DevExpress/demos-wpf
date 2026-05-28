using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo.Modules.MailMerge {
    public class MailMergeCommandFactoryService : IRichEditCommandFactoryService {
        readonly IRichEditCommandFactoryService parentFactory;
        readonly IRichEditControl richEditControl;
        readonly MailMergeOptions mailMergeOptions;

        public MailMergeCommandFactoryService(IRichEditCommandFactoryService parentFactory, IRichEditControl richEditControl, MailMergeOptions mailMergeOptions) {
            this.parentFactory = parentFactory;
            this.richEditControl = richEditControl;
            this.mailMergeOptions = mailMergeOptions;
        }

        public RichEditCommand CreateCommand(RichEditCommandId id) {
            if(id == RichEditCommandId.MailMergeSaveDocumentAs)
                return new MailMergeToNewControlCommand(richEditControl, mailMergeOptions);

            return parentFactory.CreateCommand(id);
        }
    }
}
