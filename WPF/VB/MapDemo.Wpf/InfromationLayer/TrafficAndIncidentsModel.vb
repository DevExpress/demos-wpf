Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Media.Effects
Imports System.Windows.Threading
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class TrafficAndIncidentsModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private ReadOnly mainModule As MapDemo.TrafficAndIncidents

        Private ReadOnly itemsField As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem)

        Private ReadOnly waypointsField As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.RouteWaypoint)

        Private ReadOnly trafficIncidentTimer As System.Windows.Threading.DispatcherTimer = New System.Windows.Threading.DispatcherTimer()

        Private ReadOnly routePropertiesTimer As System.Windows.Threading.DispatcherTimer = New System.Windows.Threading.DispatcherTimer()

        Private viewport As DevExpress.Map.MapBounds

        Private requestErrorMessageField As String

        Public ReadOnly Property Items As ObservableCollection(Of DevExpress.Xpf.Map.MapItem)
            Get
                Return Me.itemsField
            End Get
        End Property

        Public ReadOnly Property Waypoints As ObservableCollection(Of DevExpress.Xpf.Map.MapItem)
            Get
                Return Me.itemsField
            End Get
        End Property

        Public Property MapCenterPoint As CoordPoint

        Public ReadOnly Property RequestErrorMessage As String
            Get
                Return Me.requestErrorMessageField
            End Get
        End Property

        Public ReadOnly Property RequestErrorMessageVisible As Boolean
            Get
                Return Not Equals(Me.requestErrorMessageField, Nothing)
            End Get
        End Property

        Public Sub New(ByVal [module] As MapDemo.TrafficAndIncidents)
            Me.mainModule = [module]
            Me.MapCenterPoint = New DevExpress.Xpf.Map.GeoPoint(38.90507, -77.01909)
            Me.itemsField = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapItem)()
            Me.waypointsField = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.RouteWaypoint)()
            Me.AddWaypoint("A", New DevExpress.Xpf.Map.GeoPoint(39.025538, -77.203833))
            Me.AddWaypoint("B", New DevExpress.Xpf.Map.GeoPoint(38.960899, -76.98296))
            Me.AddWaypoint("C", New DevExpress.Xpf.Map.GeoPoint(38.905435, -77.127861))
            Me.AddWaypoint("D", New DevExpress.Xpf.Map.GeoPoint(38.8501856, -76.9276787))
            Me.trafficIncidentTimer.Interval = System.TimeSpan.FromSeconds(2.5)
            AddHandler Me.trafficIncidentTimer.Tick, Sub(s, e) Me.SendTrafficIncidentsRequest()
            Me.routePropertiesTimer.Interval = System.TimeSpan.FromSeconds(0.5)
            AddHandler Me.routePropertiesTimer.Tick, Sub(s, e) Me.SendRouteRequest()
        End Sub

        Private Sub SendTrafficIncidentsRequest()
            Me.trafficIncidentTimer.[Stop]()
            Me.mainModule.IncidentLayer.ClearResults()
            Dim boundingBox As DevExpress.Xpf.Map.SearchBoundingBox = New DevExpress.Xpf.Map.SearchBoundingBox() With {.WestLongitude = Me.viewport.Left, .NorthLatitude = Me.viewport.Top, .EastLongitude = Me.viewport.Right, .SouthLatitude = Me.viewport.Bottom}
            Dim options As DevExpress.Xpf.Map.AzureTrafficIncidentOptions = New DevExpress.Xpf.Map.AzureTrafficIncidentOptions()
            Me.mainModule.IncidentDataProvider.RequestTrafficIncidents(boundingBox, CInt(Me.mainModule.mapControl.ZoomLevel), -1, options)
        End Sub

        Public Sub AddWaypoint(ByVal description As String, ByVal location As DevExpress.Xpf.Map.GeoPoint)
            If Me.waypointsField.Count = 4 Then Me.waypointsField.Clear()
            Dim pinLetter As Char = System.Convert.ToChar(System.Convert.ToInt32("A"c) + Me.waypointsField.Count)
            Dim name As String = New String(pinLetter, 1)
            Me.waypointsField.Add(New DevExpress.Xpf.Map.RouteWaypoint(description, location))
            Me.Items.Add(New DevExpress.Xpf.Map.MapPushpin() With {.Location = location, .Information = description, .TraceDepth = 0, .Text = name})
            Me.SendRouteRequest()
        End Sub

        Public Sub ProcessRouteItems(ByVal items As DevExpress.Xpf.Map.MapItem())
            Dim pushpinIndex As Integer = 0
            For Each item As DevExpress.Xpf.Map.MapItem In items
                Dim pushpin As DevExpress.Xpf.Map.MapPushpin = TryCast(item, DevExpress.Xpf.Map.MapPushpin)
                If pushpin IsNot Nothing Then
                    Dim pinLetter As Char = System.Convert.ToChar(System.Convert.ToInt32("A"c) + System.Math.Min(System.Threading.Interlocked.Increment(pushpinIndex), pushpinIndex - 1))
                    pushpin.Text = New String(pinLetter, 1)
                End If

                Dim polyline As DevExpress.Xpf.Map.MapPolyline = TryCast(item, DevExpress.Xpf.Map.MapPolyline)
                If polyline IsNot Nothing Then polyline.Effect = New System.Windows.Media.Effects.DropShadowEffect() With {.Direction = -90, .ShadowDepth = 1}
            Next

            Me.Items.Clear()
        End Sub

        Public Sub UpdateViewport(ByVal newViewport As DevExpress.Map.MapBounds)
            Me.viewport = newViewport
            Me.trafficIncidentTimer.[Stop]()
            Me.trafficIncidentTimer.Start()
        End Sub

        Public Sub UpdateRoute()
            Me.requestErrorMessageField = Nothing
            Me.RaisePropertyChanged(NameOf(MapDemo.TrafficAndIncidentsModel.RequestErrorMessage))
            Me.RaisePropertyChanged(NameOf(MapDemo.TrafficAndIncidentsModel.RequestErrorMessageVisible))
            Me.routePropertiesTimer.[Stop]()
            Me.routePropertiesTimer.Start()
        End Sub

        Public Sub UpdateErrorMessage(ByVal exception As System.Exception)
            Dim message As String = exception.Message
            Const startPattern As String = """message"":"""
            Dim startIndex As Integer = message.IndexOf(startPattern)
            If startIndex < 0 Then Return
            message = message.Substring(startIndex + startPattern.Length)
            message = message.Substring(0, message.IndexOf(""""))
            Me.requestErrorMessageField = message
            Me.RaisePropertyChanged(NameOf(MapDemo.TrafficAndIncidentsModel.RequestErrorMessage))
            Me.RaisePropertyChanged(NameOf(MapDemo.TrafficAndIncidentsModel.RequestErrorMessageVisible))
        End Sub

        Public Sub SendRouteRequest()
            Me.routePropertiesTimer.[Stop]()
            If Me.waypointsField.Count = 4 Then
                Dim options As DevExpress.Xpf.Map.AzureRouteOptions = New DevExpress.Xpf.Map.AzureRouteOptions() With {.TravelMode = CType(Me.mainModule.travelModeRadioGroup.EditValue, DevExpress.Xpf.Map.AzureTravelMode), .AvoidTypes = CType(MapDemo.DemoUtils.ConvertListObjectToEnumFlags(Me.mainModule.avoidTypesComboBox.EditValue), DevExpress.Xpf.Map.AzureRouteAvoidType), .SectionTypes = CType(MapDemo.DemoUtils.ConvertListObjectToEnumFlags(Me.mainModule.sectionTypesComboBox.EditValue), DevExpress.Xpf.Map.AzureRouteSectionType), .MaxAlternatives = CInt(Me.mainModule.maxAlternativesSpinEdit.Value), .OptimizeWaypoints = CBool(Me.mainModule.optimizeWaypointsCheckBox.EditValue)}
                Me.mainModule.RouteDataProvider.CalculateRoute(New System.Collections.Generic.List(Of DevExpress.Xpf.Map.RouteWaypoint)(Me.waypointsField), options)
            End If
        End Sub
    End Class
End Namespace
