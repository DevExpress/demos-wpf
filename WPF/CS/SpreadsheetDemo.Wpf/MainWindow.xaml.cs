using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid,
                PreloadCategories.Spreadsheet,
                PreloadCategories.Charts,
                PreloadCategories.PropertyGrid,
                PreloadCategories.Printing
            );
        }

        public static MainWindow Instance { get; private set; }
        public static CommonRibbon CommonRibbon { get { return (CommonRibbon)Instance.Ribbon; } }

        public MainWindow() {
            Instance = this;
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }
    }
    public class CommonRibbon : Control {
        public bool? RibbonPage_Home { get; set; }
        public bool? RibbonPage_Insert { get; set; }
        public bool? RibbonPage_Formulas { get; set; }
        public bool? RibbonPage_Data { get; set; }
        public bool? RibbonPage_Review { get; set; }

        public bool? RibbonGroup_File_Info { get; set; }
        public bool? RibbonGroup_PageLayout_Show { get; set; }
        public bool? RibbonGroup_PageLayout_Print { get; set; }
        public bool? RibbonGroup_PageLayout_Arrange { get; set; }
        public bool? RibbonGroup_View_Show { get; set; }
        public bool? RibbonGroup_View_Window { get; set; }

        public string RibbonPage_File_Caption { get; set; }
        public string SelectedPage { get; set; }
        bool SetSelectedPage { get; set; }

        RibbonControl Ribbon { get; set; }

        public void Init(SpreadsheetControl spreadsheet) {
            ClearState();

            if(spreadsheet == null) {
                MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.None);
                Visibility = System.Windows.Visibility.Collapsed;
            }
            else {
                MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.InternalWithExternal);
                Visibility = System.Windows.Visibility.Visible;
            }
            var oldSpreadsheet = SpreadsheetControl.GetSpreadsheet(this);
            if(oldSpreadsheet == null) {
                SetSpreadsheet(spreadsheet);
            }
            else {
                oldSpreadsheet.Ribbon = null;
            }
            Dispatcher.InvokeAsync(() => {
                SetSpreadsheet(spreadsheet);
                UpdateState();
            }, DispatcherPriority.Loaded);
        }
        void SetSpreadsheet(SpreadsheetControl spreadsheet) {
            SpreadsheetControl.SetSpreadsheet(this, spreadsheet);
            if(spreadsheet != null)
                spreadsheet.Ribbon = Ribbon;
        }

        public override void OnApplyTemplate() {
            if(Ribbon != null) {
                Ribbon.SelectedPageChanged -= OnSelectedPageChanged;
            }
            base.OnApplyTemplate();
            Ribbon = LayoutTreeHelper.GetVisualChildren(this)
                .OfType<RibbonControl>()
                .FirstOrDefault();
            if(Ribbon == null)
                return;
            Ribbon.SelectedPageChanged += OnSelectedPageChanged;
            UpdateState();
        }
        void OnSelectedPageChanged(object sender, RibbonPropertyChangedEventArgs e) {
            UpdatePageState();
        }

        void UpdateState() {
            if(Ribbon == null)
                return;
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Home, RibbonPage_Home);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Insert, RibbonPage_Insert);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Formulas, RibbonPage_Formulas);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Data, RibbonPage_Data);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Review, RibbonPage_Review);
            SetPageCaption(DefaultBarItemNames.RibbonPage_File, RibbonPage_File_Caption);

            UpdatePageState();
            UpdateSelectionState();
        }
        void UpdatePageState() {
            if(Ribbon == null || Ribbon.SelectedPage == null) return;
            var page = Ribbon.SelectedPage;
            if(page.Name == DefaultBarItemNames.RibbonPage_File) {
                Dispatcher.InvokeAsync(() => {
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_File_Info, RibbonGroup_File_Info);
                });
            }
            else if(page.Name == DefaultBarItemNames.RibbonPage_PageLayout) {
                Dispatcher.InvokeAsync(() => {
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_PageLayout_Show, RibbonGroup_PageLayout_Show);
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_PageLayout_Print, RibbonGroup_PageLayout_Print);
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_PageLayout_Arrange, RibbonGroup_PageLayout_Arrange);
                });
            }
            else if(page.Name == DefaultBarItemNames.RibbonPage_View) {
                Dispatcher.InvokeAsync(() => {
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_Show, RibbonGroup_View_Show);
                    SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_Window, RibbonGroup_View_Window);
                });
            }
        }
        void UpdateSelectionState() {
            if(SelectedPage != null) {
                var page = GetPage(SelectedPage);
                SetPageIsSelected(page, true);
                return;
            }
            if(SetSelectedPage) {
                var page = GetPages().First(x => x.IsVisible);
                SetPageIsSelected(page, true);
            }
        }
        void ClearState() {
            RibbonPage_Home = null;
            RibbonPage_Insert = null;
            RibbonPage_Formulas = null;
            RibbonPage_Data = null;
            RibbonPage_Review = null;

            RibbonGroup_File_Info = null;
            RibbonGroup_PageLayout_Show = null;
            RibbonGroup_PageLayout_Print = null;
            RibbonGroup_PageLayout_Arrange = null;
            RibbonGroup_View_Show = null;
            RibbonGroup_View_Window = null;

            RibbonPage_File_Caption = null;
            SelectedPage = null;
            SetSelectedPage = Ribbon?.SelectedPage != null;
        }

        void SetPageVisibility(string name, bool? isVisible) {
            var page = GetPage(name);
            if(page == null)
                throw new InvalidOperationException();
            if(isVisible.HasValue) {
                page.SetCurrentValue(RibbonPage.IsVisibleProperty, isVisible.Value);
            }
            else {
                page.ClearValue(RibbonPage.IsVisibleProperty);
            }
        }
        void SetPageCaption(string name, string caption) {
            var page = GetPage(name);
            if(page == null)
                throw new InvalidOperationException();
            if(!string.IsNullOrEmpty(caption)) {
                page.SetCurrentValue(RibbonPage.CaptionProperty, caption);
            }
            else {
                page.ClearValue(RibbonPage.CaptionProperty);
            }
        }
        void SetPageGroupVisibility(RibbonPage page, string name, bool? isVisible) {
            var group = GetPageGroup(page, name);
            if(group == null)
                throw new InvalidOperationException();
            if(isVisible.HasValue) {
                group.SetCurrentValue(RibbonPageGroup.IsVisibleProperty, isVisible.Value);
            }
            else {
                group.ClearValue(RibbonPageGroup.IsVisibleProperty);
            }
        }
        void SetPageIsSelected(RibbonPage page, bool isSelected) {
            page.SetCurrentValue(RibbonPage.IsSelectedProperty, isSelected);
        }

        RibbonPage GetPage(string name) {
            return GetPages().FirstOrDefault(x => x.Name == name);
        }
        RibbonPageGroup GetPageGroup(RibbonPage page, string name) {
            return page.ActualGroups.FirstOrDefault(x => x.Name == name);
        }
        IEnumerable<RibbonPage> GetPages() {
            return Ribbon.ActualCategories.SelectMany(x => x.ActualPages);
        }
    }
}
