Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace AIDemo

    Public Partial Class TextEditModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            mainTextEdit.EditValue = Properties.Resource.TextEditContent
            Dim bitmapImage = New BitmapImage()
            bitmapImage.BeginInit()
            Dim assembly = GetType(TextEditModule).Assembly
            bitmapImage.StreamSource = assembly.GetManifestResourceStream(DemoHelper.GetPath($"{assembly.GetName().Name}.Resources.", assembly) & "ai_assistant_menu.png")
            bitmapImage.EndInit()
            image.Source = bitmapImage
        End Sub
    End Class
End Namespace
