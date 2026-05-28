Namespace GanttDemo

    Public Partial Class CriticalPath
        Inherits GanttDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGantt = gantt
        End Sub
    End Class
End Namespace
