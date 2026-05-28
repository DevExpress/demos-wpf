using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;

namespace RibbonDemo {
    public partial class PainterWindow {
        public PainterWindow() {
            InitializeComponent();
        }

        void OnEditValueChanged(object sender, RoutedEventArgs e) {
            NavigationTree.ExitMenuMode(true, false);
        }
    }
}
