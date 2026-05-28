Imports System.Drawing
Imports System.ServiceModel.Syndication
Imports System.Xml
Imports DevExpress.Office
Imports DevExpress.Utils
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo

    Public Partial Class DynamicContent
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(richEdit)
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = False
            MainWindow.CommonRibbon.RibbonCategory_TableTools = False
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = False
            MainWindow.CommonRibbon.RibbonPage_Insert = False
            MainWindow.CommonRibbon.RibbonPage_PageLayout = False
            MainWindow.CommonRibbon.RibbonPage_References = False
            MainWindow.CommonRibbon.RibbonPage_Review = False
            MainWindow.CommonRibbon.RibbonPage_View = False
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_MailMerge
        End Sub

        Private Sub RichEditControl_CalculateDocumentVariable(ByVal sender As Object, ByVal e As CalculateDocumentVariableEventArgs)
            If Equals(e.VariableName, "rssFeed") Then
                e.KeepLastParagraph = True
                e.Value = GenerateRssFeed()
                If e.Value IsNot Nothing Then e.Handled = True
            End If
        End Sub

        Private Function GenerateRssFeed() As RichEditDocumentServer
            Dim server As RichEditDocumentServer = New RichEditDocumentServer()
            Dim document As Document = server.Document
            Dim abstractNumberingList As AbstractNumberingList = document.AbstractNumberingLists.BulletedListTemplate.CreateNew()
            document.NumberingLists.CreateNew(abstractNumberingList.Index)
            Dim feed As SyndicationFeed = Nothing
            Try
                Using reader As XmlReader = SafeXml.CreateReader("https://community.devexpress.com/blogs/MainFeed.aspx")
                    feed = SyndicationFeed.Load(reader)
                End Using
            Catch
                Return Nothing
            End Try

            document.BeginUpdate()
            For Each item As SyndicationItem In feed.Items
                AddSyndicationItem(document, item)
            Next

            document.EndUpdate()
            Return server
        End Function

        Private Sub AddSyndicationItem(ByVal document As Document, ByVal item As SyndicationItem)
            Dim paragraph As Paragraph = document.Paragraphs.Append()
            paragraph.LineSpacing = 1F
            paragraph.ListIndex = 0
            paragraph.SpacingAfter = 3
            Dim range As DocumentRange = document.InsertText(paragraph.Range.Start, item.Title.Text)
            Dim properties As CharacterProperties = document.BeginUpdateCharacters(range)
            properties.FontSize = 12F
            properties.FontName = "Segoe UI"
            document.EndUpdateCharacters(properties)
            If item.Links.Count > 0 Then
                Dim hyperlink As Hyperlink = document.Hyperlinks.Create(range)
                hyperlink.NavigateUri = item.Links(0).Uri.ToString()
            End If

            range = document.InsertText(range.End, String.Format("{0}Published {1}", Characters.LineBreak, item.PublishDate.DateTime))
            properties = document.BeginUpdateCharacters(range)
            properties.FontSize = 8F
            properties.FontName = "Segoe UI"
            properties.ForeColor = Color.Gray
            document.EndUpdateCharacters(properties)
        End Sub
    End Class
End Namespace
