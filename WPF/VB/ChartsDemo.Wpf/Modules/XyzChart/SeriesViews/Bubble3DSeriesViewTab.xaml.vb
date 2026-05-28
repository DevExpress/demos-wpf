Namespace ChartsDemo

    Public Partial Class Bubble3DSeriesViewTab
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
