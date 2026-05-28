using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.DemoData.Helpers;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Ribbon;
using Microsoft.Win32;
using RibbonDemo;

namespace RibbonDemo {
    [CodeFiles(@"Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml;
                 Modules/RibbonMerging/Views/RibbonMergingUserControl.xaml.(cs);
                 Modules/RibbonMerging/Views/PaintUserControl.xaml;
                 Modules/RibbonMerging/Views/PaintUserControl.xaml.(cs);
                 Modules/RibbonMerging/Views/TextUserControl.xaml;
                 Modules/RibbonMerging/Views/TextUserControl.xaml.(cs);                 
                 Modules/RibbonMerging/ViewModels/PaintPanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/TextPanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/PanelViewModel.(cs);
                 Modules/RibbonMerging/ViewModels/RibbonMergingViewModel.(cs);")]

    public partial class RibbonMergingUserControl : RibbonDemoModule {
        public RibbonMergingUserControl() {
            InitializeComponent();
            Ribbon = mainRibbon;
        }

        protected override void Hide() {
            mainRibbon.CloseApplicationMenu();
            base.Hide();
        }
    }
}
