using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using DevExpress.Data.Utils;
using DevExpress.DemoData.Models;
using DevExpress.Xpf;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Docking;

namespace DockingDemo {
    public class DockingDemoModule : DemoModule {
        public static readonly DependencyProperty ShouldRestoreOnActivateProperty = DependencyProperty.RegisterAttached("ShouldRestoreOnActivate", typeof(bool), typeof(DockingDemoModule));

        public DockingDemoModule() {
            Loaded += OnLoaded;
            ModuleLoaded += OnModuleLoaded;
        }

        protected bool IsModuleLoaded { get; private set; }

        public static bool GetShouldRestoreOnActivate(DependencyObject target) {
            return (bool)target.GetValue(ShouldRestoreOnActivateProperty);
        }
        public static void SetShouldRestoreOnActivate(DependencyObject target, bool value) {
            target.SetValue(ShouldRestoreOnActivateProperty, value);
        }
        protected override void HidePopupContent() {
            var containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(this).OfType<DockLayoutManager>();
            foreach(var container in containerList)
                HideFloatGroups(container);
            base.HidePopupContent();
        }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) {
            if(IsModuleLoaded) return;
            var containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(this).OfType<DockLayoutManager>();
            foreach(var container in containerList) {
                HideFloatGroups(container);
            }
        }
        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            var containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(this).OfType<DockLayoutManager>();
            foreach(var container in containerList)
                ShowFloatGroups(container);
        }
        void HideFloatGroups(DockLayoutManager dockLayoutManager) {
            if(dockLayoutManager.IsDisposing) return;
            foreach(FloatGroup floatGroup in dockLayoutManager.FloatGroups) {
                if(floatGroup.IsOpen) {
                    SetShouldRestoreOnActivate(floatGroup, true);
                    floatGroup.IsOpen = false;
                }
            }
        }
        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            IsModuleLoaded = true;
        }
        void ShowFloatGroups(DockLayoutManager dockLayoutManager) {
            if(dockLayoutManager.IsDisposing) return;
            foreach(FloatGroup floatGroup in dockLayoutManager.FloatGroups) {
                if(GetShouldRestoreOnActivate(floatGroup)) {
                    SetShouldRestoreOnActivate(floatGroup, false);
                    if(!dockLayoutManager.IsVisible)
                        floatGroup.ShouldRestoreOnActivate = true;
                    else
                        floatGroup.IsOpen = true;
                }
            }
        }
    }
}
