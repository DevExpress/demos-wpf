using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.DemoData.Models;
using DevExpress.Office.Services;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Layout;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using RichEditDemo.Modules.MailMerge;

namespace RichEditDemo {
    public partial class MailMergeMasterDetail : RichEditDemoModule {
        bool HighlightingEnabled {
            get {
                return highlightingEnabled;
            }
            set {
                if(highlightingEnabled != value) {
                    highlightingEnabled = value;
                    Dispatcher.Invoke(templateRichEdit.Refresh);
                }
            }
        }
        IList<Customer> Customers { get { return customers.Value; } }

        static readonly CustomDrawHighlightType regionHighlightType = CustomDrawHighlightType.Fill;

        MailMergeRegionInfo rootRegionInfo;
        MailMergeOptions mailMergeOptions;
        List<CustomDrawHighlightInfo> regionHighlights = new List<CustomDrawHighlightInfo>();
        bool highlightingEnabled;
        bool hasValidResult;
        RichEditDemoExceptionsHandler resultRichEditExceptionsHandler;
        Lazy<IList<Customer>> customers = new Lazy<IList<Customer>>(() => DevExpress.DemoData.NWindDataProvider.Customers);

        public MailMergeMasterDetail() {
            InitializeComponent();

            SetActiveRichEdit(templateRichEdit);

            mailMergeOptions = templateRichEdit.CreateMailMergeOptions();
            var dataSource = CreateDataSource();
            recordSelectorGridControl.ItemsSource = dataSource;
            templateRichEdit.MailMergeOptions.DataSource = dataSource;

            resultRichEditExceptionsHandler = new RichEditDemoExceptionsHandler(resultRichEdit);
            resultRichEditExceptionsHandler.Install();
            templateRichEdit.Document.ContentChanged += Document_ContentChanged;

            UpdateRegionHierarchy();
        }
        void SetActiveRichEdit(RichEditControl richEditControl) {
            MainWindow.CommonRibbon.Init(null);
            MainWindow.CommonRibbon.Init(richEditControl);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_Review = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_FirstRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_LastRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_NextRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_PreviousRecord = false;
            MainWindow.CommonRibbon.RibbonItem_MailMerge_PreviewResults_ViewMergedData = false;
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_MailMerge;
        }
        bool RunMailMerge() {
            if(hasValidResult)
                return true;

            string errorMsg = null;
            bool success = false;

            try {
                templateRichEdit.Document.MailMerge(mailMergeOptions, resultRichEdit.Document);
                success = true;
            }
            catch(Exception ex) {
                errorMsg = ex.Message;
                success = false;
            }

            hasValidResult = success;
            if(success)
                return true;
            tabControl.SelectedItem = templateTabPage;
            DemoUtils.ShowErrorMessage($"Mail Merge failed:{Environment.NewLine}{errorMsg}");
            return false;
        }
        void RichEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            var sourceRichEdit = (RichEditControl)sender;
            var commandFactory = sourceRichEdit.GetService<IRichEditCommandFactoryService>();
            if(commandFactory != null && !(commandFactory is MailMergeCommandFactoryService))
                sourceRichEdit.ReplaceService<IRichEditCommandFactoryService>(new MailMergeCommandFactoryService(commandFactory, templateRichEdit, mailMergeOptions));
        }
        IEnumerable<object> CreateDataSource() {
            return Customers.Select(c => {
                c.Orders = c.Orders.Select(o => {
                    o.OrderDetails = o.OrderDetails.ToList();
                    return o;
                }).ToList();
                return c;
            }).ToList();
        }
        void UpdateRegionHierarchy() {
            try {
                rootRegionInfo = templateRichEdit.Document.GetRegionHierarchy(mailMergeOptions.RegionStartTag, mailMergeOptions.RegionEndTag);
            }
            catch {
                rootRegionInfo = null;
            }
            regionHighlights.Clear();
            AddRegionRanges(rootRegionInfo);
        }
        void AddRegionRanges(MailMergeRegionInfo mailMergeRegionInfo) {
            if(mailMergeRegionInfo is null)
                return;

            if(mailMergeRegionInfo.ParentRegion != null) {
                FixedRange regionRange = GetRegionRange(mailMergeRegionInfo);
                Color highlightColor = GetHighlightColor(mailMergeRegionInfo);
                regionHighlights.Add(new CustomDrawHighlightInfo(regionRange, regionHighlightType, highlightColor));
            }

            foreach(var childRegion in mailMergeRegionInfo.Regions) {
                AddRegionRanges(childRegion);
            }
        }
        FixedRange GetRegionRange(MailMergeRegionInfo mailMergeRegionInfo) {
            DocumentRange regionStartFieldRange = mailMergeRegionInfo.StartField.Range.GetAsFrozen();
            DocumentRange regionEndFieldRange = mailMergeRegionInfo.EndField.Range.GetAsFrozen();
            int regionLength = regionEndFieldRange.End.ToInt() - regionStartFieldRange.Start.ToInt();

            return new FixedRange(regionStartFieldRange.Start.ToInt(), regionLength);
        }
        static Color GetHighlightColor(MailMergeRegionInfo region) {
            if(region.ParentRegion is null)
                return Colors.Transparent;
            switch(region.Name) {
                case "Orders": return Color.FromArgb((byte)(0.4 * 255), 129, 212, 250);
                case "OrderDetails": return Color.FromArgb((byte)(1 * 255), 255, 106, 0);
                default: return Colors.Brown;
            }
        }
        void recordSelectorGridControl_SelectionChanged(object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e) {
            mailMergeOptions.DataSource = recordSelectorGridControl.SelectedItems;
            hasValidResult = false;
            if(resultRichEdit.IsVisible && !RunMailMerge())
                tabControl.SelectedItem = templateTabPage;
        }
        void Document_ContentChanged(object sender, System.EventArgs e) {
            UpdateRegionHierarchy();
            hasValidResult = false;
        }
        void templateRichEdit_BeforePagePaint(object sender, BeforePagePaintEventArgs e) {
            if(!HighlightingEnabled || e.CanvasOwnerType == CanvasOwnerType.Printer || regionHighlights.Count == 0)
                return;

            var visibleRegionHighlights = regionHighlights.Where(rh => e.Page.IsRangeVisible(rh.HighlightRange)).ToList();
            var highlightPainter = new CustomDrawPagePainter(templateRichEdit) {
                LayoutRowHighlights = visibleRegionHighlights
            };
            e.Painter = highlightPainter;
        }
        void HighlightRegionsBarItem_CheckedChanged(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            HighlightingEnabled = ((BarCheckItem)sender).IsChecked ?? false;
        }

        void tabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if(tabControl.SelectedItem == resultTabPage && RunMailMerge()) {
                SetActiveRichEdit(resultRichEdit);
            }
            else if(tabControl.SelectedItem == templateTabPage) {
                SetActiveRichEdit(templateRichEdit);
            }
        }
    }
}
