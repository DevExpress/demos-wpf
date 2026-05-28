Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.AIIntegration.Wpf
Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Ribbon
Imports DevExpress.Xpf.RichEdit

Namespace AIDemo

    Public Partial Class SmartSearchModule

        Private window As ThemedWindow

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            AddHandler Loaded, AddressOf TurnOnSearch
            AddHandler Unloaded, AddressOf TurnOffSearch
        End Sub

        Private Sub TurnOnSearch(ByVal sender As Object, ByVal e As RoutedEventArgs)
            window = CType(System.Windows.Window.GetWindow(Me), ThemedWindow)
            If window IsNot Nothing Then
                window.SearchItemDisplayMode = SearchItemDisplayMode.Right
                window.SearchDelay = 300
                Call Interaction.GetBehaviors(window).Add(New SmartSearchBehavior())
            End If
        End Sub

        Private Sub TurnOffSearch(ByVal sender As Object, ByVal e As RoutedEventArgs)
            window.SearchItemDisplayMode = SearchItemDisplayMode.Hidden
            Dim behaviors = Interaction.GetBehaviors(window)
            Dim behavior = behaviors.OfType(Of SmartSearchBehavior)().FirstOrDefault()
            If behavior IsNot Nothing Then behaviors.Remove(behavior)
            window = Nothing
        End Sub
    End Class

    Public Class SmartSearchDemoRibbon
        Inherits Control

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Dim ribbon = LayoutTreeHelper.GetVisualChildren(Me).OfType(Of RibbonControl)().FirstOrDefault()
            If ribbon Is Nothing Then Return
            For Each page In ribbon.ActualCategories.SelectMany(Function(x) x.ActualPages)
                page.SetCurrentValue(RibbonPage.IsVisibleProperty, IsPageVisisble(page.Name))
            Next

            For Each category In ribbon.ActualCategories.Where(New Func(Of RibbonPageCategoryBase, Boolean)(AddressOf ShouldHideCategory))
                category.SetCurrentValue(RibbonPageCategoryBase.IsVisibleProperty, False)
            Next
        End Sub

        Private Function ShouldHideCategory(ByVal category As RibbonPageCategoryBase) As Boolean
            Select Case category.Name
                Case DefaultBarItemNames.RibbonCategory_HeaderFooterTools, DefaultBarItemNames.RibbonCategory_TableTools, DefaultBarItemNames.RibbonCategory_PictureTools
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Private Function IsPageVisisble(ByVal pageName As String) As Boolean
            Select Case pageName
                Case DefaultBarItemNames.RibbonPage_MailMerge, DefaultBarItemNames.RibbonPage_File, DefaultBarItemNames.RibbonPage_Home, DefaultBarItemNames.RibbonPage_Insert, DefaultBarItemNames.RibbonPage_PageLayout, DefaultBarItemNames.RibbonPage_References, DefaultBarItemNames.RibbonPage_Review, DefaultBarItemNames.RibbonPage_View
                    Return True
                Case Else
                    Return False
            End Select
        End Function
    End Class
End Namespace
