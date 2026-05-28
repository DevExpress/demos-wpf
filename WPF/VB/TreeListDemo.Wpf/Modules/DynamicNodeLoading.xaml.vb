Imports DevExpress.Xpf.DemoBase

Namespace TreeListDemo

    <CodeFile("Data/DynamicNodeLoadingViewModel.(cs)")>
    <CodeFile("Data/DynamicNodeLoadingBehavior.(cs)")>
    <CodeFile("Data/FileSystemData.(cs)")>
    Public Partial Class DynamicNodeLoading
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.InitRibbon(treeList)
        End Sub
    End Class
End Namespace
