Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports DevExpress.Data.Utils

Namespace ProductsDemo

    Public Class ImageHyperlinkBehavior
        Inherits Behavior(Of Image)

        Public Shared ReadOnly NavigatetUriProperty As DependencyProperty = DependencyProperty.Register("NavigatetUri", GetType(String), GetType(ImageHyperlinkBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property NavigatetUri As String
            Get
                Return CStr(GetValue(NavigatetUriProperty))
            End Get

            Set(ByVal value As String)
                SetValue(NavigatetUriProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.MouseLeftButtonUp, AddressOf OnImageMouseLeftButtonUp
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.MouseLeftButtonUp, AddressOf OnImageMouseLeftButtonUp
            MyBase.OnDetaching()
        End Sub

        Private Sub OnImageMouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            SafeProcess.Start(NavigatetUri, Nothing, Sub(startInfo) startInfo.UseShellExecute = True)
        End Sub
    End Class
End Namespace
