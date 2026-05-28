Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Printing
Imports DevExpress.Printing.Native.PrintEditor
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Printing.PreviewControl.Native
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports

Namespace DevExpress.DevAV.Controls

    Public Class BackstageDocumentPreview
        Inherits System.Windows.Controls.Control

        Shared Sub New()
            Call System.Windows.FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.FrameworkPropertyMetadata(GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview)))
        End Sub

        Friend Shared ReadOnly IsPrintersAvailablePropertyKey As System.Windows.DependencyPropertyKey = System.Windows.DependencyProperty.RegisterReadOnly("IsPrintersAvailable", GetType(Boolean), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.FrameworkPropertyMetadata(True))

        Public Shared ReadOnly IsPrintersAvailableProperty As System.Windows.DependencyProperty = DevExpress.DevAV.Controls.BackstageDocumentPreview.IsPrintersAvailablePropertyKey.DependencyProperty

        Public Property IsPrintersAvailable As Boolean
            Get
                Return CBool(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.IsPrintersAvailableProperty))
            End Get

            Protected Set(ByVal value As Boolean)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.IsPrintersAvailablePropertyKey, value)
            End Set
        End Property

        Public Shared ReadOnly DocumentSourceProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("DocumentSource", GetType(DevExpress.XtraReports.IReport), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Property DocumentSource As IReport
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.DocumentSourceProperty), DevExpress.XtraReports.IReport)
            End Get

            Set(ByVal value As IReport)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.DocumentSourceProperty, value)
            End Set
        End Property

        Friend Shared ReadOnly PrintersErrorMessagePropertyKey As System.Windows.DependencyPropertyKey = System.Windows.DependencyProperty.RegisterReadOnly("PrintersErrorMessage", GetType(String), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.FrameworkPropertyMetadata(String.Empty))

        Public Shared ReadOnly PrintersErrorMessageProperty As System.Windows.DependencyProperty = DevExpress.DevAV.Controls.BackstageDocumentPreview.PrintersErrorMessagePropertyKey.DependencyProperty

        Public Property PrintersErrorMessage As String
            Get
                Return CStr(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.PrintersErrorMessageProperty))
            End Get

            Protected Set(ByVal value As String)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.PrintersErrorMessagePropertyKey, value)
            End Set
        End Property

        Public Property SelectedPrinter As PrinterViewModel
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.SelectedPrinterProperty), DevExpress.DevAV.Controls.PrinterViewModel)
            End Get

            Set(ByVal value As PrinterViewModel)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.SelectedPrinterProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SelectedPrinterProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("SelectedPrinter", GetType(DevExpress.DevAV.Controls.PrinterViewModel), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Property Printers As List(Of DevExpress.DevAV.Controls.PrinterViewModel)
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.PrintersProperty), System.Collections.Generic.List(Of DevExpress.DevAV.Controls.PrinterViewModel))
            End Get

            Protected Set(ByVal value As List(Of DevExpress.DevAV.Controls.PrinterViewModel))
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.PrintersPropertyKey, value)
            End Set
        End Property

        Private Shared ReadOnly PrintersPropertyKey As System.Windows.DependencyPropertyKey = System.Windows.DependencyProperty.RegisterReadOnly("Printers", GetType(System.Collections.Generic.List(Of DevExpress.DevAV.Controls.PrinterViewModel)), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Shared ReadOnly PrintersProperty As System.Windows.DependencyProperty = DevExpress.DevAV.Controls.BackstageDocumentPreview.PrintersPropertyKey.DependencyProperty

        Public Property OptionsMode As BackstagePreviewOptionsMode
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.OptionsModeProperty), DevExpress.DevAV.Controls.BackstagePreviewOptionsMode)
            End Get

            Set(ByVal value As BackstagePreviewOptionsMode)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.OptionsModeProperty, value)
            End Set
        End Property

        Public Shared ReadOnly OptionsModeProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("OptionsMode", GetType(DevExpress.DevAV.Controls.BackstagePreviewOptionsMode), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(DevExpress.DevAV.Controls.BackstagePreviewOptionsMode.Printing, Sub(d, e) CType(d, DevExpress.DevAV.Controls.BackstageDocumentPreview).IsPrintOptionsVisible = CType(d, DevExpress.DevAV.Controls.BackstageDocumentPreview).OptionsMode = DevExpress.DevAV.Controls.BackstagePreviewOptionsMode.Printing))

        Public Property IsPrintOptionsVisible As Boolean
            Get
                Return CBool(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.IsPrintOptionsVisibleProperty))
            End Get

            Protected Set(ByVal value As Boolean)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.IsPrintOptionsVisiblePropertyKey, value)
            End Set
        End Property

        Private Shared ReadOnly IsPrintOptionsVisiblePropertyKey As System.Windows.DependencyPropertyKey = System.Windows.DependencyProperty.RegisterReadOnly("IsPrintOptionsVisible", GetType(Boolean), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(True))

        Public Shared ReadOnly IsPrintOptionsVisibleProperty As System.Windows.DependencyProperty = DevExpress.DevAV.Controls.BackstageDocumentPreview.IsPrintOptionsVisiblePropertyKey.DependencyProperty

        Public Property SelectedFormat As String
            Get
                Return CStr(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.SelectedFormatProperty))
            End Get

            Set(ByVal value As String)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.SelectedFormatProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SelectedFormatProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("SelectedFormat", GetType(String), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Property ExportFormats As IEnumerable(Of String)
            Get
                Return CType(Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.ExportFormatsProperty), System.Collections.Generic.IEnumerable(Of String))
            End Get

            Protected Set(ByVal value As IEnumerable(Of String))
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.ExportFormatsPropertyKey, value)
            End Set
        End Property

        Private Shared ReadOnly ExportFormatsPropertyKey As System.Windows.DependencyPropertyKey = System.Windows.DependencyProperty.RegisterReadOnly("ExportFormats", GetType(System.Collections.Generic.IEnumerable(Of String)), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Shared ReadOnly ExportFormatsProperty As System.Windows.DependencyProperty = DevExpress.DevAV.Controls.BackstageDocumentPreview.ExportFormatsPropertyKey.DependencyProperty

        Public Property CustomSettingsHeader As Object
            Get
                Return Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.CustomSettingsHeaderProperty)
            End Get

            Set(ByVal value As Object)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.CustomSettingsHeaderProperty, value)
            End Set
        End Property

        Public Shared ReadOnly CustomSettingsHeaderProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("CustomSettingsHeader", GetType(Object), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Property CustomSettings As Object
            Get
                Return Me.GetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.CustomSettingsProperty)
            End Get

            Set(ByVal value As Object)
                Me.SetValue(DevExpress.DevAV.Controls.BackstageDocumentPreview.CustomSettingsProperty, value)
            End Set
        End Property

        Public Shared ReadOnly CustomSettingsProperty As System.Windows.DependencyProperty = System.Windows.DependencyProperty.Register("CustomSettings", GetType(Object), GetType(DevExpress.DevAV.Controls.BackstageDocumentPreview), New System.Windows.PropertyMetadata(CType(Nothing, System.Windows.PropertyChangedCallback)))

        Public Sub New()
            Try
                Me.Printers = New System.Collections.Generic.List(Of DevExpress.DevAV.Controls.PrinterViewModel)()
                Me.LoadPrinters()
            Catch ex As System.Exception
                Me.IsPrintersAvailable = False
                Me.PrintersErrorMessage = ex.Message
            End Try

            Me.ExportFormats = System.[Enum].GetNames(GetType(DevExpress.XtraPrinting.ExportFormat)).Except(New String() {"Prnx"})
            Me.SelectedFormat = Me.ExportFormats.FirstOrDefault()
        End Sub

        Private Sub LoadPrinters()
            Dim imageConverter = New DevExpress.Xpf.Printing.PreviewControl.Native.PrinterTypeToImageUriConverter()
            Dim printContainer = New DevExpress.Printing.PrinterItemContainer()
            For Each item In printContainer.Items
                Dim model = New DevExpress.DevAV.Controls.PrinterViewModel() With {.IsOffline = item.PrinterType.HasFlag(DevExpress.Printing.Native.PrintEditor.PrinterType.Offline), .DisplayName = item.DisplayName, .Name = item.FullName, .Status = item.Status, .ImageUri = imageConverter.Convert(CObj((item.PrinterType)), CType((Nothing), System.Type), CObj((Nothing)), CType((Nothing), System.Globalization.CultureInfo)).ToString()}
                Me.Printers.Add(model)
                If item.PrinterType.HasFlag(DevExpress.Printing.Native.PrintEditor.PrinterType.[Default]) AndAlso Not model.IsOffline Then Me.SelectedPrinter = model
            Next

            If Me.SelectedPrinter Is Nothing Then Me.SelectedPrinter = Me.Printers.FirstOrDefault(Function(x) Not x.IsOffline)
        End Sub
    End Class

    Public Class PrinterViewModel

        Public Property Name As String

        Public Property DisplayName As String

        Public Property ImageUri As String

        Public Property Status As String

        Public Property IsOffline As Boolean
    End Class

    Public Enum BackstagePreviewOptionsMode
        Printing
        Export
    End Enum
End Namespace
