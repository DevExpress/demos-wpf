Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Bars.Native
Imports DevExpress.Xpf.Docking
Imports System

Namespace DockingDemo

    Public Class DockLayoutManagerLinkerService
        Inherits ServiceBase

        <ThreadStatic>
        Private Shared ReadOnly managers As WeakList(Of DockLayoutManager)

        Shared Sub New()
            managers = New WeakList(Of DockLayoutManager)()
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Dim manager As DockLayoutManager = TryCast(AssociatedObject, DockLayoutManager)
            If manager Is Nothing OrElse managers.Contains(manager) Then Return
            SyncLock managers
                For Each m In managers
                    Call DockLayoutManagerLinker.Link(manager, m)
                Next

                Call managers.Add(manager)
            End SyncLock
        End Sub

        Protected Overrides Sub OnDetaching()
            Dim manager As DockLayoutManager = TryCast(AssociatedObject, DockLayoutManager)
            If manager IsNot Nothing AndAlso managers.Contains(manager) Then
                SyncLock managers
                    For Each m In managers
                        Call DockLayoutManagerLinker.Unlink(manager, m)
                    Next

                    Call managers.Remove(manager)
                End SyncLock
            End If

            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
