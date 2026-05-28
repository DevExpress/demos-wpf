Imports System
Imports System.Collections.Generic

Namespace DevExpress.SalesDemo.Model

    Public Module DataSource

        Private instance As IDataProvider

        Public Function GetDataProvider() As IDataProvider
            If instance Is Nothing Then Call EnsureDataProvider()
            Return instance
        End Function

        Public Sub EnsureDataProvider()
            If instance IsNot Nothing Then Return
            Dim dbPath As String = Internal.DataDirectoryHelper.GetDataFile("sales.mdb")
            IO.File.SetAttributes(dbPath, IO.FileAttributes.Normal)
            Dim generator = New Demos.SalesDBGenerator.SalesGenerator()
            Using ProgressTracker.Instance.StartTracking(generator)
                generator.Run(OleDataProvider.GetConnectionString(dbPath))
                instance = New OleDataProvider(dbPath)
            End Using
        End Sub

        Public ReadOnly Property Progress As IObservable(Of Integer)
            Get
                Return ProgressTracker.Instance
            End Get
        End Property

#Region "IObservable"
        Private NotInheritable Class ProgressTracker
            Implements IObservable(Of Integer)

            Friend Shared ReadOnly Instance As ProgressTracker = New ProgressTracker()

            Private ReadOnly observers As IList(Of IObserver(Of Integer))

            Private Sub New()
                observers = New List(Of IObserver(Of Integer))()
            End Sub

            Public Function StartTracking(ByVal generator As IDataGenerator) As IDisposable
                Return New TrackingContext(generator, Me)
            End Function

            Private Function Subscribe(ByVal observer As IObserver(Of Integer)) As IDisposable Implements IObservable(Of Integer).Subscribe
                Return New Subscription(Me, observer)
            End Function

            Private Sub generator_Start(ByVal sender As Object, ByVal e As ProgressEventArgs)
                For Each observer As IObserver(Of Integer) In observers
                    observer.OnNext(e.Progress)
                Next
            End Sub

            Private Sub generator_Complete(ByVal sender As Object, ByVal e As ProgressEventArgs)
                For Each observer As IObserver(Of Integer) In observers
                    observer.OnCompleted()
                Next
            End Sub

            Private Sub generator_Progress(ByVal sender As Object, ByVal e As ProgressEventArgs)
                For Each observer As IObserver(Of Integer) In observers
                    observer.OnNext(e.Progress)
                Next
            End Sub

            Private NotInheritable Class TrackingContext
                Implements IDisposable

                Private ReadOnly generator As IDataGenerator

                Private ReadOnly tracker As ProgressTracker

                Public Sub New(ByVal generator As IDataGenerator, ByVal tracker As ProgressTracker)
                    Me.generator = generator
                    Me.tracker = tracker
                    AddHandler generator.GenerationStart, AddressOf OnGenerationStart
                    AddHandler generator.GenerationComplete, AddressOf OnGenerationComplete
                    AddHandler generator.GenerationProgress, AddressOf OnGenerationProgress
                End Sub

                Private Sub Dispose() Implements IDisposable.Dispose
                    RemoveHandler generator.GenerationStart, AddressOf OnGenerationStart
                    RemoveHandler generator.GenerationComplete, AddressOf OnGenerationComplete
                    RemoveHandler generator.GenerationProgress, AddressOf OnGenerationProgress
                End Sub

                Private Sub OnGenerationStart(ByVal sender As Object, ByVal e As ProgressEventArgs)
                    tracker.generator_Start(sender, e)
                End Sub

                Private Sub OnGenerationComplete(ByVal sender As Object, ByVal e As ProgressEventArgs)
                    tracker.generator_Complete(sender, e)
                End Sub

                Private Sub OnGenerationProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)
                    tracker.generator_Progress(sender, e)
                End Sub
            End Class

            Private NotInheritable Class Subscription
                Implements IDisposable

                Private ReadOnly observer As IObserver(Of Integer)

                Private ReadOnly tracker As ProgressTracker

                Public Sub New(ByVal tracker As ProgressTracker, ByVal observer As IObserver(Of Integer))
                    If Not tracker.observers.Contains(observer) Then tracker.observers.Add(observer)
                    Me.tracker = tracker
                    Me.observer = observer
                End Sub

                Private Sub Dispose() Implements IDisposable.Dispose
                    tracker.observers.Remove(observer)
                End Sub
            End Class
        End Class
#End Region  ' IObservable
    End Module

    Public Class SalesGroup

        Public Sub New(ByVal groupName As String, ByVal totalCost As Decimal, ByVal unitsSold As Integer, ByVal startOfPeriod As Date, ByVal endOfPeriod As Date)
            Me.GroupName = groupName
            Me.TotalCost = totalCost
            Units = unitsSold
            Me.StartOfPeriod = startOfPeriod
            Me.EndOfPeriod = endOfPeriod
        End Sub

        Public ReadOnly Property GroupName As String

        Public ReadOnly Property TotalCost As Decimal

        Public ReadOnly Property Units As Integer

        Public ReadOnly Property StartOfPeriod As Date

        Public ReadOnly Property EndOfPeriod As Date
    End Class

    Public Enum GroupingPeriod
        Hour
        Day
        All
        None
    End Enum
End Namespace
