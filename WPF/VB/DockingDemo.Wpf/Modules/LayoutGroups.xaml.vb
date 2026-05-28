Namespace DockingDemo

    Public Partial Class LayoutGroups
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
            AddHandler SizeChanged, New System.Windows.SizeChangedEventHandler(AddressOf Me.LayoutGroups_SizeChanged)
        End Sub

        Private Sub LayoutGroups_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs)
            If e.NewSize.Height < 500 Then
                layoutGroup2.Visibility = System.Windows.Visibility.Collapsed
                layoutGroup3.Visibility = System.Windows.Visibility.Collapsed
            Else
                layoutGroup2.Visibility = System.Windows.Visibility.Visible
                layoutGroup3.Visibility = System.Windows.Visibility.Visible
            End If
        End Sub
    End Class
End Namespace
