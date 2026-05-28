Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Charts.Designer
Imports DevExpress.Charts.Native
Imports DevExpress.Drawing
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Charts
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrinting.Native

Namespace ChartsDemo

    Public Partial Class ChartsDemoRibbon
        Inherits UserControl

        Public Property Chart As ChartControlBase

        Public Property ShowRunChartDesignerButton As Boolean

        Public Property ShowPaletteButton As Boolean

        Private vm As ChartsDemoRibbonViewModel

        Public Sub New()
            vm = New ChartsDemoRibbonViewModel()
            DataContext = vm
            InitializeComponent()
            AddHandler Loaded, AddressOf OnLoaded
        End Sub

        Public Sub Init(ByVal chart As ChartControlBase, ByVal Optional showPaletteButton As Boolean = True, ByVal Optional showRunChartDesignerButton As Boolean = False)
            vm.Chart = chart
            vm.ShowPaletteButton = showPaletteButton
            vm.ShowRunChartDesignerButton = showRunChartDesignerButton
        End Sub

        Public Sub Clear()
            vm.Chart = Nothing
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim mainVM = CType(MainWindow.Instance.DataContext, MainViewModel)
            CType(vm, ISupportParentViewModel).ParentViewModel = mainVM
        End Sub

        Public Class ChartsDemoRibbonViewModel
            Inherits ViewModelBase

            Private _RunChartDesignerCommand As DelegateCommand, _ExportToImageCommand As DelegateCommand, _ExportToXlsCommand As DelegateCommand, _ExportToDocxCommand As DelegateCommand, _ExportToXlsxCommand As DelegateCommand, _ExportToMhtCommand As DelegateCommand, _ExportToHtmlCommand As DelegateCommand, _ExportToPdfCommand As DelegateCommand, _ExportToRtfCommand As DelegateCommand, _ExportToXpsCommand As DelegateCommand

            Const sTR_DXChart As String = "Exported DXChart"

            Private chartField As ChartControlBase

            Private showPaletteButtonField As Boolean = True

            Private showChartGroupField As Boolean = True

            Private showRunChartDesignerButtonField As Boolean = False

            Private ReadOnly Property SaveFileDialogService As ISaveFileDialogService
                Get
                    Return GetService(Of ISaveFileDialogService)()
                End Get
            End Property

            Private ReadOnly Property MessageBoxService As IMessageBoxService
                Get
                    Return GetService(Of IMessageBoxService)(ServiceSearchMode.PreferParents)
                End Get
            End Property

            Public Property Chart As ChartControlBase
                Get
                    Return chartField
                End Get

                Set(ByVal value As ChartControlBase)
                    SetProperty(chartField, value, NameOf(ChartsDemoRibbonViewModel.Chart), New Action(AddressOf SetChartGroupVisibility))
                End Set
            End Property

            Public Property RunChartDesignerCommand As DelegateCommand
                Get
                    Return _RunChartDesignerCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _RunChartDesignerCommand = value
                End Set
            End Property

            Public Property ExportToImageCommand As DelegateCommand
                Get
                    Return _ExportToImageCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToImageCommand = value
                End Set
            End Property

            Public Property ExportToXlsCommand As DelegateCommand
                Get
                    Return _ExportToXlsCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToXlsCommand = value
                End Set
            End Property

            Public Property ExportToDocxCommand As DelegateCommand
                Get
                    Return _ExportToDocxCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToDocxCommand = value
                End Set
            End Property

            Public Property ExportToXlsxCommand As DelegateCommand
                Get
                    Return _ExportToXlsxCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToXlsxCommand = value
                End Set
            End Property

            Public Property ExportToMhtCommand As DelegateCommand
                Get
                    Return _ExportToMhtCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToMhtCommand = value
                End Set
            End Property

            Public Property ExportToHtmlCommand As DelegateCommand
                Get
                    Return _ExportToHtmlCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToHtmlCommand = value
                End Set
            End Property

            Public Property ExportToPdfCommand As DelegateCommand
                Get
                    Return _ExportToPdfCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToPdfCommand = value
                End Set
            End Property

            Public Property ExportToRtfCommand As DelegateCommand
                Get
                    Return _ExportToRtfCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToRtfCommand = value
                End Set
            End Property

            Public Property ExportToXpsCommand As DelegateCommand
                Get
                    Return _ExportToXpsCommand
                End Get

                Private Set(ByVal value As DelegateCommand)
                    _ExportToXpsCommand = value
                End Set
            End Property

            Public Property ShowPaletteButton As Boolean
                Get
                    Return showPaletteButtonField
                End Get

                Set(ByVal value As Boolean)
                    SetProperty(showPaletteButtonField, value, NameOf(ChartsDemoRibbonViewModel.ShowPaletteButton), New Action(AddressOf SetChartGroupVisibility))
                End Set
            End Property

            Public Property ShowChartGroup As Boolean
                Get
                    Return showChartGroupField
                End Get

                Set(ByVal value As Boolean)
                    SetProperty(showChartGroupField, value, NameOf(ChartsDemoRibbonViewModel.ShowChartGroup))
                End Set
            End Property

            Public Property ShowRunChartDesignerButton As Boolean
                Get
                    Return showRunChartDesignerButtonField
                End Get

                Set(ByVal value As Boolean)
                    SetProperty(showRunChartDesignerButtonField, value, NameOf(ChartsDemoRibbonViewModel.ShowRunChartDesignerButton), New Action(AddressOf SetChartGroupVisibility))
                End Set
            End Property

            Public ReadOnly Property ShowExportButton As Boolean
                Get
                    Return True
                End Get
            End Property

            Public Sub New()
                RunChartDesignerCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControl = TryCast(Chart, ChartControl)
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't run the designer because the Chart is null or it is a 3D Chart Control.")
                        Return
                    End If

                    Dim designer As ChartDesigner = New ChartDesigner(chartControl)
                    Dim window As Window = Window.GetWindow(Chart)
                    designer.Show(window)
                End Sub, False)
                ExportToImageCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "BMP Image|*.BMP|Gif Image|*.GIF|JPEG Image|*.JPG|PNG Image|*.PNG|TIFF Image|*.TIFF"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim options As ImageExportOptions = New ImageExportOptions()
                        Select Case SaveFileDialogService.FilterIndex
                            Case 1
                                options.Format = DXImageFormat.Bmp
                            Case 2
                                options.Format = DXImageFormat.Gif
                            Case 3
                                options.Format = DXImageFormat.Jpeg
                            Case 4
                                options.Format = DXImageFormat.Png
                            Case 5
                                options.Format = DXImageFormat.Tiff
                            Case Else
                                MessageBoxService.ShowMessage("The selected format is not supported.", "Error", MessageButton.OK, MessageIcon.Error)
                                Return
                        End Select

                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToImage(fileName, options)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToXlsxCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "XLSX Document|*.XLSX"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToXlsx(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToXlsCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "XLS Document|*.XLS"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToXls(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToDocxCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "DOCX Document|*.DOCX"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToDocx(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToMhtCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "MHT Document|*.MHT"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToMht(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToHtmlCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "HTML Document|*.HTML"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToHtml(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToPdfCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "PDF Document|*.PDF"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToPdf(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToRtfCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "RTF Document|*.RTF"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToRtf(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
                ExportToXpsCommand = New DelegateCommand(Sub()
                    Dim chartControl As ChartControlBase = Chart
                    If chartControl Is Nothing Then
                        ChartDebug.WriteWarning("ChartDemoRibbon can't be exported because the Chart is null.")
                        Return
                    End If

                    SaveFileDialogService.DefaultFileName = sTR_DXChart
                    SaveFileDialogService.Filter = "XPS Document|*.XPS"
                    Dim dialogResult = SaveFileDialogService.ShowDialog()
                    If Not dialogResult Then
                        Return
                    Else
                        Dim fileName = SaveFileDialogService.GetFullFileName()
                        chartControl.ExportToXps(fileName)
                        AskAndOpenResultFile(fileName)
                    End If
                End Sub, False)
            End Sub

            Private Sub AskAndOpenResultFile(ByVal fileName As String)
                If MessageBoxService.ShowMessage("Do you want to open the result file?", String.Empty, MessageButton.YesNo, MessageIcon.Question) = MessageResult.Yes Then ProcessLaunchHelper.StartConfirmed(fileName)
            End Sub

            Private Sub SetChartGroupVisibility()
                ShowChartGroup = Chart IsNot Nothing AndAlso (ShowPaletteButton OrElse ShowRunChartDesignerButton OrElse ShowExportButton)
            End Sub
        End Class
    End Class
End Namespace
