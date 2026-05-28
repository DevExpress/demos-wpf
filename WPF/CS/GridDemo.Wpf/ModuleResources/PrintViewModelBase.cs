using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;

namespace GridDemo {
    public abstract class PrintViewModelBase : ViewModelBase {
        public IPrintableControl PrintableControl { get; set; }

        [Command]
        public void OnModuleLoaded() {
            if(isModuleLoaded)
                return;
            isModuleLoaded = true;
            var service = this.GetService<IDocumentManagerService>();
            service.Documents
                .Select(x => x.Content as PrintDocumentViewModel)
                .Where(x => x != null)
                .ForEach(x => x.CreatePrintableControlLink());
        }
        [Command]
        public void OnModuleUnloaded() {
            if(!isModuleLoaded)
                return;
            isModuleLoaded = false;
            var service = this.GetService<IDocumentManagerService>();
            if(service == null)
                return;
            service.Documents
                .Select(x => x.Content as PrintDocumentViewModel)
                .Where(x => x != null)
                .ForEach(x => x.StopPageBuilding());
        }
        [Command]
        public virtual void ReOpenPreviewTabs() {
            var service = this.GetService<IDocumentManagerService>();
            foreach(var document in service.Documents.ToArray()) {
                OnTabClosed(document.Content);
                document.Close();
            }

            ShowPreviewInNewTab();
        }
        [Command]
        public virtual void ShowPreviewInNewTab() {
            var service = this.GetService<IDocumentManagerService>();
            var docVM = new PrintDocumentViewModel(PrintableControl);
            if(isModuleLoaded)
                docVM.CreatePrintableControlLink();

            var doc = service.CreateDocument(docVM);
            doc.Title = GetTitle();
            doc.DestroyOnClose = true;
            doc.Show();
        }
        public void OnTabClosed(object tabContent) {
            ((PrintableControlLink)tabContent).Dispose();
        }
        
        protected abstract string GetTitle();

        bool isModuleLoaded;
    }
    public class PrintDocumentViewModel : ViewModelBase {
        public IPrintableControl PrintableControl { get; set; }
        public PrintableControlLink PrintableControlLink { get { return printableControlLink; } private set { SetProperty(ref printableControlLink, value, nameof(PrintableControlLink)); } }

        public PrintDocumentViewModel(IPrintableControl printableControl) {
            PrintableControl = printableControl;
        }
        public void CreatePrintableControlLink() {
            if(PrintableControlLink != null)
                return;
            PrintableControlLink = new PrintableControlLink(PrintableControl);
            PrintableControlLink.CreateDocument(true);
        }
        public void StopPageBuilding() {
            PrintableControlLink?.StopPageBuilding();
        }

        PrintableControlLink printableControlLink;
    }

    public class PrintableTabBehavior : Behavior<DataViewBase> {

        public IPrintableControl PrintableControl {
            get { return (IPrintableControl)GetValue(PrintableControlProperty); }
            set { SetValue(PrintableControlProperty, value); }
        }

        public static readonly DependencyProperty PrintableControlProperty =
            DependencyProperty.Register("PrintableControl", typeof(IPrintableControl), typeof(PrintableTabBehavior), new PropertyMetadata(null));

        public ICommand ShowPreviewTab {
            get { return (ICommand)GetValue(ShowPreviewTabProperty); }
            set { SetValue(ShowPreviewTabProperty, value); }
        }

        public static readonly DependencyProperty ShowPreviewTabProperty =
            DependencyProperty.Register("ShowPreviewTab", typeof(ICommand), typeof(PrintableTabBehavior), new PropertyMetadata(null));

        protected override void OnAttached() {
            base.OnAttached();
            PrintableControl = AssociatedObject;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            PrintableControl = null;
        }
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            ShowPreviewTab?.Execute(null);
        }
    }
}
