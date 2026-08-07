Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls.Primitives
Imports System.Windows.Documents
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading
Imports DevExpress.AIIntegration.Wpf.Reporting
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Bars.Native
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Reports.UserDesigner
Imports DevExpress.Xpf.Reports.UserDesigner.Native
Imports DevExpress.Xpf.Ribbon
Imports ReportWpfDemo.AIOperations

Namespace ReportWpfDemo

    Public Partial Class AIIntegrationReportDesigner

        Public ReadOnly Property FeatureItems As List(Of ReportWpfDemo.StartScreenItem)

        Private isGuideShown As Boolean = False

        Shared Sub New()
            Call ReportWpfDemo.AIOperations.AIServicesRegistration.RegisterIfNeeded()
        End Sub

        Public Sub New()
            Me.WarningText = ReportWpfDemo.Properties.Resources.EndpointWarning
            Me.FeatureItems = Me.CreateStartScreenItems()
            Me.InitializeComponent()
        End Sub

        Private Function CreateStartScreenItem(ByVal title As String, ByVal imageName As String, ByVal description As String, ByVal link As String) As StartScreenItem
            Dim thumbImage = New System.Windows.Media.Imaging.BitmapImage(New System.Uri($"pack://application:,,,/Resources/AIReports/{imageName}Thumb.png"))
            Dim guideImage = New System.Windows.Media.Imaging.BitmapImage(New System.Uri($"pack://application:,,,/Resources/AIReports/{imageName}Guide.png"))
            thumbImage.Freeze()
            guideImage.Freeze()
            Dim item = New ReportWpfDemo.StartScreenItem() With {.Title = title, .ThumbImage = thumbImage, .GuideImage = guideImage, .Description = description, .Link = link}
            Return item
        End Function

        Private Function CreateStartScreenItems() As List(Of ReportWpfDemo.StartScreenItem)
            Dim startScreenItems = New System.Collections.Generic.List(Of ReportWpfDemo.StartScreenItem)()
            Dim localizationItem = Me.CreateStartScreenItem("Localization", "AILocalization", "Use AI-powered localization to translate report content to a different language with a single click.", "https://docs.devexpress.com/XtraReports/405605/ai-powered-functionality/desktop-reporting/ai-powered-report-localization-wpf-report-designer?v=26.1")
            Dim wizardItem = Me.CreateStartScreenItem("Report Creation", "AIReportWizard", "Run the Report Wizard to generate a new report layout based on a natural language prompt.", "https://docs.devexpress.com/XtraReports/405625/ai-powered-functionality/desktop-reporting/prompt-to-report-in-wpf-report-designer?v=26.1")
            Dim testDataSourceItem = Me.CreateStartScreenItem("Test Data Preview", "AITestData", "Generate test data with AI to preview reports without connecting to a live data source.", "https://docs.devexpress.com/XtraReports/405567/ai-powered-functionality/desktop-reporting/preview-reports-with-ai-generated-test-data-wpf-report-designer?v=26.1")
            Dim expressionItem = Me.CreateStartScreenItem("Prompt-to-Expression", "AIExpression", "Generate a data binding expression or a filter condition based on a natural language prompt.", "https://docs.devexpress.com/XtraReports/405777/ai-powered-functionality/desktop-reporting/generate-expressions-from-prompts-wpf-report-designer?v=26.1")
            startScreenItems.Add(wizardItem)
            startScreenItems.Add(localizationItem)
            startScreenItems.Add(testDataSourceItem)
            startScreenItems.Add(expressionItem)
            Return startScreenItems
        End Function

        Private Sub OnDesignerLoaded(ByVal sender As Object, ByVal args As System.Windows.RoutedEventArgs)
            Call DevExpress.Xpf.Core.SplashScreenManager.CloseAll()
            Dim designer As DevExpress.Xpf.Reports.UserDesigner.ReportDesigner = TryCast(sender, DevExpress.Xpf.Reports.UserDesigner.ReportDesigner)
            If designer Is Nothing Then Return
            Dim scroll As DevExpress.Xpf.Reports.UserDesigner.Native.XRScrollViewer = DevExpress.Xpf.Bars.Native.TreeHelper.GetChild(Of DevExpress.Xpf.Reports.UserDesigner.Native.XRScrollViewer)(designer, Function(x) Equals(x.Name, "ScrollViewer"))
            If scroll Is Nothing Then Return
            Dim topMargin = designer.ActiveDocument.Report.Bands(DevExpress.XtraReports.UI.BandKind.TopMargin)
            Dim topMarginItem = designer.ActiveDocument.ReportModel.Factory.GetModel(topMargin)
            Dim point = New System.Windows.Point(topMarginItem.RenderSize.Width / 2, topMarginItem.Position.Y - 150)
            designer.ActiveDocument.Diagram.ScrollToPoint(point, verticalAlignment:=System.Windows.VerticalAlignment.Top)
            Me.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, New System.Action(Sub() Me.ShowGuide(designer)))
        End Sub

        Private Sub ShowGuide(ByVal designer As DevExpress.Xpf.Reports.UserDesigner.ReportDesigner)
            Dim guideAdorner = New DevExpress.Xpf.DemoBase.Helpers.GuideAdorner(designer)
            If Me.isGuideShown Then Return
            If guideAdorner.IsRunning Then
                guideAdorner.UpdateGeometry()
                Return
            End If

            Dim smartTagButton As System.Windows.Controls.Primitives.ToggleButton = DevExpress.Xpf.Bars.Native.TreeHelper.GetChild(Of System.Windows.Controls.Primitives.ToggleButton)(designer, Function(x) Equals(x.Name, "SmartTagButton"))
            Dim ribbonPageHeader = DevExpress.Xpf.Bars.Native.TreeHelper.GetChild(Of DevExpress.Xpf.Ribbon.RibbonPageHeaderLinksControl)(designer)
            If ribbonPageHeader.Items.Count < 2 Then Return
            Dim testDataSourceButton As DevExpress.Xpf.Bars.BarItemLinkInfo = TryCast(ribbonPageHeader.Items(2), DevExpress.Xpf.Bars.BarItemLinkInfo)
            Dim newReportButton As DevExpress.Xpf.Bars.BarItemLinkInfo = DevExpress.Xpf.Bars.Native.TreeHelper.GetChild(Of DevExpress.Xpf.Bars.BarItemLinkInfo)(designer, Function(x) Equals(x.LinkControl?.ActualContent?.ToString(), "New Report"))
            Dim adornerLayer = System.Windows.Documents.AdornerLayer.GetAdornerLayer(designer)
            adornerLayer?.Add(guideAdorner)
            Dim wizardItem = Me.FeatureItems(0)
            guideAdorner.AddStep(New DevExpress.Xpf.DemoBase.Helpers.GuideAdorner.GuideStep With {.Target = designer, .FillTarget = True, .Image = wizardItem.GuideImage, .Title = wizardItem.Title, .Description = wizardItem.Description})
            Dim localizationItem = Me.FeatureItems(1)
            guideAdorner.AddStep(New DevExpress.Xpf.DemoBase.Helpers.GuideAdorner.GuideStep With {.Target = smartTagButton, .Image = localizationItem.GuideImage, .Title = localizationItem.Title, .Description = localizationItem.Description})
            Dim testDataSourceItem = Me.FeatureItems(2)
            guideAdorner.AddStep(New DevExpress.Xpf.DemoBase.Helpers.GuideAdorner.GuideStep With {.Target = testDataSourceButton, .Image = testDataSourceItem.GuideImage, .Title = testDataSourceItem.Title, .Description = testDataSourceItem.Description})
            Dim expressionItem = Me.FeatureItems(3)
            guideAdorner.AddStep(New DevExpress.Xpf.DemoBase.Helpers.GuideAdorner.GuideStep With {.Target = designer, .FillTarget = True, .Image = expressionItem.GuideImage, .Title = expressionItem.Title, .Description = expressionItem.Description})
            guideAdorner.Start()
            Dim handler As System.EventHandler = Nothing
            handler = Sub(s, a)
                RemoveHandler guideAdorner.Closed, handler
                adornerLayer?.Remove(guideAdorner)
                Me.isGuideShown = True
            End Sub
            AddHandler guideAdorner.Closed, handler
        End Sub

        Private Sub ButtonBase_OnClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim designerWindow As DevExpress.Xpf.Core.ThemedWindow = New DevExpress.Xpf.Core.ThemedWindow() With {.WindowState = System.Windows.WindowState.Maximized}
            Dim designer As DevExpress.Xpf.Reports.UserDesigner.ReportDesigner = New DevExpress.Xpf.Reports.UserDesigner.ReportDesigner()
            designer.DocumentSource = New XtraReportsDemos.NorthwindTraders.InvoiceReport()
            designerWindow.Content = designer
            Dim behaviors As DevExpress.Mvvm.UI.Interactivity.BehaviorCollection = DevExpress.Mvvm.UI.Interactivity.Interaction.GetBehaviors(designer)
            behaviors.Add(New DevExpress.AIIntegration.Wpf.Reporting.ReportTestDataSourceBehavior() With {.Temperature = 1})
            behaviors.Add(New DevExpress.AIIntegration.Wpf.Reporting.ReportLocalizationBehavior())
            behaviors.Add(New DevExpress.AIIntegration.Wpf.Reporting.ReportPromptToReportBehavior() With {.Temperature = 1})
            behaviors.Add(New DevExpress.AIIntegration.Wpf.Reporting.ReportPromptToExpressionBehavior() With {.Temperature = 1})
            AddHandler designer.Loaded, AddressOf Me.OnDesignerLoaded
            Dim waitIndicator = DevExpress.Xpf.Core.SplashScreenManager.CreateWaitIndicator()
            waitIndicator.Show(owner:=Me, startupLocation:=System.Windows.WindowStartupLocation.CenterOwner)
            designerWindow.ShowDialog()
            RemoveHandler designer.Loaded, AddressOf Me.OnDesignerLoaded
            Me.isGuideShown = False
        End Sub
    End Class

    Public Class StartScreenItem

        Public Property Title As String

        Public Property ThumbImage As ImageSource

        Public Property GuideImage As ImageSource

        Public Property Description As String

        Public Property Link As String
    End Class
End Namespace
