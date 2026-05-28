using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GaugesDemo.Controls {
    public partial class FiveRangeCircularProgressGauge : UserControl {
        public FiveRangeCircularProgressGauge() {
            InitializeComponent();
        }

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register(nameof(Thickness), typeof(int), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(15));

        public static readonly DependencyProperty ProgressThicknessProperty =
            DependencyProperty.Register(nameof(ProgressThickness), typeof(int), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(15));

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(nameof(MinValue), typeof(double), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(0d));

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(nameof(MaxValue), typeof(double), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(100d));

        public static readonly DependencyProperty Range1BrushProperty =
            DependencyProperty.Register(nameof(Range1Brush), typeof(Brush), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC106"))));

        public static readonly DependencyProperty Range2BrushProperty =
            DependencyProperty.Register(nameof(Range2Brush), typeof(Brush), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDC3B"))));

        public static readonly DependencyProperty Range3BrushProperty =
                   DependencyProperty.Register(nameof(Range3Brush), typeof(Brush), typeof(FiveRangeCircularProgressGauge),
                       new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5E01"))));

        public static readonly DependencyProperty Range4BrushProperty =
            DependencyProperty.Register(nameof(Range4Brush), typeof(Brush), typeof(FiveRangeCircularProgressGauge),
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#29B6F6"))));


        public int Thickness {
            get { return (int)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        public int ProgressThickness {
            get { return (int)GetValue(ProgressThicknessProperty); }
            set { SetValue(ProgressThicknessProperty, value); }
        }

        public double MinValue {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public double MaxValue {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        #region Range Brushes

        public Brush Range1Brush {
            get { return (Brush)GetValue(Range1BrushProperty); }
            set { SetValue(Range1BrushProperty, value); }
        }

        public Brush Range2Brush {
            get { return (Brush)GetValue(Range2BrushProperty); }
            set { SetValue(Range2BrushProperty, value); }
        }

        public Brush Range3Brush {
            get { return (Brush)GetValue(Range3BrushProperty); }
            set { SetValue(Range3BrushProperty, value); }
        }

        public Brush Range4Brush {
            get { return (Brush)GetValue(Range4BrushProperty); }
            set { SetValue(Range4BrushProperty, value); }
        }

        #endregion Range Brushes
    }
}
