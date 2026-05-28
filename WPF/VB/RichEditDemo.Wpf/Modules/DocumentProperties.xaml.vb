Imports System
Imports DevExpress.Xpf.Bars
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo

    Public Partial Class DocumentProperties
        Inherits RichEditDemoModule

        Public Shared ReadOnly Properties As DocumentPropertyInfo() = New DocumentPropertyInfo() {New DocumentPropertyInfo("Category", "DOCPROPERTY Category"), New DocumentPropertyInfo("Created", "CREATEDATE"), New DocumentPropertyInfo("Creator", "AUTHOR"), New DocumentPropertyInfo("Description", "COMMENTS"), New DocumentPropertyInfo("Keywords"), New DocumentPropertyInfo("LastModifiedBy", "LASTSAVEDBY"), New DocumentPropertyInfo("LastPrinted", "PRINTDATE"), New DocumentPropertyInfo("Modified", "SAVEDATE"), New DocumentPropertyInfo("Revision", "REVNUM"), New DocumentPropertyInfo("Subject"), New DocumentPropertyInfo("Title")}

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(richEdit)
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = False
            MainWindow.CommonRibbon.RibbonCategory_TableTools = False
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = False
            MainWindow.CommonRibbon.RibbonPage_Home = False
            MainWindow.CommonRibbon.RibbonPage_Insert = False
            MainWindow.CommonRibbon.RibbonPage_PageLayout = False
            MainWindow.CommonRibbon.RibbonPage_References = False
            MainWindow.CommonRibbon.RibbonPage_Review = False
            MainWindow.CommonRibbon.RibbonPage_View = False
            MainWindow.CommonRibbon.RibbonGroup_MailMerge_WriteInsertFields = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_ViewMergedData = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_FirstRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_PreviousRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_NextRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_LastRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_MailMerge = False
            Document.Fields.Update()
        End Sub

        Private ReadOnly Property Document As Document
            Get
                Return richEdit.Document
            End Get
        End Property

        Private Sub OnDocumentPropertiesChanged(ByVal sender As Object, ByVal e As EventArgs)
            Document.Fields.Update()
        End Sub

        Private Sub OnCalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
            If e.Arguments.Count = 0 OrElse Not Equals(e.VariableName, "CustomProperty") Then Return
            Dim name As String = e.Arguments(0).Value
            Dim customProperty As Object = Document.CustomProperties(name)
            If customProperty IsNot Nothing Then e.Value = customProperty.ToString()
            e.Handled = True
        End Sub

        Private Sub OnPropertyItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            Dim propertyInfo = CType(e.Item.DataContext, DocumentPropertyInfo)
            Document.BeginUpdate()
            Dim field As Field = Document.Fields.Create(richEdit.Document.CaretPosition, propertyInfo.Name)
            field.Update()
            Document.EndUpdate()
        End Sub
    End Class
End Namespace
