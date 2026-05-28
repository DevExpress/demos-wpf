Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo.Modules.MailMerge

    ''' <summary>
    ''' Interaction logic for MailMergeResultWindow.xaml
    ''' </summary>
    Public Partial Class MailMergeResultWindow
        Inherits ThemedWindow

        Public ReadOnly Property RichEditDocument As Document
            Get
                Return RichEditControl.Document
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
