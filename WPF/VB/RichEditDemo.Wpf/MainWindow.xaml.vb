Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.RichEdit

Namespace RichEditDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.RichEdit, PreloadCategories.Grid, PreloadCategories.PropertyGrid, PreloadCategories.Printing)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As CommonRibbon
            Get
                Return CType(Instance.Ribbon, CommonRibbon)
            End Get
        End Property

        Public Sub New()
            Instance = Me
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
        End Sub
    End Class

    Public Class CommonRibbon
        Inherits Control

        Public Property RibbonCategory_HeaderFooterTools As Boolean?

        Public Property RibbonCategory_TableTools As Boolean?

        Public Property RibbonCategory_PictureTools As Boolean?

        Public Property RibbonPage_MailMerge As Boolean?

        Public Property RibbonPage_File As Boolean?

        Public Property RibbonPage_Home As Boolean?

        Public Property RibbonPage_Insert As Boolean?

        Public Property RibbonPage_PageLayout As Boolean?

        Public Property RibbonPage_References As Boolean?

        Public Property RibbonPage_Review As Boolean?

        Public Property RibbonPage_View As Boolean?

        Public Property RibbonGroup_View_DocumentViews As Boolean?

        Public Property RibbonGroup_View_Show As Boolean?

        Public Property RibbonGroup_Review_Proofing As Boolean?

        Public Property RibbonGroup_Review_Protect As Boolean?

        Public Property RibbonGroup_Review_Comment As Boolean?

        Public Property RibbonGroup_Review_Tracking As Boolean?

        Public Property RibbonGroup_Review_Changes As Boolean?

        Public Property RibbonGroup_MailMerge_WriteInsertFields As Boolean?

        Public Property RibbonItem_MailMerge_PreviewResults_ViewMergedData As Boolean?

        Public Property RibbonItem_MailMerge_PreviewResults_FirstRecord As Boolean?

        Public Property RibbonItem_MailMerge_PreviewResults_PreviousRecord As Boolean?

        Public Property RibbonItem_MailMerge_PreviewResults_NextRecord As Boolean?

        Public Property RibbonItem_MailMerge_PreviewResults_LastRecord As Boolean?

        Public Property RibbonItem_MailMerge_PreviewResults_MailMerge As Boolean?

        Public Property SelectedPage As String

        Private Property SetSelectedPage As Boolean

        Private Property Ribbon As RibbonControl

        Public Sub Init(ByVal richEdit As RichEditControl)
            ClearState()
            If richEdit Is Nothing Then
                MergingProperties.SetElementMergingBehavior(Me, ElementMergingBehavior.None)
                Visibility = System.Windows.Visibility.Collapsed
            Else
                MergingProperties.SetElementMergingBehavior(Me, ElementMergingBehavior.InternalWithExternal)
                Visibility = System.Windows.Visibility.Visible
            End If

            Dim oldRichEdit = RichEditControl.GetRichEdit(Me)
            If oldRichEdit Is Nothing Then
                RichEditControl.SetRichEdit(Me, richEdit)
            End If

            Dispatcher.InvokeAsync(Sub()
                RichEditControl.SetRichEdit(Me, richEdit)
                UpdateState()
            End Sub)
        End Sub

        Public Overrides Sub OnApplyTemplate()
            If Ribbon IsNot Nothing Then
                RemoveHandler Ribbon.SelectedPageChanged, AddressOf OnSelectedPageChanged
            End If

            MyBase.OnApplyTemplate()
            Ribbon = LayoutTreeHelper.GetVisualChildren(Me).OfType(Of RibbonControl)().FirstOrDefault()
            If Ribbon Is Nothing Then Return
            AddHandler Ribbon.SelectedPageChanged, AddressOf OnSelectedPageChanged
            UpdateState()
        End Sub

        Private Sub OnSelectedPageChanged(ByVal sender As Object, ByVal e As RibbonPropertyChangedEventArgs)
            UpdatePageState()
        End Sub

        Private Sub UpdateState()
            If Ribbon Is Nothing Then Return
            SetPageCategoryVisibility(DefaultBarItemNames.RibbonCategory_HeaderFooterTools, RibbonCategory_HeaderFooterTools)
            SetPageCategoryVisibility(DefaultBarItemNames.RibbonCategory_TableTools, RibbonCategory_TableTools)
            SetPageCategoryVisibility(DefaultBarItemNames.RibbonCategory_PictureTools, RibbonCategory_PictureTools)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_MailMerge, RibbonPage_MailMerge)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_File, RibbonPage_File)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Home, RibbonPage_Home)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Insert, RibbonPage_Insert)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_PageLayout, RibbonPage_PageLayout)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_References, RibbonPage_References)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Review, RibbonPage_Review)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_View, RibbonPage_View)
            UpdatePageState()
            UpdateSelectionState()
        End Sub

        Private Sub UpdatePageState()
            Dispatcher.InvokeAsync(New Action(AddressOf UpdatePageStateCore))
        End Sub

        Private Sub UpdatePageStateCore()
            If Ribbon Is Nothing OrElse Ribbon.SelectedPage Is Nothing Then Return
            Dim page = Ribbon.SelectedPage
            If Equals(page.Name, DefaultBarItemNames.RibbonPage_View) Then
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_DocumentViews, RibbonGroup_View_DocumentViews)
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_Show, RibbonGroup_View_Show)
            ElseIf Equals(page.Name, DefaultBarItemNames.RibbonPage_Review) Then
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Proofing, RibbonGroup_Review_Proofing)
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Protect, RibbonGroup_Review_Protect)
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Comment, RibbonGroup_Review_Comment)
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Tracking, RibbonGroup_Review_Tracking)
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Changes, RibbonGroup_Review_Changes)
            ElseIf Equals(page.Name, DefaultBarItemNames.RibbonPage_MailMerge) Then
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_MailMerge_WriteInsertFields, RibbonGroup_MailMerge_WriteInsertFields)
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_ViewMergedData, RibbonItem_MailMerge_PreviewResults_ViewMergedData)
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_FirstRecord, RibbonItem_MailMerge_PreviewResults_FirstRecord)
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_PreviousRecord, RibbonItem_MailMerge_PreviewResults_PreviousRecord)
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_NextRecord, RibbonItem_MailMerge_PreviewResults_NextRecord)
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_LastRecord, RibbonItem_MailMerge_PreviewResults_LastRecord)
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_MailMerge, RibbonItem_MailMerge_PreviewResults_MailMerge)
            End If
        End Sub

        Private Sub UpdateSelectionState()
            If Not Equals(SelectedPage, Nothing) Then
                Dim page = GetPage(SelectedPage)
                SetPageIsSelected(page, True)
                Return
            End If

            If SetSelectedPage Then
                Dim page = GetPages().First(Function(x) x.IsVisible)
                SetPageIsSelected(page, True)
            End If
        End Sub

        Private Sub ClearState()
            RibbonCategory_HeaderFooterTools = Nothing
            RibbonCategory_TableTools = Nothing
            RibbonCategory_PictureTools = Nothing
            RibbonPage_MailMerge = Nothing
            RibbonPage_File = Nothing
            RibbonPage_Home = Nothing
            RibbonPage_Insert = Nothing
            RibbonPage_PageLayout = Nothing
            RibbonPage_References = Nothing
            RibbonPage_Review = Nothing
            RibbonPage_View = Nothing
            RibbonGroup_View_DocumentViews = Nothing
            RibbonGroup_View_Show = Nothing
            RibbonGroup_Review_Proofing = Nothing
            RibbonGroup_Review_Protect = Nothing
            RibbonGroup_Review_Comment = Nothing
            RibbonGroup_Review_Tracking = Nothing
            RibbonGroup_Review_Changes = Nothing
            RibbonGroup_MailMerge_WriteInsertFields = Nothing
            RibbonItem_MailMerge_PreviewResults_ViewMergedData = Nothing
            RibbonItem_MailMerge_PreviewResults_FirstRecord = Nothing
            RibbonItem_MailMerge_PreviewResults_PreviousRecord = Nothing
            RibbonItem_MailMerge_PreviewResults_NextRecord = Nothing
            RibbonItem_MailMerge_PreviewResults_LastRecord = Nothing
            RibbonItem_MailMerge_PreviewResults_MailMerge = Nothing
            SelectedPage = Nothing
            SetSelectedPage = If(Ribbon IsNot Nothing, Ribbon.SelectedPage IsNot Nothing, False)
        End Sub

        Private Sub SetPageCategoryVisibility(ByVal name As String, ByVal isVisible As Boolean?)
            Dim category = GetCategory(name)
            If category Is Nothing Then Throw New InvalidOperationException()
            If isVisible.HasValue Then
                category.SetCurrentValue(RibbonPageCategoryBase.IsVisibleProperty, isVisible.Value)
            Else
                category.ClearValue(RibbonPageCategoryBase.IsVisibleProperty)
            End If
        End Sub

        Private Sub SetPageVisibility(ByVal name As String, ByVal isVisible As Boolean?)
            Dim page = GetPage(name)
            If page Is Nothing Then Throw New InvalidOperationException()
            If isVisible.HasValue Then
                page.SetCurrentValue(RibbonPage.IsVisibleProperty, isVisible.Value)
            Else
                page.ClearValue(RibbonPage.IsVisibleProperty)
            End If
        End Sub

        Private Sub SetPageGroupVisibility(ByVal page As RibbonPage, ByVal name As String, ByVal isVisible As Boolean?)
            Dim group = GetPageGroup(page, name)
            If group Is Nothing Then Throw New InvalidOperationException()
            If isVisible.HasValue Then
                group.SetCurrentValue(RibbonPageGroup.IsVisibleProperty, isVisible.Value)
            Else
                group.ClearValue(RibbonPageGroup.IsVisibleProperty)
            End If
        End Sub

        Private Sub SetPageItemVisibility(ByVal page As RibbonPage, ByVal name As String, ByVal isVisible As Boolean?)
            Dim item = GetItem(page, name)
            If item Is Nothing Then Throw New InvalidOperationException()
            If isVisible.HasValue Then
                item.SetCurrentValue(BarItem.IsVisibleProperty, isVisible.Value)
            Else
                item.ClearValue(BarItem.IsVisibleProperty)
            End If
        End Sub

        Private Sub SetPageIsSelected(ByVal page As RibbonPage, ByVal isSelected As Boolean)
            page.SetCurrentValue(RibbonPage.IsSelectedProperty, isSelected)
        End Sub

        Private Function GetCategory(ByVal name As String) As RibbonPageCategoryBase
            Return Ribbon.ActualCategories.FirstOrDefault(Function(x) Equals(x.Name, name))
        End Function

        Private Function GetPage(ByVal name As String) As RibbonPage
            Return GetPages().FirstOrDefault(Function(x) Equals(x.Name, name))
        End Function

        Private Function GetPageGroup(ByVal page As RibbonPage, ByVal name As String) As RibbonPageGroup
            Return page.ActualGroups.FirstOrDefault(Function(x) Equals(x.Name, name))
        End Function

        Private Function GetItem(ByVal page As RibbonPage, ByVal name As String) As BarItem
            Return GetPageItems(page).FirstOrDefault(Function(x) Equals(x.Name, name))
        End Function

        Private Function GetPages() As IEnumerable(Of RibbonPage)
            Return Ribbon.ActualCategories.SelectMany(Function(x) x.ActualPages)
        End Function

        Private Function GetPageItems(ByVal page As RibbonPage) As IEnumerable(Of BarItem)
            Return page.ActualGroups.SelectMany(Function(x) x.Items).OfType(Of BarItem)()
        End Function
    End Class
End Namespace
