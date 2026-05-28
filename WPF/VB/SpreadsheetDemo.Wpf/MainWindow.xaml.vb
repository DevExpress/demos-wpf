Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.Spreadsheet

Namespace SpreadsheetDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Spreadsheet, PreloadCategories.Charts, PreloadCategories.PropertyGrid, PreloadCategories.Printing)
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

        Public Property RibbonPage_Home As Boolean?

        Public Property RibbonPage_Insert As Boolean?

        Public Property RibbonPage_Formulas As Boolean?

        Public Property RibbonPage_Data As Boolean?

        Public Property RibbonPage_Review As Boolean?

        Public Property RibbonGroup_File_Info As Boolean?

        Public Property RibbonGroup_PageLayout_Show As Boolean?

        Public Property RibbonGroup_PageLayout_Print As Boolean?

        Public Property RibbonGroup_PageLayout_Arrange As Boolean?

        Public Property RibbonGroup_View_Show As Boolean?

        Public Property RibbonGroup_View_Window As Boolean?

        Public Property RibbonPage_File_Caption As String

        Public Property SelectedPage As String

        Private Property SetSelectedPage As Boolean

        Private Property Ribbon As RibbonControl

        Public Sub Init(ByVal spreadsheet As SpreadsheetControl)
            ClearState()
            If spreadsheet Is Nothing Then
                MergingProperties.SetElementMergingBehavior(Me, ElementMergingBehavior.None)
                Visibility = System.Windows.Visibility.Collapsed
            Else
                MergingProperties.SetElementMergingBehavior(Me, ElementMergingBehavior.InternalWithExternal)
                Visibility = System.Windows.Visibility.Visible
            End If

            Dim oldSpreadsheet = SpreadsheetControl.GetSpreadsheet(Me)
            If oldSpreadsheet Is Nothing Then
                SetSpreadsheet(spreadsheet)
            Else
                oldSpreadsheet.Ribbon = Nothing
            End If

            Dispatcher.InvokeAsync(Sub()
                SetSpreadsheet(spreadsheet)
                UpdateState()
            End Sub, DispatcherPriority.Loaded)
        End Sub

        Private Sub SetSpreadsheet(ByVal spreadsheet As SpreadsheetControl)
            SpreadsheetControl.SetSpreadsheet(Me, spreadsheet)
            If spreadsheet IsNot Nothing Then spreadsheet.Ribbon = Ribbon
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
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Home, RibbonPage_Home)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Insert, RibbonPage_Insert)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Formulas, RibbonPage_Formulas)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Data, RibbonPage_Data)
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Review, RibbonPage_Review)
            SetPageCaption(DefaultBarItemNames.RibbonPage_File, RibbonPage_File_Caption)
            UpdatePageState()
            UpdateSelectionState()
        End Sub

        Private Sub UpdatePageState()
            If Ribbon Is Nothing OrElse Ribbon.SelectedPage Is Nothing Then Return
            Dim page = Ribbon.SelectedPage
            If Equals(page.Name, DefaultBarItemNames.RibbonPage_File) Then
                Dispatcher.InvokeAsync(Sub() SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_File_Info, RibbonGroup_File_Info))
            ElseIf Equals(page.Name, DefaultBarItemNames.RibbonPage_PageLayout) Then
                Dispatcher.InvokeAsync(Sub()
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_PageLayout_Show, RibbonGroup_PageLayout_Show)
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_PageLayout_Print, RibbonGroup_PageLayout_Print)
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_PageLayout_Arrange, RibbonGroup_PageLayout_Arrange)
                End Sub)
            ElseIf Equals(page.Name, DefaultBarItemNames.RibbonPage_View) Then
                Dispatcher.InvokeAsync(Sub()
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_Show, RibbonGroup_View_Show)
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_Window, RibbonGroup_View_Window)
                End Sub)
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
            RibbonPage_Home = Nothing
            RibbonPage_Insert = Nothing
            RibbonPage_Formulas = Nothing
            RibbonPage_Data = Nothing
            RibbonPage_Review = Nothing
            RibbonGroup_File_Info = Nothing
            RibbonGroup_PageLayout_Show = Nothing
            RibbonGroup_PageLayout_Print = Nothing
            RibbonGroup_PageLayout_Arrange = Nothing
            RibbonGroup_View_Show = Nothing
            RibbonGroup_View_Window = Nothing
            RibbonPage_File_Caption = Nothing
            SelectedPage = Nothing
            SetSelectedPage = Ribbon?.SelectedPage IsNot Nothing
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

        Private Sub SetPageCaption(ByVal name As String, ByVal caption As String)
            Dim page = GetPage(name)
            If page Is Nothing Then Throw New InvalidOperationException()
            If Not String.IsNullOrEmpty(caption) Then
                page.SetCurrentValue(RibbonPage.CaptionProperty, caption)
            Else
                page.ClearValue(RibbonPage.CaptionProperty)
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

        Private Sub SetPageIsSelected(ByVal page As RibbonPage, ByVal isSelected As Boolean)
            page.SetCurrentValue(RibbonPage.IsSelectedProperty, isSelected)
        End Sub

        Private Function GetPage(ByVal name As String) As RibbonPage
            Return GetPages().FirstOrDefault(Function(x) Equals(x.Name, name))
        End Function

        Private Function GetPageGroup(ByVal page As RibbonPage, ByVal name As String) As RibbonPageGroup
            Return page.ActualGroups.FirstOrDefault(Function(x) Equals(x.Name, name))
        End Function

        Private Function GetPages() As IEnumerable(Of RibbonPage)
            Return Ribbon.ActualCategories.SelectMany(Function(x) x.ActualPages)
        End Function
    End Class
End Namespace
