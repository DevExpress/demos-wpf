using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.RichEdit;

namespace RichEditDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadData(true, false);
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.RichEdit,
                PreloadCategories.Grid,
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
        public bool? RibbonCategory_HeaderFooterTools { get; set; }
        public bool? RibbonCategory_TableTools { get; set; }
        public bool? RibbonCategory_PictureTools { get; set; }

        public bool? RibbonPage_MailMerge { get; set; }
        public bool? RibbonPage_File { get; set; }
        public bool? RibbonPage_Home { get; set; }
        public bool? RibbonPage_Insert { get; set; }
        public bool? RibbonPage_PageLayout { get; set; }
        public bool? RibbonPage_References { get; set; }
        public bool? RibbonPage_Review { get; set; }
        public bool? RibbonPage_View { get; set; }

        public bool? RibbonGroup_View_DocumentViews { get; set; }
        public bool? RibbonGroup_View_Show { get; set; }
        public bool? RibbonGroup_Review_Proofing { get; set; }
        public bool? RibbonGroup_Review_Protect { get; set; }
        public bool? RibbonGroup_Review_Comment { get; set; }
        public bool? RibbonGroup_Review_Tracking { get; set; }
        public bool? RibbonGroup_Review_Changes { get; set; }
        public bool? RibbonGroup_MailMerge_WriteInsertFields { get; set; }

        public bool? RibbonItem_MailMerge_PreviewResults_ViewMergedData { get; set; }
        public bool? RibbonItem_MailMerge_PreviewResults_FirstRecord { get; set; }
        public bool? RibbonItem_MailMerge_PreviewResults_PreviousRecord { get; set; }
        public bool? RibbonItem_MailMerge_PreviewResults_NextRecord { get; set; }
        public bool? RibbonItem_MailMerge_PreviewResults_LastRecord { get; set; }
        public bool? RibbonItem_MailMerge_PreviewResults_MailMerge { get; set; }

        public string SelectedPage { get; set; }
        bool SetSelectedPage { get; set; }

        RibbonControl Ribbon { get; set; }

        public void Init(RichEditControl richEdit) {
            ClearState();

            if(richEdit == null) {
                MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.None);
                Visibility = System.Windows.Visibility.Collapsed;
            }
            else {
                MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.InternalWithExternal);
                Visibility = System.Windows.Visibility.Visible;
            }
            var oldRichEdit = RichEditControl.GetRichEdit(this);
            if(oldRichEdit == null) {
                RichEditControl.SetRichEdit(this, richEdit);
            }
            Dispatcher.InvokeAsync(() => {
                RichEditControl.SetRichEdit(this, richEdit);
                UpdateState();
            });
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
            SetPageCategoryVisibility(DefaultBarItemNames.RibbonCategory_HeaderFooterTools, RibbonCategory_HeaderFooterTools);
            SetPageCategoryVisibility(DefaultBarItemNames.RibbonCategory_TableTools, RibbonCategory_TableTools);
            SetPageCategoryVisibility(DefaultBarItemNames.RibbonCategory_PictureTools, RibbonCategory_PictureTools);

            SetPageVisibility(DefaultBarItemNames.RibbonPage_MailMerge, RibbonPage_MailMerge);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_File, RibbonPage_File);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Home, RibbonPage_Home);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Insert, RibbonPage_Insert);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_PageLayout, RibbonPage_PageLayout);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_References, RibbonPage_References);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_Review, RibbonPage_Review);
            SetPageVisibility(DefaultBarItemNames.RibbonPage_View, RibbonPage_View);

            UpdatePageState();
            UpdateSelectionState();
        }
        void UpdatePageState() {
            Dispatcher.InvokeAsync(UpdatePageStateCore);
        }
        void UpdatePageStateCore() {
            if(Ribbon == null || Ribbon.SelectedPage == null) return;
            var page = Ribbon.SelectedPage;
            if(page.Name == DefaultBarItemNames.RibbonPage_View) {
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_DocumentViews, RibbonGroup_View_DocumentViews);
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_View_Show, RibbonGroup_View_Show);
            }
            else if(page.Name == DefaultBarItemNames.RibbonPage_Review) {
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Proofing, RibbonGroup_Review_Proofing);
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Protect, RibbonGroup_Review_Protect);
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Comment, RibbonGroup_Review_Comment);
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Tracking, RibbonGroup_Review_Tracking);
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_Review_Changes, RibbonGroup_Review_Changes);
            }
            else if(page.Name == DefaultBarItemNames.RibbonPage_MailMerge) {
                SetPageGroupVisibility(page, DefaultBarItemNames.RibbonGroup_MailMerge_WriteInsertFields, RibbonGroup_MailMerge_WriteInsertFields);

                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_ViewMergedData, RibbonItem_MailMerge_PreviewResults_ViewMergedData);
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_FirstRecord, RibbonItem_MailMerge_PreviewResults_FirstRecord);
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_PreviousRecord, RibbonItem_MailMerge_PreviewResults_PreviousRecord);
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_NextRecord, RibbonItem_MailMerge_PreviewResults_NextRecord);
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_LastRecord, RibbonItem_MailMerge_PreviewResults_LastRecord);
                SetPageItemVisibility(page, DefaultBarItemNames.RibbonItem_MailMerge_PreviewResults_MailMerge, RibbonItem_MailMerge_PreviewResults_MailMerge);
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
            RibbonCategory_HeaderFooterTools = null;
            RibbonCategory_TableTools = null;
            RibbonCategory_PictureTools = null;

            RibbonPage_MailMerge = null;
            RibbonPage_File = null;
            RibbonPage_Home = null;
            RibbonPage_Insert = null;
            RibbonPage_PageLayout = null;
            RibbonPage_References = null;
            RibbonPage_Review = null;
            RibbonPage_View = null;

            RibbonGroup_View_DocumentViews = null;
            RibbonGroup_View_Show = null;
            RibbonGroup_Review_Proofing = null;
            RibbonGroup_Review_Protect = null;
            RibbonGroup_Review_Comment = null;
            RibbonGroup_Review_Tracking = null;
            RibbonGroup_Review_Changes = null;
            RibbonGroup_MailMerge_WriteInsertFields = null;

            RibbonItem_MailMerge_PreviewResults_ViewMergedData = null;
            RibbonItem_MailMerge_PreviewResults_FirstRecord = null;
            RibbonItem_MailMerge_PreviewResults_PreviousRecord = null;
            RibbonItem_MailMerge_PreviewResults_NextRecord = null;
            RibbonItem_MailMerge_PreviewResults_LastRecord = null;
            RibbonItem_MailMerge_PreviewResults_MailMerge = null;

            SelectedPage = null;
            SetSelectedPage = Ribbon != null ? Ribbon.SelectedPage != null : false;
        }

        void SetPageCategoryVisibility(string name, bool? isVisible) {
            var category = GetCategory(name);
            if(category == null)
                throw new InvalidOperationException();
            if(isVisible.HasValue) {
                category.SetCurrentValue(RibbonPageCategoryBase.IsVisibleProperty, isVisible.Value);
            }
            else {
                category.ClearValue(RibbonPageCategoryBase.IsVisibleProperty);
            }
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
        void SetPageItemVisibility(RibbonPage page, string name, bool? isVisible) {
            var item = GetItem(page, name);
            if(item == null)
                throw new InvalidOperationException();
            if(isVisible.HasValue) {
                item.SetCurrentValue(BarItem.IsVisibleProperty, isVisible.Value);
            }
            else {
                item.ClearValue(BarItem.IsVisibleProperty);
            }
        }
        void SetPageIsSelected(RibbonPage page, bool isSelected) {
            page.SetCurrentValue(RibbonPage.IsSelectedProperty, isSelected);
        }

        RibbonPageCategoryBase GetCategory(string name) {
            return Ribbon.ActualCategories.FirstOrDefault(x => x.Name == name);
        }
        RibbonPage GetPage(string name) {
            return GetPages().FirstOrDefault(x => x.Name == name);
        }
        RibbonPageGroup GetPageGroup(RibbonPage page, string name) {
            return page.ActualGroups.FirstOrDefault(x => x.Name == name);
        }
        BarItem GetItem(RibbonPage page, string name) {
            return GetPageItems(page).FirstOrDefault(x => x.Name == name);
        }

        IEnumerable<RibbonPage> GetPages() {
            return Ribbon.ActualCategories.SelectMany(x => x.ActualPages);
        }
        IEnumerable<BarItem> GetPageItems(RibbonPage page) {
            return page.ActualGroups.SelectMany(x => x.Items).OfType<BarItem>();
        }
    }
}
