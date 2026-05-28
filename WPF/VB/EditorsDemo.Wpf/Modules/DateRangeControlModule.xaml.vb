Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Data.Utils
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo

    Public Partial Class DateRangeControlModule

        Public Sub New()
            Me.InitializeComponent()
            Me.DateRangeControl.PredefinedRanges.Clear()
            Me.DateRangeControl.PredefinedRanges.Add(New DevExpress.Xpf.Editors.PredefinedDateRange("1 week", System.DateTime.Today.AddDays(-6), System.DateTime.Today))
            Me.DateRangeControl.PredefinedRanges.Add(New DevExpress.Xpf.Editors.PredefinedDateRange("2 weeks", System.DateTime.Today.AddDays(-13), System.DateTime.Today))
            Me.DateRangeControl.PredefinedRanges.Add(New DevExpress.Xpf.Editors.PredefinedDateRange("1 month", System.DateTime.Today.AddMonths(-1), System.DateTime.Today))
            Me.DateRangeControl.PredefinedRanges.Add(New DevExpress.Xpf.Editors.PredefinedDateRange("3 months", System.DateTime.Today.AddMonths(-3), System.DateTime.Today))
        End Sub
    End Class

    Friend Class ChartViewModel

        Public Sub New()
            Me.Min = System.DateTime.Today.AddMonths(-12)
            Me.Max = System.DateTime.Today
            Me.FullSeries = New System.Collections.Generic.List(Of EditorsDemo.DataPoint)()
            Dim rand As DevExpress.Data.Utils.NonCryptographicRandom = DevExpress.Data.Utils.NonCryptographicRandom.[Default]
            Dim [date] = Me.Min
            While [date] <= Me.Max
                Me.FullSeries.Add(New EditorsDemo.DataPoint With {.[Date] = [date], .Value = 1000 + rand.[Next](-5, 5)})
                [date] = [date].AddDays(1)
            End While

            Me.Start = System.DateTime.Today.AddDays(-6)
            Me.[End] = System.DateTime.Today
            Me.Update()
        End Sub

        Private ReadOnly FullSeries As System.Collections.Generic.List(Of EditorsDemo.DataPoint)

        Public ReadOnly Property Series As ObservableCollection(Of EditorsDemo.DataPoint) = New System.Collections.ObjectModel.ObservableCollection(Of EditorsDemo.DataPoint)()

        Public ReadOnly Property Min As DateTime

        Public ReadOnly Property Max As DateTime

        Private startField As System.DateTime?

        Public Property Start As System.DateTime?
            Get
                Return Me.startField
            End Get

            Set(ByVal value As System.DateTime?)
                Me.startField = value
                Me.Update()
            End Set
        End Property

        Private endField As System.DateTime?

        Public Property [End] As System.DateTime?
            Get
                Return Me.endField
            End Get

            Set(ByVal value As System.DateTime?)
                Me.endField = value
                Me.Update()
            End Set
        End Property

        Private Sub Update()
            Dim min = If(Me.Start, Me.Min)
            Dim max = If(Me.[End], Me.Max)
            Me.Series.Clear()
            Me.FullSeries.Where(Function(dp) dp.[Date] >= min AndAlso dp.[Date] <= max).ForEach(New Global.System.Action(Of Global.EditorsDemo.DataPoint)(AddressOf Me.Series.Add))
        End Sub
    End Class

    Public Class DataPoint

        Public Property [Date] As DateTime

        Public Property Value As Integer
    End Class
End Namespace
