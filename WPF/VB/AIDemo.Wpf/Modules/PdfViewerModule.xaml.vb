Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo

    Public Partial Class PdfViewerModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            Dim assembly = GetType(ReportViewerModule).Assembly
            Dim documentStream = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Data.", assembly) & "PageContent.pdf")
            pdfViewerControl.DocumentSource = documentStream
        End Sub
    End Class
End Namespace
