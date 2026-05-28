using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using DevExpress.DemoData.Helpers;
using DevExpress.DemoReports;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Utils.About;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DocumentViewer;

namespace ReportWpfDemo {
    public class ReportModuleBase : DemoModule {
        static ReportModuleBase() {
            PatchDemoReportsConnectionStrings();
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.ConvertBindingsToExpressions = DevExpress.XtraReports.UI.PromptBoolean.False;
            DevExpress.Xpf.Reports.UserDesigner.Extensions.ReportDesignerRibbonDemoButtonHelper.Load();
            var dispatcher = Dispatcher.CurrentDispatcher;
            if(dispatcher != null) {
                dispatcher.UnhandledException += Dispatcher_UnhandledException;
            }
        }
        static void PatchDemoReportsConnectionStrings() {
            string path = DataFilesHelper.FindFile("nwind.db", DataFilesHelper.DataPath);
            string directory = System.IO.Path.GetDirectoryName(path);
            XtraReportsDemos.ConnectionHelper.SetDataDirectory(directory);
        }
        static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            e.Handled = true;

            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject())) {
                var exception = e.Exception.GetBaseException();
                MessageBox.Show(exception.ToString(), "Demo Application Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                UAlgo.Default.DoEventException(exception);
            }
        }

        protected ReportModuleViewModelBase ReportModuleViewModel {
            get { return DataContext as ReportModuleViewModelBase; }
        }

        public ReportModuleBase() {
            Interaction.GetBehaviors(this).Add(CreateWindowService());
            Interaction.GetBehaviors(this).Add(CreateDispatchFocusBehavior());
            Loaded += OnLoaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            Loaded -= OnLoaded;

            if(System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                return;


            //ReportModuleViewModel.InitializeReport();
        }

        protected override void Hide() {
            base.Hide();
            var viewModel = ReportModuleViewModel;
            if(viewModel != null && viewModel.Report != null) {
                viewModel.Report.StopPageBuilding();
            }
        }

        static WindowService CreateWindowService() {
            var style = new Style(typeof(ThemedWindow));
            style.Setters.Add(new Setter(ThemedWindow.WindowStateProperty, WindowState.Maximized));
            style.Setters.Add(new Setter(ThemedWindow.ShowIconProperty, false));
            style.Setters.Add(new Setter(ThemedWindow.TitleProperty, "Report Designer"));
            style.Setters.Add(new Setter(ThemedWindow.UseLayoutRoundingProperty, true));
            style.Setters.Add(new Setter(ThemedWindow.ShowInTaskbarProperty, false));
            return new WindowService {
                WindowShowMode = WindowShowMode.Dialog,
                WindowType = typeof(ThemedWindow),
                WindowStyle = style
            };
        }

        static DispatchFocusBehavior CreateDispatchFocusBehavior() {
            var behavior = new DispatchFocusBehavior();
            BindingOperations.SetBinding(behavior, DispatchFocusBehavior.ElementProperty, new Binding() { ElementName = "viewer" });
            return behavior;
        }
    }
}
