Imports System.Collections.Generic
Imports System.Drawing
Imports DevExpress.Utils
Imports DevExpress.Xpf.DocumentViewer
Imports DevExpress.Xpf.Printing
Imports DevExpress.XtraPrinting.Localization
Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class CharacterComb
        Inherits ReportModuleViewModelBase

        Shared Sub New()
            EditingFieldExtensions.Instance.RegisterEditorInfo("Name", "Custom", "Name")
            EditingFieldExtensions.Instance.RegisterEditorInfo("NumbersAndUppercaseLetters", "Custom", "Numbers and Uppercase letters")
            EditingFieldExtensions.Instance.RegisterEditorInfo("UppercaseText", "Custom", "Uppercase Text")
            EditingFieldExtensions.Instance.RegisterEditorInfo("NumbersAndUppercaseLatinLetters", "Custom", "Numbers and Uppercase Latin letters")
            Call EditingFieldExtensions.Instance.RegisterEditorInfo("Day", PreviewStringId.EditingFieldEditorCategories_DateTime.GetString())
            Call EditingFieldExtensions.Instance.RegisterEditorInfo("Month", PreviewStringId.EditingFieldEditorCategories_DateTime.GetString())
            Call EditingFieldExtensions.Instance.RegisterEditorInfo("Year", PreviewStringId.EditingFieldEditorCategories_DateTime.GetString())
            Dim images = GetImages()
            EditingFieldExtensions.Instance.RegisterImageCollectionEditorInfo("Nationality", images, True, False, "Nationality")
        End Sub

        Public Overrides ReadOnly Property DefaultZoomMode As ZoomMode
            Get
                Return ZoomMode.ActualSize
            End Get
        End Property

        Public Overrides ReadOnly Property ShowPageMargins As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property HighlightEditingFields As Boolean
            Get
                Return True
            End Get
        End Property

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.CharacterComb.Report()
        End Function

        Private Shared Function GetImages() As IDictionary(Of String, Image)
            Dim asm = GetType(CharacterComb).Assembly
            Dim images As Dictionary(Of String, Image) = New Dictionary(Of String, Image)()
            Dim names As String() = New String() {"United_States_of_America", "United_Kingdom", "Australia", "Germany", "France", "Russia", "China", "Japan", "India", "Italy"}
            For Each name As String In names
                Dim image As Image = ResourceImageHelperCore.CreateImageFromResources(String.Format("ReportWpfDemo.Resources.Flags.{0}.png", name), asm)
                If image IsNot Nothing Then images.Add(name.Replace("_", " "), image)
            Next

            Return images
        End Function
    End Class
End Namespace
