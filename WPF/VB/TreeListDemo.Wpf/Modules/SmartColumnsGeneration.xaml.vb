Imports DevExpress.Xpf.DemoBase

Namespace TreeListDemo

    <CodeFile("Data/SmartColumnsGenerationViewModel.(cs)")>
    Public Partial Class SmartColumnsGeneration
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.InitRibbon(treeList)
        End Sub
    End Class
End Namespace
