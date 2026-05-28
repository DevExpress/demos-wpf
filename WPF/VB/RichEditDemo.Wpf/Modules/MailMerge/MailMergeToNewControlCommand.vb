Imports DevExpress.Xpf.DemoCenterBase
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Commands

Namespace RichEditDemo.Modules.MailMerge

    Public Class MailMergeToNewControlCommand
        Inherits MailMergeSaveDocumentAsCommand

        Private ReadOnly sourceRichEditControl As IRichEditControl

        Private ReadOnly mailMergeOptions As MailMergeOptions

        Public Sub New(ByVal sourceRichEditControl As IRichEditControl, ByVal mailMergeOptions As MailMergeOptions)
            MyBase.New(sourceRichEditControl)
            Me.sourceRichEditControl = sourceRichEditControl
            Me.mailMergeOptions = mailMergeOptions
        End Sub

        Protected Overrides Sub ExecuteCore()
            Dim options = If(mailMergeOptions, sourceRichEditControl.Document.CreateMailMergeOptions())
            Dim resultWindow = New MailMergeResultWindow()
            Try
                DemoRunner.ShowApplicationSplashScreen(initSplashScreenViewModel:=Sub(vm) vm.Status = "Mail Merge in progress...", createWaitIndicator:=True)
                sourceRichEditControl.Document.MailMerge(options, resultWindow.RichEditDocument)
            Finally
                Call DemoRunner.CloseApplicationSplashScreen()
            End Try

            resultWindow.ShowDialog()
        End Sub
    End Class
End Namespace
