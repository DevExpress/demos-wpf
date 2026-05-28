Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.XtraRichEdit.API.Native

Namespace RichEditDemo

    Public Partial Class DocumentProtection
        Inherits RichEditDemoModule

        Private ReadOnly userService As UserService = New UserService()

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(richEdit)
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = False
            MainWindow.CommonRibbon.RibbonCategory_TableTools = False
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = False
            MainWindow.CommonRibbon.RibbonPage_MailMerge = False
            MainWindow.CommonRibbon.RibbonPage_File = False
            MainWindow.CommonRibbon.RibbonPage_Home = False
            MainWindow.CommonRibbon.RibbonPage_Insert = False
            MainWindow.CommonRibbon.RibbonPage_PageLayout = False
            MainWindow.CommonRibbon.RibbonPage_References = False
            MainWindow.CommonRibbon.RibbonPage_View = False
            MainWindow.CommonRibbon.RibbonGroup_Review_Proofing = False
            richEdit.ReplaceService(userService)
        End Sub

        Private Sub RichEditControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            AddHandler richEdit.DocumentLoaded, AddressOf RichEditControl_DocumentLoaded
            richEdit.DocumentSource = DemoUtils.GetRelativePath("DocumentProtection.docx")
        End Sub

        Private Sub RichEditControl_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            Dim rangePermissions As RangePermissionCollection = richEdit.Document.BeginUpdateRangePermissions()
            richEdit.Document.CancelUpdateRangePermissions(rangePermissions)
            Dim users As List(Of String) = rangePermissions.[Select](Function(rangePermission) rangePermission.UserName).Where(Function(name) Not String.IsNullOrEmpty(name)).Distinct().ToList()
            userService.Update(users)
            cbUserName.ItemsSource = users
            If users.Count > 0 Then cbUserName.EditValue = users(0)
        End Sub
    End Class
End Namespace
