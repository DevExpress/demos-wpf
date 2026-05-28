Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo

    Public Partial Class RichEditModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            Dim assembly = GetType(RichEditModule).Assembly
            Dim documentStream = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Data.", assembly) & "FirstLook.docx")
            mainRichEditControl.LoadDocument(documentStream)
        End Sub
    End Class
End Namespace
