Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.ViewModels

    Public Interface IMapRouteService

        Function CalculateRouteAsync(ByVal waypoints As System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Map.RouteWaypoint), ByVal travelMode As DevExpress.Xpf.Map.AzureTravelMode) As Task(Of DevExpress.Xpf.Map.AzureRouteCalculationResult)

    End Interface
End Namespace

Namespace DevExpress.DevAV

    Public Class MapRouteService
        Inherits DevExpress.Mvvm.UI.ServiceBase
        Implements DevExpress.DevAV.ViewModels.IMapRouteService

        Private ReadOnly Property Layer As InformationLayer
            Get
                Return CType(Me.AssociatedObject, DevExpress.Xpf.Map.InformationLayer)
            End Get
        End Property

        Private ReadOnly Property Provider As AzureRouteDataProvider
            Get
                Return CType(Me.Layer.DataProvider, DevExpress.Xpf.Map.AzureRouteDataProvider)
            End Get
        End Property

        Private taskSource As System.Threading.Tasks.TaskCompletionSource(Of DevExpress.Xpf.Map.AzureRouteCalculationResult)

        Public Function CalculateRouteAsync(ByVal waypoints As System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Map.RouteWaypoint), ByVal travelMode As DevExpress.Xpf.Map.AzureTravelMode) As Task(Of DevExpress.Xpf.Map.AzureRouteCalculationResult) Implements Global.DevExpress.DevAV.ViewModels.IMapRouteService.CalculateRouteAsync
            Me.taskSource = New System.Threading.Tasks.TaskCompletionSource(Of DevExpress.Xpf.Map.AzureRouteCalculationResult)()
            Dim options As DevExpress.Xpf.Map.AzureRouteOptions = New DevExpress.Xpf.Map.AzureRouteOptions() With {.TravelMode = travelMode, .InstructionsType = DevExpress.Xpf.Map.AzureRouteInstructionsType.Text}
            Me.Provider.CalculateRoute(waypoints.ToList(), options)
            AddHandler Me.Provider.RouteCalculated, AddressOf Me.Provider_RouteCalculated
            Return Me.taskSource.Task
        End Function

        Private Sub Provider_RouteCalculated(ByVal sender As Object, ByVal e As DevExpress.Xpf.Map.AzureRouteCalculatedEventArgs)
            RemoveHandler Me.Provider.RouteCalculated, AddressOf Me.Provider_RouteCalculated
            If e.Cancelled Then
                Me.taskSource.SetCanceled()
            ElseIf e.[Error] IsNot Nothing Then
                Me.taskSource.SetException(e.[Error])
            Else
                Me.taskSource.SetResult(e.CalculationResult)
            End If
        End Sub
    End Class
End Namespace
