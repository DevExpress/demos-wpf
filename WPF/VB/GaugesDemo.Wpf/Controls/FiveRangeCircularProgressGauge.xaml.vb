Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace GaugesDemo.Controls

    Public Partial Class FiveRangeCircularProgressGauge
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Shared ReadOnly ThicknessProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.Thickness), GetType(Integer), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(15))

        Public Shared ReadOnly ProgressThicknessProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.ProgressThickness), GetType(Integer), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(15))

        Public Shared ReadOnly MinValueProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.MinValue), GetType(Double), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(0R))

        Public Shared ReadOnly MaxValueProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.MaxValue), GetType(Double), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(100R))

        Public Shared ReadOnly Range1BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.Range1Brush), GetType(Brush), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(New SolidColorBrush(CType(ColorConverter.ConvertFromString("#FFC106"), Color))))

        Public Shared ReadOnly Range2BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.Range2Brush), GetType(Brush), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(New SolidColorBrush(CType(ColorConverter.ConvertFromString("#CCDC3B"), Color))))

        Public Shared ReadOnly Range3BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.Range3Brush), GetType(Brush), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(New SolidColorBrush(CType(ColorConverter.ConvertFromString("#FF5E01"), Color))))

        Public Shared ReadOnly Range4BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(FiveRangeCircularProgressGauge.Range4Brush), GetType(Brush), GetType(FiveRangeCircularProgressGauge), New PropertyMetadata(New SolidColorBrush(CType(ColorConverter.ConvertFromString("#29B6F6"), Color))))

        Public Property Thickness As Integer
            Get
                Return CInt(GetValue(ThicknessProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(ThicknessProperty, value)
            End Set
        End Property

        Public Property ProgressThickness As Integer
            Get
                Return CInt(GetValue(ProgressThicknessProperty))
            End Get

            Set(ByVal value As Integer)
                SetValue(ProgressThicknessProperty, value)
            End Set
        End Property

        Public Property MinValue As Double
            Get
                Return CDbl(GetValue(MinValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MinValueProperty, value)
            End Set
        End Property

        Public Property MaxValue As Double
            Get
                Return CDbl(GetValue(MaxValueProperty))
            End Get

            Set(ByVal value As Double)
                SetValue(MaxValueProperty, value)
            End Set
        End Property

#Region "Range Brushes"
        Public Property Range1Brush As Brush
            Get
                Return CType(GetValue(Range1BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Range1BrushProperty, value)
            End Set
        End Property

        Public Property Range2Brush As Brush
            Get
                Return CType(GetValue(Range2BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Range2BrushProperty, value)
            End Set
        End Property

        Public Property Range3Brush As Brush
            Get
                Return CType(GetValue(Range3BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Range3BrushProperty, value)
            End Set
        End Property

        Public Property Range4Brush As Brush
            Get
                Return CType(GetValue(Range4BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Range4BrushProperty, value)
            End Set
        End Property
#End Region  ' Range Brushes
    End Class
End Namespace
