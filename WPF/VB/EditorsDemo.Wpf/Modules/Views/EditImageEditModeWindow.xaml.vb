Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo.Modules.Views

    Public Partial Class EditImageEditModeWindow

        Public Sub New(ByVal imageSource As ImageSource)
            InitializeComponent()
            ImageEdit.Source = imageSource
        End Sub

        Public ReadOnly Property ImageSource As ImageSource
            Get
                Return ImageEdit.Source
            End Get
        End Property

        Private Sub OnCustomEditMenuItems(ByVal sender As Object, ByVal e As CustomEditMenuItemsEventArgs)
            e.EditMenuItems.Add(New ImageEditToolSeparatorInfo())
            e.EditMenuItems.Add(New ImageEditToolButtonInfo With {.Glyph = ImageEditToolButtonHelper.ImageIdToImageSource("ok"), .ToolTip = EditorLocalizer.GetString(EditorStringId.OK), .Command = New DelegateCommand(AddressOf Confirm)})
            e.EditMenuItems.Add(New ImageEditToolButtonInfo With {.Glyph = ImageEditToolButtonHelper.ImageIdToImageSource("cancel"), .ToolTip = EditorLocalizer.GetString(EditorStringId.Cancel), .Command = New DelegateCommand(AddressOf Close)})
        End Sub

        Private Sub Confirm()
            DialogResult = True
            Close()
        End Sub
    End Class
End Namespace
