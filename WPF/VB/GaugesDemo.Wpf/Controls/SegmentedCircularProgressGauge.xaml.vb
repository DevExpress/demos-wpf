Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace GaugesDemo.Controls

    Public Partial Class SegmentedCircularProgressGauge
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Shared ReadOnly ThicknessProperty As DependencyProperty = DependencyProperty.Register(NameOf(SegmentedCircularProgressGauge.Thickness), GetType(Integer), GetType(SegmentedCircularProgressGauge), New PropertyMetadata(15))

        Public Shared ReadOnly ProgressThicknessProperty As DependencyProperty = DependencyProperty.Register(NameOf(SegmentedCircularProgressGauge.ProgressThickness), GetType(Integer), GetType(SegmentedCircularProgressGauge), New PropertyMetadata(15))

        Public Shared ReadOnly Segment1BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(SegmentedCircularProgressGauge.Segment1Brush), GetType(Brush), GetType(SegmentedCircularProgressGauge), New PropertyMetadata(Brushes.Coral))

        Public Shared ReadOnly Segment2BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(SegmentedCircularProgressGauge.Segment2Brush), GetType(Brush), GetType(SegmentedCircularProgressGauge), New PropertyMetadata(Brushes.LightCoral))

        Public Shared ReadOnly Segment3BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(SegmentedCircularProgressGauge.Segment3Brush), GetType(Brush), GetType(SegmentedCircularProgressGauge), New PropertyMetadata(Brushes.Gold))

        Public Shared ReadOnly Segment4BrushProperty As DependencyProperty = DependencyProperty.Register(NameOf(SegmentedCircularProgressGauge.Segment4Brush), GetType(Brush), GetType(SegmentedCircularProgressGauge), New PropertyMetadata(Brushes.LightGreen))

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

        Public Property Segment1Brush As Brush
            Get
                Return CType(GetValue(Segment1BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Segment1BrushProperty, value)
            End Set
        End Property

        Public Property Segment2Brush As Brush
            Get
                Return CType(GetValue(Segment2BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Segment2BrushProperty, value)
            End Set
        End Property

        Public Property Segment3Brush As Brush
            Get
                Return CType(GetValue(Segment3BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Segment3BrushProperty, value)
            End Set
        End Property

        Public Property Segment4Brush As Brush
            Get
                Return CType(GetValue(Segment4BrushProperty), Brush)
            End Get

            Set(ByVal value As Brush)
                SetValue(Segment4BrushProperty, value)
            End Set
        End Property
    End Class
End Namespace
