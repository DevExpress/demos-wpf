Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Windows.Media
Imports System.Windows.Threading
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.RichEdit
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Layout
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.XtraRichEdit.Services
Imports RichEditDemo.Modules.MailMerge

Namespace RichEditDemo

    Public Partial Class MailMergeMasterDetail
        Inherits RichEditDemoModule

        Private Property HighlightingEnabled As Boolean
            Get
                Return highlightingEnabledField
            End Get

            Set(ByVal value As Boolean)
                If highlightingEnabledField <> value Then
                    highlightingEnabledField = value
                    Dispatcher.Invoke(New Action(AddressOf templateRichEdit.Refresh))
                End If
            End Set
        End Property

        Private ReadOnly Property Customers As IList(Of Customer)
            Get
                Return customersField.Value
            End Get
        End Property

        Private Shared ReadOnly regionHighlightType As CustomDrawHighlightType = CustomDrawHighlightType.Fill

        Private rootRegionInfo As MailMergeRegionInfo

        Private mailMergeOptions As MailMergeOptions

        Private regionHighlights As List(Of CustomDrawHighlightInfo) = New List(Of CustomDrawHighlightInfo)()

        Private highlightingEnabledField As Boolean

        Private hasValidResult As Boolean

        Private resultRichEditExceptionsHandler As RichEditDemoExceptionsHandler

        Private customersField As Lazy(Of IList(Of Customer)) = New Lazy(Of IList(Of Customer))(Function() DevExpress.DemoData.NWindDataProvider.Customers)

        Public Sub New()
            InitializeComponent()
            SetActiveRichEdit(templateRichEdit)
            mailMergeOptions = templateRichEdit.CreateMailMergeOptions()
            Dim dataSource = CreateDataSource()
            recordSelectorGridControl.ItemsSource = dataSource
            templateRichEdit.MailMergeOptions.DataSource = dataSource
            resultRichEditExceptionsHandler = New RichEditDemoExceptionsHandler(resultRichEdit)
            resultRichEditExceptionsHandler.Install()
            AddHandler templateRichEdit.Document.ContentChanged, AddressOf Document_ContentChanged
            UpdateRegionHierarchy()
        End Sub

        Private Sub SetActiveRichEdit(ByVal richEditControl As RichEditControl)
            MainWindow.CommonRibbon.Init(Nothing)
            MainWindow.CommonRibbon.Init(richEditControl)
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = False
            MainWindow.CommonRibbon.RibbonCategory_TableTools = False
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = False
            MainWindow.CommonRibbon.RibbonPage_Insert = False
            MainWindow.CommonRibbon.RibbonPage_PageLayout = False
            MainWindow.CommonRibbon.RibbonPage_References = False
            MainWindow.CommonRibbon.RibbonPage_Review = False
            MainWindow.CommonRibbon.RibbonPage_View = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_FirstRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_LastRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_NextRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_PreviousRecord = False
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_ViewMergedData = False
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_MailMerge
        End Sub

        Private Function RunMailMerge() As Boolean
            If hasValidResult Then Return True
            Dim errorMsg As String = Nothing
            Dim success As Boolean = False
            Try
                templateRichEdit.Document.MailMerge(mailMergeOptions, resultRichEdit.Document)
                success = True
            Catch ex As Exception
                errorMsg = ex.Message
                success = False
            End Try

            hasValidResult = success
            If success Then Return True
            tabControl.SelectedItem = templateTabPage
            DemoUtils.ShowErrorMessage($"Mail Merge failed:{Environment.NewLine} {errorMsg}")
            Return False
        End Function

        Private Sub RichEdit_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim sourceRichEdit = CType(sender, RichEditControl)
            Dim commandFactory = sourceRichEdit.GetService(Of IRichEditCommandFactoryService)()
            If commandFactory IsNot Nothing AndAlso Not(TypeOf commandFactory Is MailMergeCommandFactoryService) Then sourceRichEdit.ReplaceService(Of IRichEditCommandFactoryService)(New MailMergeCommandFactoryService(commandFactory, templateRichEdit, mailMergeOptions))
        End Sub

        Private Function CreateDataSource() As IEnumerable(Of Object)
            Return Customers.[Select](Function(c)
                c.Orders = c.Orders.[Select](Function(o)
                    o.OrderDetails = o.OrderDetails.ToList()
                    Return o
                End Function).ToList()
                Return c
            End Function).ToList()
        End Function

        Private Sub UpdateRegionHierarchy()
            Try
                rootRegionInfo = templateRichEdit.Document.GetRegionHierarchy(mailMergeOptions.RegionStartTag, mailMergeOptions.RegionEndTag)
            Catch
                rootRegionInfo = Nothing
            End Try

            regionHighlights.Clear()
            AddRegionRanges(rootRegionInfo)
        End Sub

        Private Sub AddRegionRanges(ByVal mailMergeRegionInfo As MailMergeRegionInfo)
            If mailMergeRegionInfo Is Nothing Then Return
            If mailMergeRegionInfo.ParentRegion IsNot Nothing Then
                Dim regionRange As FixedRange = GetRegionRange(mailMergeRegionInfo)
                Dim highlightColor As Color = GetHighlightColor(mailMergeRegionInfo)
                regionHighlights.Add(New CustomDrawHighlightInfo(regionRange, regionHighlightType, highlightColor))
            End If

            For Each childRegion In mailMergeRegionInfo.Regions
                AddRegionRanges(childRegion)
            Next
        End Sub

        Private Function GetRegionRange(ByVal mailMergeRegionInfo As MailMergeRegionInfo) As FixedRange
            Dim regionStartFieldRange As DocumentRange = mailMergeRegionInfo.StartField.Range.GetAsFrozen()
            Dim regionEndFieldRange As DocumentRange = mailMergeRegionInfo.EndField.Range.GetAsFrozen()
            Dim regionLength As Integer = regionEndFieldRange.End.ToInt() - regionStartFieldRange.Start.ToInt()
            Return New FixedRange(regionStartFieldRange.Start.ToInt(), regionLength)
        End Function

        Private Shared Function GetHighlightColor(ByVal region As MailMergeRegionInfo) As Color
            If region.ParentRegion Is Nothing Then Return Colors.Transparent
            Select Case region.Name
                Case "Orders"
                    Return Color.FromArgb(CByte(0.4 * 255), 129, 212, 250)
                Case "OrderDetails"
                    Return Color.FromArgb(CByte(1 * 255), 255, 106, 0)
                Case Else
                    Return Colors.Brown
            End Select
        End Function

        Private Sub recordSelectorGridControl_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Grid.GridSelectionChangedEventArgs)
            mailMergeOptions.DataSource = recordSelectorGridControl.SelectedItems
            hasValidResult = False
            If resultRichEdit.IsVisible AndAlso Not RunMailMerge() Then tabControl.SelectedItem = templateTabPage
        End Sub

        Private Sub Document_ContentChanged(ByVal sender As Object, ByVal e As EventArgs)
            UpdateRegionHierarchy()
            hasValidResult = False
        End Sub

        Private Sub templateRichEdit_BeforePagePaint(ByVal sender As Object, ByVal e As BeforePagePaintEventArgs)
            If Not HighlightingEnabled OrElse e.CanvasOwnerType = CanvasOwnerType.Printer OrElse regionHighlights.Count = 0 Then Return
            Dim visibleRegionHighlights = regionHighlights.Where(Function(rh) e.Page.IsRangeVisible(rh.HighlightRange)).ToList()
            Dim highlightPainter = New CustomDrawPagePainter(templateRichEdit) With {.LayoutRowHighlights = visibleRegionHighlights}
            e.Painter = highlightPainter
        End Sub

        Private Sub HighlightRegionsBarItem_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs)
            HighlightingEnabled = If(CType(sender, BarCheckItem).IsChecked, False)
        End Sub

        Private Sub tabControl_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)
            If tabControl.SelectedItem Is resultTabPage AndAlso RunMailMerge() Then
                SetActiveRichEdit(resultRichEdit)
            ElseIf tabControl.SelectedItem Is templateTabPage Then
                SetActiveRichEdit(templateRichEdit)
            End If
        End Sub
    End Class
End Namespace
