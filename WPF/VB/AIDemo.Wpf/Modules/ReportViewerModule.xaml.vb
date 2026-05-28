Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo

    Public Partial Class ReportViewerModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            Dim assembly = GetType(ReportViewerModule).Assembly
            Dim documentStream = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Data.", assembly) & "TableReport.prnx")
            documentPreview.DocumentSource = documentStream
        End Sub
    End Class
End Namespace
