using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.Data.Internal;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace ReportWpfDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes();
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.PropertyGrid,
                PreloadCategories.Grid
            );
        }
        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs e) {
            var domain = sender as AppDomain;
            var assembly = typeof(MainWindow).Assembly;
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(e.Name);
            var loadedAssembly = domain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == partialName);
            if(loadedAssembly != null)
                return loadedAssembly;
            string assemblyName = partialName + ".dll";
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), assemblyName);
            if(File.Exists(filePath)) {
                return SafeTypeResolver.GetOrLoadAssemblyFile(filePath, false);
            }
            string manifestResourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(x => x.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase));
            if(string.IsNullOrEmpty(manifestResourceName))
                return null;
            using(Stream manifestResourceStream = assembly.GetManifestResourceStream(manifestResourceName)) {
                if(manifestResourceStream == null)
                    return null;
                byte[] data = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(data, 0, data.Length);
                return DevExpress.Data.Internal.SafeTypeResolver.GetOrLoadAssemblyBytes(data);
            }
        }

        readonly DocumentPreview cachedModule;
        readonly Dictionary<Type, ReportModuleViewModelBase> viewModels;
        public MainWindow() {
            cachedModule = new DocumentPreview();
            viewModels = new Dictionary<Type, ReportModuleViewModelBase>();
            AllowAnimation = false;
            InitializeComponent();
        }

        protected override void OnDemoModuleCompletelyLoaded(DemoModule demo) {
            base.OnDemoModuleCompletelyLoaded(demo);
            if(CurrentDemoModule != cachedModule)
                cachedModule.DataContext = null;
        }
        protected override DemoModule LoadDemoModule(Type demoModuleType, bool disableRtl, Func<DemoModule, DemoModuleOwner> owner, out Exception exception) {
            if(demoModuleType == null) {
                exception = null;
                return null;
            }
            if(!demoModuleType.IsSubclassOf(typeof(ReportModuleViewModelBase)))
                return base.LoadDemoModule(demoModuleType, disableRtl, owner, out exception);

            try {
                var demoViewModel = GetViewModel(demoModuleType);
                if(disableRtl)
                    cachedModule.FlowDirection = System.Windows.FlowDirection.LeftToRight;
                cachedModule.UpdateOwner(owner(cachedModule));
                cachedModule.DataContext = demoViewModel;
                exception = null;
                return cachedModule;
            }
            catch(Exception e) {
                exception = e;
                return null;
            }
        }
        protected override void ClearDemoModule(DemoModule module, bool hide) { }
        object GetViewModel(Type demoModuleType) {
            ReportModuleViewModelBase viewModel;
            if(!viewModels.TryGetValue(demoModuleType, out viewModel)) {
                viewModel = (ReportModuleViewModelBase)ViewModelSourceHelper.Create(demoModuleType);
                viewModels.Add(demoModuleType, viewModel);
            }
            return viewModel;
        }
    }
}
