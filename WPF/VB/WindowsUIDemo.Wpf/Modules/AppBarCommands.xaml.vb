Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports System.Resources.ResXFileRef

Namespace WindowsUIDemo

    Public Partial Class AppBarCommands
        Inherits WindowsUIDemo.WindowsUIDemoModule

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class PhotoCollection

        Private Shared converter As System.Windows.Media.ImageSourceConverter = New System.Windows.Media.ImageSourceConverter()

        Public Overridable Property Photos As List(Of WindowsUIDemo.Photo)

        Public Overridable Property SelectedItem As Photo

        Public Sub New()
            If Me.IsInDesignMode() Then Return
            Dim context = New DevExpress.DemoData.Models.Vehicles.VehiclesContext()
            Me.Photos = context.Models.ToList().[Select](Function(x) DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New WindowsUIDemo.Photo With {.Caption = x.Name, .Source = CType(WindowsUIDemo.PhotoCollection.converter.ConvertFrom(x.Photo), System.Windows.Media.ImageSource), .ViewSize = New System.Windows.Size(150, 100), .Model = x})).ToList()
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub RotateClockwise()
            If Me.SelectedItem IsNot Nothing Then
                Select Case Me.SelectedItem.Rotation
                    Case System.Windows.Media.Imaging.Rotation.Rotate0
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate90
                    Case System.Windows.Media.Imaging.Rotation.Rotate90
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate180
                    Case System.Windows.Media.Imaging.Rotation.Rotate180
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate270
                    Case System.Windows.Media.Imaging.Rotation.Rotate270
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
                End Select
            End If
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub RotateCounterclockwise()
            If Me.SelectedItem IsNot Nothing Then
                Select Case Me.SelectedItem.Rotation
                    Case System.Windows.Media.Imaging.Rotation.Rotate0
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate270
                    Case System.Windows.Media.Imaging.Rotation.Rotate90
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
                    Case System.Windows.Media.Imaging.Rotation.Rotate180
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate90
                    Case System.Windows.Media.Imaging.Rotation.Rotate270
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate180
                End Select
            End If
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub Rotate180()
            If Me.SelectedItem IsNot Nothing Then
                Select Case Me.SelectedItem.Rotation
                    Case System.Windows.Media.Imaging.Rotation.Rotate0
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate180
                    Case System.Windows.Media.Imaging.Rotation.Rotate90
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate270
                    Case System.Windows.Media.Imaging.Rotation.Rotate180
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
                    Case System.Windows.Media.Imaging.Rotation.Rotate270
                        Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate90
                End Select
            End If
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub RotateReset()
            If Me.SelectedItem IsNot Nothing Then
                Me.SelectedItem.Rotation = System.Windows.Media.Imaging.Rotation.Rotate0
            End If
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub ZoomIn()
            If Me.SelectedItem IsNot Nothing Then Me.SelectedItem.Scale += 0.1
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub ZoomOut()
            If Me.SelectedItem IsNot Nothing Then Me.SelectedItem.Scale = System.Math.Max(0.1, Me.SelectedItem.Scale - 0.1)
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub ResetScale()
            If Me.SelectedItem IsNot Nothing Then Me.SelectedItem.Scale = 1
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub Print()
            If Me.SelectedItem Is Nothing Then Return
            Dim printDialog As System.Windows.Controls.PrintDialog = New System.Windows.Controls.PrintDialog()
            If printDialog.ShowDialog() = True Then
                printDialog.PrintVisual(New System.Windows.Controls.Image() With {.Source = Me.SelectedItem.Source}, Me.SelectedItem.Caption)
            End If
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub Flip()
            If Me.SelectedItem IsNot Nothing Then Me.SelectedItem.Flip = Not Me.SelectedItem.Flip
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class Photo

        Public Property Source As ImageSource

        Public Property Caption As String

        Public Property ViewSize As Size

        Public Overridable Property Rotation As Rotation

        Public Overridable Property Scale As Double

        Public Overridable Property Flip As Boolean

        Public Overridable Property Model As Model

        Public Sub New()
            Me.Scale = 1
            Me.ViewSize = New System.Windows.Size(Double.NaN, Double.NaN)
        End Sub
    End Class
End Namespace
