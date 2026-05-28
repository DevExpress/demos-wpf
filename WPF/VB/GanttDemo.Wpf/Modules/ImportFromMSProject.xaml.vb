Imports DevExpress.Xpf.DemoBase

Namespace GanttDemo

    <CodeFile("Utils/ProjectXMLLoader.cs")>
    <CodeFile("ViewModels/ProjectViewModel.cs")>
    Public Partial Class ImportFromMSProject
        Inherits GanttDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGantt = gantt
        End Sub
    End Class
End Namespace
