Imports System
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Utils.Svg
Imports DevExpress.Xpf.Core.Native

Namespace EditorsDemo

    Public Class BrowsePathEditModuleViewModel
        Inherits ViewModelBase

        Private sourceFilePathField As String

        Public Property SourceFilePath As String
            Get
                Return sourceFilePathField
            End Get

            Set(ByVal value As String)
                If Equals(sourceFilePathField, value) Then Return
                If Not CheckUploadingFile(value) Then Return
                sourceFilePathField = value
                RaisePropertyChanged(Function() SourceFilePath)
                SourceImage = WpfSvgRenderer.CreateImageSource(SvgImage.FromFile(SourceFilePath))
                ResultFilePath = Path.Combine(Path.GetDirectoryName(sourceFilePathField), String.Format("{0}.png", Path.GetFileNameWithoutExtension(sourceFilePathField)))
            End Set
        End Property

        Const MaxFileSize As Integer = 5 * 1024 * 1024 ' 5 MB

        Private Function CheckUploadingFile(ByVal file As String) As Boolean
            ' Normalize Unicode
            Dim decoded As String = Uri.UnescapeDataString(file)
            decoded = decoded.Normalize(NormalizationForm.FormC)
            ' Check for UNC path
            If decoded.StartsWith("//") OrElse decoded.StartsWith("\\") Then Return False
            ' Check decoded name and check if file exists
            If decoded.Length = 0 OrElse Not IO.File.Exists(decoded) Then Return False
            ' Check extension
            Dim ext As String = Path.GetExtension(decoded)
            If String.IsNullOrEmpty(ext) OrElse Not Equals(ext.ToLowerInvariant(), ".svg") Then Return False
            ' Check file size
            If New FileInfo(decoded).Length > MaxFileSize Then Return False
            Return True
        End Function

        Public Property ResultFilePath As String
            Get
                Return GetProperty(Function() Me.ResultFilePath)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() ResultFilePath, value)
            End Set
        End Property

        Public Property SourceImage As ImageSource
            Get
                Return GetProperty(Function() Me.SourceImage)
            End Get

            Set(ByVal value As ImageSource)
                SetProperty(Function() SourceImage, value)
            End Set
        End Property

        Public Property ResultFileSizeKilobytes As Integer
            Get
                Return GetProperty(Function() Me.ResultFileSizeKilobytes)
            End Get

            Set(ByVal value As Integer)
                SetProperty(Function() ResultFileSizeKilobytes, value)
            End Set
        End Property

        Private ReadOnly Property messageBoxService As IMessageBoxService
            Get
                Return ServiceContainer.GetService(Of IMessageBoxService)()
            End Get
        End Property

        Private Function CheckCanConvert() As Boolean
            If Not File.Exists(SourceFilePath) Then
                messageBoxService.ShowMessage("Specify the source file.", "Warning", MessageButton.OK, MessageIcon.Warning)
                Return False
            End If

            If String.IsNullOrEmpty(ResultFilePath) Then
                messageBoxService.ShowMessage("Specify the destination file.", "Warning", MessageButton.OK, MessageIcon.Warning)
                Return False
            End If

            Return True
        End Function

        <Command>
        Public Sub Convert()
            If Not CheckCanConvert() Then Return
            Try
                Dim k = SourceImage.Width / SourceImage.Height
                Dim height = 200R
                Dim width = height * k
                Dim img = New System.Windows.Controls.Image With {.Source = SourceImage}
                img.Arrange(New Rect(0, 0, width, height))
                Dim bitmap = New RenderTargetBitmap(CInt(width), CInt(height), 96, 96, PixelFormats.Pbgra32)
                bitmap.Render(img)
                Dim encoder = New PngBitmapEncoder()
                encoder.Frames.Add(BitmapFrame.Create(bitmap))
                Using stream = New FileStream(ResultFilePath, FileMode.Create)
                    encoder.Save(stream)
                    ResultFileSizeKilobytes = CInt(stream.Length / 1000)
                End Using

                If messageBoxService.Show("Operation completed. Do you want to open the destination file and review the processed image?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
                    DevExpress.Data.Utils.SafeProcess.Start(ResultFilePath)
                End If
            Catch ex As Exception
                messageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error)
            End Try
        End Sub

        <Command>
        Public Sub Clear()
            SourceImage = Nothing
            SourceFilePath = Nothing
            ResultFilePath = Nothing
            ResultFileSizeKilobytes = 0
        End Sub
    End Class
End Namespace
