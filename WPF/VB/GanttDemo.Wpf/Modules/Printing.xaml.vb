Imports DevExpress.Xpf.DemoBase

Namespace GanttDemo

    <CodeFile("ViewModels/PrintingViewModel.cs")>
    <CodeFile("ViewModels/PrintingViewModelBase.(cs)")>
    <CodeFile("ModuleResources/PrintDemoModuleResources.xaml")>
    Public Partial Class Printing
        Inherits GanttDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGantt = gantt
        End Sub
    End Class
End Namespace
