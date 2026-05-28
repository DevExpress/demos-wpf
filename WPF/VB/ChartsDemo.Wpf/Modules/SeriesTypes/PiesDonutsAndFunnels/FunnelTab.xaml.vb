Namespace ChartsDemo

    Public Partial Class FunnelTab
        Inherits TabItemModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnSelected()
            MyBase.OnSelected()
            MainWindow.CommonRibbon.Init(chart)
        End Sub
    End Class
End Namespace
