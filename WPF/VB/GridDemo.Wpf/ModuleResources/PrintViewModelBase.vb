Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing

Namespace GridDemo

    Public MustInherit Class PrintViewModelBase
        Inherits ViewModelBase

        Public Property PrintableControl As IPrintableControl

        <Command>
        Public Sub OnModuleLoaded()
            If isModuleLoaded Then Return
            isModuleLoaded = True
            Dim service = GetService(Of IDocumentManagerService)()
            service.Documents.[Select](Function(x) TryCast(x.Content, PrintDocumentViewModel)).Where(Function(x) x IsNot Nothing).ForEach(Sub(x) x.CreatePrintableControlLink())
        End Sub

        <Command>
        Public Sub OnModuleUnloaded()
            If Not isModuleLoaded Then Return
            isModuleLoaded = False
            Dim service = GetService(Of IDocumentManagerService)()
            If service Is Nothing Then Return
            service.Documents.[Select](Function(x) TryCast(x.Content, PrintDocumentViewModel)).Where(Function(x) x IsNot Nothing).ForEach(Sub(x) x.StopPageBuilding())
        End Sub

        <Command>
        Public Overridable Sub ReOpenPreviewTabs()
            Dim service = GetService(Of IDocumentManagerService)()
            For Each document In service.Documents.ToArray()
                OnTabClosed(document.Content)
                document.Close()
            Next

            ShowPreviewInNewTab()
        End Sub

        <Command>
        Public Overridable Sub ShowPreviewInNewTab()
            Dim service = GetService(Of IDocumentManagerService)()
            Dim docVM = New PrintDocumentViewModel(PrintableControl)
            If isModuleLoaded Then docVM.CreatePrintableControlLink()
            Dim doc = service.CreateDocument(docVM)
            doc.Title = GetTitle()
            doc.DestroyOnClose = True
            doc.Show()
        End Sub

        Public Sub OnTabClosed(ByVal tabContent As Object)
            CType(tabContent, PrintableControlLink).Dispose()
        End Sub

        Protected MustOverride Function GetTitle() As String

        Private isModuleLoaded As Boolean
    End Class

    Public Class PrintDocumentViewModel
        Inherits ViewModelBase

        Public Property PrintableControl As IPrintableControl

        Public Property PrintableControlLink As PrintableControlLink
            Get
                Return printableControlLinkField
            End Get

            Private Set(ByVal value As PrintableControlLink)
                SetProperty(printableControlLinkField, value, NameOf(PrintDocumentViewModel.PrintableControlLink))
            End Set
        End Property

        Public Sub New(ByVal printableControl As IPrintableControl)
            Me.PrintableControl = printableControl
        End Sub

        Public Sub CreatePrintableControlLink()
            If PrintableControlLink IsNot Nothing Then Return
            PrintableControlLink = New PrintableControlLink(PrintableControl)
            PrintableControlLink.CreateDocument(True)
        End Sub

        Public Sub StopPageBuilding()
            PrintableControlLink?.StopPageBuilding()
        End Sub

        Private printableControlLinkField As PrintableControlLink
    End Class

    Public Class PrintableTabBehavior
        Inherits Behavior(Of DataViewBase)

        Public Property PrintableControl As IPrintableControl
            Get
                Return CType(GetValue(PrintableControlProperty), IPrintableControl)
            End Get

            Set(ByVal value As IPrintableControl)
                SetValue(PrintableControlProperty, value)
            End Set
        End Property

        Public Shared ReadOnly PrintableControlProperty As DependencyProperty = DependencyProperty.Register("PrintableControl", GetType(IPrintableControl), GetType(PrintableTabBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ShowPreviewTab As ICommand
            Get
                Return CType(GetValue(ShowPreviewTabProperty), ICommand)
            End Get

            Set(ByVal value As ICommand)
                SetValue(ShowPreviewTabProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ShowPreviewTabProperty As DependencyProperty = DependencyProperty.Register("ShowPreviewTab", GetType(ICommand), GetType(PrintableTabBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            PrintableControl = AssociatedObject
            AddHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            PrintableControl = Nothing
        End Sub

        Private Sub AssociatedObject_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            RemoveHandler AssociatedObject.Loaded, AddressOf AssociatedObject_Loaded
            ShowPreviewTab?.Execute(Nothing)
        End Sub
    End Class
End Namespace
