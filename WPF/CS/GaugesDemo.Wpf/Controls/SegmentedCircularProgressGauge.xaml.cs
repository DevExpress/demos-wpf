using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GaugesDemo.Controls {
    public partial class SegmentedCircularProgressGauge : UserControl {
        public SegmentedCircularProgressGauge() {
            InitializeComponent();
        }
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register(nameof(Thickness), typeof(int), typeof(SegmentedCircularProgressGauge), new PropertyMetadata(15));

        public static readonly DependencyProperty ProgressThicknessProperty =
            DependencyProperty.Register(nameof(ProgressThickness), typeof(int), typeof(SegmentedCircularProgressGauge), new PropertyMetadata(15));

        public static readonly DependencyProperty Segment1BrushProperty =
            DependencyProperty.Register(nameof(Segment1Brush), typeof(Brush), typeof(SegmentedCircularProgressGauge), new PropertyMetadata(Brushes.Coral));

        public static readonly DependencyProperty Segment2BrushProperty =
            DependencyProperty.Register(nameof(Segment2Brush), typeof(Brush), typeof(SegmentedCircularProgressGauge), new PropertyMetadata(Brushes.LightCoral));

        public static readonly DependencyProperty Segment3BrushProperty =
            DependencyProperty.Register(nameof(Segment3Brush), typeof(Brush), typeof(SegmentedCircularProgressGauge), new PropertyMetadata(Brushes.Gold));

        public static readonly DependencyProperty Segment4BrushProperty =
            DependencyProperty.Register(nameof(Segment4Brush), typeof(Brush), typeof(SegmentedCircularProgressGauge), new PropertyMetadata(Brushes.LightGreen));

        public int Thickness {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        public int ProgressThickness {
            get { return (int)GetValue(ProgressThicknessProperty); }
            set { SetValue(ProgressThicknessProperty, value); }
        }
        public Brush Segment1Brush {
            get { return (Brush)GetValue(Segment1BrushProperty); }
            set { SetValue(Segment1BrushProperty, value); }
        }
        public Brush Segment2Brush {
            get { return (Brush)GetValue(Segment2BrushProperty); }
            set { SetValue(Segment2BrushProperty, value); }
        }
        public Brush Segment3Brush {
            get { return (Brush)GetValue(Segment3BrushProperty); }
            set { SetValue(Segment3BrushProperty, value); }
        }
        public Brush Segment4Brush {
            get { return (Brush)GetValue(Segment4BrushProperty); }
            set { SetValue(Segment4BrushProperty, value); }
        }
    }
}
