Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.ExpressionEditor
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors.ExpressionEditor.Native
Imports System.Windows.Controls.Primitives

Namespace GridDemo.Tests

    Public Class GridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function SwitchAllThemes(ByVal moduleType As Type) As Boolean
            Return False
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return moduleType IsNot GetType(ClipboardFormats) AndAlso moduleType IsNot GetType(RealtimeDataSource)
        End Function

        Protected Overrides Function TestingThemesCount() As Integer
            Return 5
        End Function
    End Class

#Region "GridDemoModulesAccessor"
    Public Class GridDemoModulesAccessor
        Inherits DemoModulesAccessor(Of GridDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub

        Public ReadOnly Property GridControl As GridControl
            Get
                Return DemoModule.GridControl
            End Get
        End Property

        Public ReadOnly Property View As GridViewBase
            Get
                Return CType(GridControl.View, GridViewBase)
            End Get
        End Property

        Public ReadOnly Property TableView As TableView
            Get
                Return CType(View, TableView)
            End Get
        End Property

        Public ReadOnly Property CardView As DevExpress.Xpf.Grid.CardView
            Get
                Return CType(View, DevExpress.Xpf.Grid.CardView)
            End Get
        End Property

        Public ReadOnly Property TableViewModule As StandardTableView
            Get
                Return CType(DemoModule, StandardTableView)
            End Get
        End Property

        Public ReadOnly Property MultiSelectionModule As MultiRowSelection
            Get
                Return CType(DemoModule, MultiRowSelection)
            End Get
        End Property

        Public ReadOnly Property CopyPasteModule As CopyPasteOperations
            Get
                Return CType(DemoModule, CopyPasteOperations)
            End Get
        End Property

        Public ReadOnly Property LargeDataSetModule As LargeDataSource
            Get
                Return CType(DemoModule, LargeDataSource)
            End Get
        End Property

        Public ReadOnly Property DragDropModule As DragDrop
            Get
                Return CType(DemoModule, DragDrop)
            End Get
        End Property

        Public ReadOnly Property HitTestModule As HitTesting
            Get
                Return CType(DemoModule, HitTesting)
            End Get
        End Property

        Public ReadOnly Property UnboundColumnsModule As UnboundColumns
            Get
                Return CType(DemoModule, UnboundColumns)
            End Get
        End Property
    End Class

#End Region
    Public MustInherit Class BaseGridDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As GridDemoModulesAccessor

        Public Sub New()
            modulesAccessor = New GridDemoModulesAccessor(Me)
        End Sub

        Public ReadOnly Property GridControl As GridControl
            Get
                Return modulesAccessor.GridControl
            End Get
        End Property

        Public ReadOnly Property View As GridViewBase
            Get
                Return modulesAccessor.View
            End Get
        End Property

        Public ReadOnly Property TableView As TableView
            Get
                Return modulesAccessor.TableView
            End Get
        End Property

        Public ReadOnly Property CardView As DevExpress.Xpf.Grid.CardView
            Get
                Return modulesAccessor.CardView
            End Get
        End Property

        Public ReadOnly Property TableViewModule As StandardTableView
            Get
                Return modulesAccessor.TableViewModule
            End Get
        End Property

        Public ReadOnly Property MultiSelectionModule As MultiRowSelection
            Get
                Return modulesAccessor.MultiSelectionModule
            End Get
        End Property

        Public ReadOnly Property CopyPasteModule As CopyPasteOperations
            Get
                Return modulesAccessor.CopyPasteModule
            End Get
        End Property

        Public ReadOnly Property LargeDataSetModule As LargeDataSource
            Get
                Return modulesAccessor.LargeDataSetModule
            End Get
        End Property

        Public ReadOnly Property DragDropModule As DragDrop
            Get
                Return modulesAccessor.DragDropModule
            End Get
        End Property

        Public ReadOnly Property HitTestModule As HitTesting
            Get
                Return modulesAccessor.HitTestModule
            End Get
        End Property

        Public ReadOnly Property UnboundColumnsModule As UnboundColumns
            Get
                Return modulesAccessor.UnboundColumnsModule
            End Get
        End Property
    End Class

    Public Class CheckDemoOptionsFixture
        Inherits BaseGridDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CreateCheckUnboundColumnsDemoActions()
            CreateCheckMultiSelectionDemoActions()
            CreateCheckTableViewDemoActions()
            CreateCheckFixedColumnsDemoActions()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub

        Private Function IsListBox(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is ListBox
        End Function

        Private Function GetListBoxEditCount(ByVal listBoxEdit As ListBoxEdit) As Integer
            Return CType(listBoxEdit.ItemsSource, Collections.IList).Count
        End Function

#Region "UnboundColumns"
        Private Sub CreateCheckUnboundColumnsDemoActions()
            AddLoadModuleActions(GetType(UnboundColumns))
            Dispatch(Sub()
                Assert.AreEqual(0, GridControl.Columns("OrderID").GroupIndex)
                Assert.AreEqual(1, GridControl.GroupCount)
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-1))
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-2))
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-3))
                Assert.IsTrue(UnboundColumnsModule.showExpressionEditorButton.IsEnabled)
                Assert.IsTrue(GridControl.Columns("Total").AllowUnboundExpressionEditor)
                Assert.IsTrue(GridControl.Columns("DiscountAmount").AllowUnboundExpressionEditor)
                Assert.AreEqual(2, UnboundColumnsModule.columnsList.Items.Count)
                Dim listBox As System.Windows.Controls.ListBox
                listBox = CType(LayoutHelper.FindElement(UnboundColumnsModule.columnsList, New Predicate(Of FrameworkElement)(AddressOf IsListBox)), System.Windows.Controls.ListBox)
                Assert.AreEqual("Discount Amount", CType(listBox.ItemContainerGenerator.ContainerFromIndex(0), System.Windows.Controls.ListBoxItem).Content)
                Assert.AreEqual("Total", CType(listBox.ItemContainerGenerator.ContainerFromIndex(1), System.Windows.Controls.ListBoxItem).Content)
                Assert.AreEqual("DiscountAmount", CType(listBox.ItemContainerGenerator.ContainerFromIndex(0), System.Windows.Controls.ListBoxItem).Tag)
                Assert.AreEqual("Total", CType(listBox.ItemContainerGenerator.ContainerFromIndex(1), System.Windows.Controls.ListBoxItem).Tag)
                Assert.AreEqual(CDec(0.0), GridControl.GetCellValue(0, GridControl.Columns("DiscountAmount")))
                Assert.AreEqual(CDec(174.0), GridControl.GetCellValue(0, GridControl.Columns("Total")))
                Assert.IsTrue(Math.Abs(CDbl(38.0) - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns("DiscountAmount")))) < 0.001)
                Assert.IsTrue(Math.Abs(CDbl(214.2) - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns("Total")))) < 0.001)
                UnboundColumnsModule.columnsList.SelectedIndex = 0
                AssertShowExpressionEditor("Round([UnitPrice] * [Quantity] - [Total])")
                UpdateLayoutAndDoEvents()
                UnboundColumnsModule.columnsList.SelectedIndex = 1
                AssertShowExpressionEditor("[UnitPrice] * [Quantity] * (1 - [Discount])")
                UpdateLayoutAndDoEvents()
                GridControl.Columns("DiscountAmount").UnboundExpression = "[UnitPrice] * [Quantity] * (1 - [Discount])+[Discount][[Discount]]"
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(DevExpress.Data.UnboundErrorObject.Value, GridControl.GetCellValue(0, GridControl.Columns("DiscountAmount")))
            End Sub)
        End Sub

        Private _expectedExpression As String

        Private expressionEditorControl As ExpressionEditorControl = Nothing

        Private Sub OnExpressionEditorLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim _dw As Window = TryCast(expressionEditorControl.Parent, Window)
            Assert.IsNotNull(_dw, "2")
            RemoveHandler expressionEditorControl.Loaded, AddressOf OnExpressionEditorLoaded
            Assert.IsTrue(expressionEditorControl.IsVisible)
            Assert.AreEqual(_expectedExpression, CType(expressionEditorControl, ISupportExpressionString).GetExpressionString(Nothing))
            _dw.DialogResult = True
            _dw.Close()
        End Sub

        Private Sub OnExpressionEditorCreated(ByVal sender As Object, ByVal e As UnboundExpressionEditorEventArgs)
            expressionEditorControl = e.ExpressionEditorControl
            Assert.IsNotNull(expressionEditorControl, "1")
            AddHandler expressionEditorControl.Loaded, AddressOf OnExpressionEditorLoaded
            RemoveHandler View.UnboundExpressionEditorCreated, AddressOf OnExpressionEditorCreated
        End Sub

        Private Sub AssertShowExpressionEditor(ByVal expectedExpression As String)
            _expectedExpression = expectedExpression
            AddHandler View.UnboundExpressionEditorCreated, AddressOf OnExpressionEditorCreated
            UIAutomationActions.ClickButton(UnboundColumnsModule.showExpressionEditorButton)
            UpdateLayoutAndDoEvents()
        End Sub

