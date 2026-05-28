Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Scheduling
Imports System.Windows.Controls
Imports System.Windows.Threading

Namespace SchedulingDemo

    Public Partial Class SchedulerDemoRibbon
        Inherits UserControl

        Public Property ShowPageHome As Boolean

        Public Property ShowPageGroupShare As Boolean

        Private lockApplyState As Boolean

        Public Sub New()
            ClearState()
            InitializeComponent()
        End Sub

        Public Sub Init(ByVal scheduler As SchedulerControl)
            lockApplyState = True
            ClearState()
            If scheduler Is Nothing Then
                MergingProperties.SetElementMergingBehavior(Me, ElementMergingBehavior.None)
                Visibility = System.Windows.Visibility.Collapsed
            Else
                MergingProperties.SetElementMergingBehavior(Me, ElementMergingBehavior.InternalWithExternal)
                Visibility = System.Windows.Visibility.Visible
            End If

            Dim oldScheduler = SchedulerControl.GetScheduler(Me)
            If oldScheduler Is Nothing Then
                SchedulerControl.SetScheduler(Me, scheduler)
            End If

            Dispatcher.InvokeAsync(Sub()
                SchedulerControl.SetScheduler(Me, scheduler)
                lockApplyState = False
                ApplyState()
            End Sub, DispatcherPriority.Loaded)
        End Sub

        Public Sub Update()
            ApplyState()
        End Sub

        Private Sub ClearState()
            ShowPageHome = True
            ShowPageGroupShare = False
        End Sub

        Private Sub ApplyState()
            If lockApplyState Then Return
            pageHome.IsVisible = ShowPageHome
            pageGroupShare.IsVisible = ShowPageGroupShare
        End Sub
    End Class
End Namespace
