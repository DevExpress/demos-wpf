Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.XtraRichEdit.Services

Namespace RichEditDemo.Modules.MailMerge

    Public Class MailMergeCommandFactoryService
        Implements IRichEditCommandFactoryService

        Private ReadOnly parentFactory As IRichEditCommandFactoryService

        Private ReadOnly richEditControl As IRichEditControl

        Private ReadOnly mailMergeOptions As MailMergeOptions

        Public Sub New(ByVal parentFactory As IRichEditCommandFactoryService, ByVal richEditControl As IRichEditControl, ByVal mailMergeOptions As MailMergeOptions)
            Me.parentFactory = parentFactory
            Me.richEditControl = richEditControl
            Me.mailMergeOptions = mailMergeOptions
        End Sub

        Public Function CreateCommand(ByVal id As RichEditCommandId) As RichEditCommand Implements IRichEditCommandFactoryService.CreateCommand
            If id = RichEditCommandId.MailMergeSaveDocumentAs Then Return New MailMergeToNewControlCommand(richEditControl, mailMergeOptions)
            Return parentFactory.CreateCommand(id)
        End Function
    End Class
End Namespace