#End Region
#Region "table view"
        Private Sub CreateCheckTableViewDemoActions()
            AddLoadModuleActions(GetType(StandardTableView))
            Dispatch(Sub()
                Assert.IsTrue(TableView.AllowSorting)
                Assert.IsTrue(TableView.AllowGrouping)
                Assert.IsTrue(TableView.AllowColumnMoving)
                Assert.IsTrue(TableView.AllowResizing)
                Assert.IsTrue(TableView.AllowBestFit)
                Assert.IsTrue(TableView.ShowHorizontalLines)
                Assert.IsTrue(TableView.ShowVerticalLines)
                Dim hasStarColumn As Boolean = False
                For Each column In GridControl.Columns
                    hasStarColumn = hasStarColumn OrElse column.Width.IsStar
                    Select Case column.FieldName
                        Case "Phone"
                            Assert.IsFalse(column.AllowSorting.ToBoolean(False))
                            Assert.IsFalse(column.ActualAllowGrouping)
                        Case Else
                            Assert.IsTrue(column.ActualAllowSorting)
                            Assert.IsTrue(column.ActualAllowGrouping)
                    End Select
                Next

                Assert.IsTrue(hasStarColumn)
                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowSorting)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowSorting)
                    Assert.IsFalse(column.ActualAllowGrouping)
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit)
                EditorsActions.ToggleCheckEdit(TableViewModule.allowGroupingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowGrouping)
                For Each column In GridControl.Columns
                    Select Case column.FieldName
                        Case "Phone"
                            Assert.IsFalse(column.AllowSorting.ToBoolean(False))
                            Assert.IsFalse(column.ActualAllowGrouping)
                        Case Else
                            Assert.IsTrue(column.ActualAllowSorting)
                            Assert.IsFalse(column.ActualAllowGrouping)
                    End Select
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowMovingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowColumnMoving)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowMoving)
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowResizingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowResizing)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowResizing)
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowBestFitCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowBestFit)
                EditorsActions.ToggleCheckEdit(TableViewModule.showHorizontalLinesCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.ShowHorizontalLines)
                EditorsActions.ToggleCheckEdit(TableViewModule.showVerticalLinesCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.ShowVerticalLines)
                Assert.AreEqual(GridViewNavigationStyle.Row, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
                Assert.AreEqual(GridViewNavigationStyle.Row, View.NavigationStyle)
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridViewNavigationStyle.Cell, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 2
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridViewNavigationStyle.None, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
            End Sub)
        End Sub

