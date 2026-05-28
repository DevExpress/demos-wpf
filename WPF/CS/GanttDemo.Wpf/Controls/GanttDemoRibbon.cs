using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Gantt;

namespace GanttDemo {
    public partial class GanttDemoRibbon : UserControl {
        public static readonly DependencyProperty ExportGanttProperty =
             DependencyProperty.Register("ExportGantt", typeof(GanttControl), typeof(GanttDemoRibbon), new PropertyMetadata(null));
        public GanttControl ExportGantt {
            get { return (GanttControl)GetValue(ExportGanttProperty); }
            set { SetValue(ExportGanttProperty, value); }
        }
        public GanttDemoRibbon() {
            InitializeComponent();
        }
    }
}
