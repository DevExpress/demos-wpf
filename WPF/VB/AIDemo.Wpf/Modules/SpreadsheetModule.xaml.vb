Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo

    Public Partial Class SpreadsheetModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            Dim assembly = GetType(SpreadsheetModule).Assembly
            Dim documentStream = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Data.", assembly) & "LoanAmortizationSchedule.xlsx")
            mainSpreadsheetControl.LoadDocument(documentStream)
        End Sub
    End Class
End Namespace