#End Region
#Region "Multi-Selection"
        Private Sub CreateCheckMultiSelectionDemoActions()
            AddLoadModuleActions(GetType(MultiRowSelection))
            Dispatch(Sub()
                Assert.AreEqual(GridControl.View, TableView)
                Assert.AreEqual(GridViewNavigationStyle.Cell, TableView.NavigationStyle)
                AssertMuliSelection(TableView)
                MultiSelectionModule.viewsListBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridControl.View, CardView)
                Assert.AreEqual(GridViewNavigationStyle.Row, CardView.NavigationStyle)
                GridControl.SelectItem(0)
                AssertMuliSelection(CardView)
            End Sub)
        End Sub

        Private Sub AssertMuliSelection(ByVal gridView As GridViewBase)
            AssertMultiSelectMode(gridView, True)
            MultiSelectionModule.selectionModeListBox.EditValue = MultiSelectMode.None
            UpdateLayoutAndDoEvents()
            AssertMultiSelectMode(gridView, False)
            Assert.IsFalse(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled)
            Assert.IsFalse(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled)
            MultiSelectionModule.selectionModeListBox.EditValue = MultiSelectMode.Row
            UpdateLayoutAndDoEvents()
            AssertMultiSelectMode(gridView, True)
            Assert.IsTrue(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled)
            Assert.IsTrue(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled)
            Assert.AreEqual(77, CType(CType(MultiSelectionModule.ProductsMultiSelectionOptionsControl.comboBoxControl, ComboBoxEdit).ItemsSource, IEnumerable(Of Product)).Count())
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count)
            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.SelectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(39, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(39, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))
            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.UnselectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(1, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))
            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.ReselectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(38, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(38, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))
            gridView.Grid.UnselectAll()
            gridView.Grid.SelectRange(0, 3)
            Assert.AreEqual("Selection Total=${0:N}", gridView.VisibleColumns(4).TotalSummaries(0).Item.DisplayFormat)
            Assert.AreEqual(Convert.ToString(607.4, Globalization.CultureInfo.CurrentCulture), gridView.VisibleColumns(4).TotalSummaries(0).Value.ToString())
            GridControl.UnselectAll()
            Dim count As Integer = TryCast(MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.ItemsSource, List(Of Range)).Count
            Assert.AreEqual(9, count)
            For i As Integer = 0 To count - 1
                MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.SelectedIndex = i
                UIAutomationActions.ClickButton(MultiSelectionModule.PriceMultiSelectionOptionsControl.SelectButton)
                UpdateLayoutAndDoEvents()
            Next

            Assert.AreEqual(GridControl.VisibleRowCount, gridView.Grid.SelectedItems.Count)
            GridControl.UnselectAll()
            GridControl.GroupBy("OrderID")
            UpdateLayoutAndDoEvents()
            GridControl.SelectItem(0)
            GridControl.SelectItem(10)
            Assert.AreEqual("Grand Total=${0:N}", CType(gridView.RootRowsContainer.Items(0), GroupRowData).GroupSummaryData(0).SummaryItem.DisplayFormat)
            Assert.AreEqual(168, Convert.ToInt32(CType(gridView.RootRowsContainer.Items(0), GroupRowData).GroupSummaryData(0).SummaryValue))
            Assert.AreEqual(336, Convert.ToInt32(CType(gridView.RootRowsContainer.Items(3), GroupRowData).GroupSummaryData(0).SummaryValue))
            Assert.AreEqual(504, Convert.ToInt32(gridView.VisibleColumns(4).TotalSummaries(0).Value))
            GridControl.ClearGrouping()
            GridControl.UnselectAll()
        End Sub

        Private Sub AssertMultiSelectMode(ByVal view As GridViewBase, ByVal isMultiSelect As Boolean)
            Assert.AreEqual(If(isMultiSelect, MultiSelectMode.Row, MultiSelectMode.None), view.DataControl.SelectionMode)
        End Sub

#End Region
        Private Sub CreateCheckFixedColumnsDemoActions()
            AddLoadModuleActions(GetType(FixedDataColumns))
        End Sub
    End Class

    Public Class BugFixesFixture
        Inherits BaseGridDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            Create_B144178_Actions()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub

#Region "B144178"
        Private Sub Create_B144178_Actions()
            AddLoadModuleActions(GetType(LargeDataSource))
            Dispatch(Sub() AssertPrioritySorting("Priority6"))
            AddLoadModuleActions(GetType(CellEditors))
            Dispatch(Sub() AssertPrioritySorting("Priority"))
        End Sub

        Private Sub AssertPrioritySorting(ByVal fieldName As String)
            GridControl.SortBy(fieldName)
            For i As Integer = 0 To 10 - 1
                Assert.AreEqual(Priority.Low, CType(GridControl.GetCellValue(i, fieldName), Priority), fieldName)
                Assert.AreEqual(Priority.High, CType(GridControl.GetCellValue(GridControl.VisibleRowCount - 1 - i, fieldName), Priority))
            Next
        End Sub
#End Region
    End Class
End Namespace
