Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.ViewModels

    Public Interface INavigatorMapViewModel

        Sub NewPushpinCreated(ByVal newPushpin As DevExpress.Xpf.Map.MapPushpin)

    End Interface

    Public Class NavigatorMapViewModel(Of TEntity As Class)
        Inherits DevExpress.DevAV.ViewModels.MapViewModelBase
        Implements DevExpress.Mvvm.IDocumentContent, DevExpress.DevAV.ViewModels.INavigatorMapViewModel

        Private _DisplayEntity As TEntity

        Public Shared Function Create(ByVal displayEntity As TEntity, ByVal startingAddress As String, ByVal startingLocation As DevExpress.Xpf.Map.GeoPoint, ByVal destinationAddress As String, ByVal destinationLocation As DevExpress.Xpf.Map.GeoPoint, ByVal Optional applyDestination As System.Action(Of DevExpress.DevAV.Address) = Nothing) As NavigatorMapViewModel(Of TEntity)
            Return DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New DevExpress.DevAV.ViewModels.NavigatorMapViewModel(Of TEntity)(displayEntity, startingAddress, startingLocation, destinationAddress, destinationLocation, applyDestination))
        End Function

        Const maximumWalkingDistance As Double = 7.0

        Private linksViewModelField As DevExpress.DevAV.ViewModels.LinksViewModel

        Protected Sub New(ByVal displayEntity As TEntity, ByVal startingAddress As String, ByVal startingLocation As DevExpress.Xpf.Map.GeoPoint, ByVal destinationAddress As String, ByVal destinationLocation As DevExpress.Xpf.Map.GeoPoint, ByVal applyDestination As System.Action(Of DevExpress.DevAV.Address))
            Me.DisplayEntity = displayEntity
            Me.StartingAddress = startingAddress
            Me.StartingLocation = startingLocation
            Me.DestinationAddress = destinationAddress
            Me.DestinationLocation = destinationLocation
            Me.CenterPoint = New DevExpress.Xpf.Map.GeoPoint((startingLocation.Latitude + destinationLocation.Latitude) / 2, (startingLocation.Longitude + destinationLocation.Longitude) / 2)
            Me.IsWalkingAvailable = False
            Me.applyDestination = applyDestination
        End Sub

        Public Property DisplayEntity As TEntity
            Get
                Return _DisplayEntity
            End Get

            Private Set(ByVal value As TEntity)
                _DisplayEntity = value
            End Set
        End Property

        Public Property Destination As Address

        Public ReadOnly Property IsEditingMode As Boolean
            Get
                Return Me.applyDestination IsNot Nothing
            End Get
        End Property

        Public Overridable Property StartingAddress As String

        Public Overridable Property StartingLocation As GeoPoint

        Public Overridable Property DestinationAddress As String

        Public Overridable Property DestinationLocation As GeoPoint

        Protected ReadOnly Property RouteService As IMapRouteService
            Get
                Return Me.GetRequiredService(Of DevExpress.DevAV.ViewModels.IMapRouteService)()
            End Get
        End Property

        Protected ReadOnly Property MapPushpinsService As IMapPushpinsService
            Get
                Return Me.GetRequiredService(Of DevExpress.DevAV.ViewModels.IMapPushpinsService)()
            End Get
        End Property

        Public Overridable Property ActiveItinerary As List(Of DevExpress.DevAV.ViewModels.RoutePoint)

        Public Overridable Property SelectedItineraryItem As RoutePoint

        Public Overridable Property CenterPoint As GeoPoint

        Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return Me.GetService(Of DevExpress.Mvvm.IDocumentManagerService)()
            End Get
        End Property

        Public Overridable Property IsWalkingAvailable As Boolean

        Public Overridable Property IsWalkingRoute As Boolean

        Private applyDestination As System.Action(Of DevExpress.DevAV.Address)

        Public Sub OnSelectedItineraryItemChanged()
            If Me.SelectedItineraryItem IsNot Nothing Then
                Me.CenterPoint = Me.SelectedItineraryItem.Location
            End If
        End Sub

        Public Sub SaveAndClose()
            Me.applyDestination(Me.Destination)
            Me.Close()
        End Sub

        Public Function CanSaveAndClose() As Boolean
            Return Me.applyDestination IsNot Nothing AndAlso Me.Destination IsNot Nothing
        End Function

        Public Sub Swap()
            Me.applyDestination = Nothing
            Me.RaisePropertyChanged(Function(x) x.IsEditingMode)
            Dim address As String = Me.StartingAddress
            Me.StartingAddress = Me.DestinationAddress
            Me.DestinationAddress = address
            Dim location As DevExpress.Xpf.Map.GeoPoint = Me.StartingLocation
            Me.StartingLocation = Me.DestinationLocation
            Me.DestinationLocation = location
            Me.CalculateRouteDriving()
        End Sub

        Public Sub CalculateRouteDriving()
            Me.CalculateRoute(DevExpress.Xpf.Map.AzureTravelMode.Car)
            Me.IsWalkingRoute = False
        End Sub

        Public Sub CalculateRouteWalking()
            Me.CalculateRoute(DevExpress.Xpf.Map.AzureTravelMode.Pedestrian)
            Me.IsWalkingRoute = True
        End Sub

        Private isBusy As Boolean = False

        Private Sub CalculateRoute(ByVal mode As DevExpress.Xpf.Map.AzureTravelMode)
            If Me.isBusy Then Return
            Me.isBusy = True
            Dim waypoints = {New DevExpress.Xpf.Map.RouteWaypoint(Me.StartingAddress, Me.StartingLocation), New DevExpress.Xpf.Map.RouteWaypoint(Me.DestinationAddress, Me.DestinationLocation)}
            Me.RouteService.CalculateRouteAsync(CType((waypoints), System.Collections.Generic.IEnumerable(Of DevExpress.Xpf.Map.RouteWaypoint)), CType((mode), DevExpress.Xpf.Map.AzureTravelMode)).ContinueWith(Sub(t)
                If t.Result.ResultCode = DevExpress.Xpf.Map.RequestResultCode.Success AndAlso t.Result.RouteResults.Count > 0 Then
                    Dim route As DevExpress.Xpf.Map.AzureRouteResult = t.Result.RouteResults.First()
                    Me.IsWalkingAvailable = If((route.Summary.TravelLengthMeters > DevExpress.DevAV.ViewModels.NavigatorMapViewModel(Of TEntity).maximumWalkingDistance), False, True)
                    If route.Legs.Count > 0 Then
                        Dim leg As DevExpress.Xpf.Map.AzureRouteLeg = route.Legs.First()
                        Dim startLocation As DevExpress.Xpf.Map.GeoPoint = leg.Points.First()
                        Dim endLocation As DevExpress.Xpf.Map.GeoPoint = leg.Points.Last()
                        Me.CenterPoint = New DevExpress.Xpf.Map.GeoPoint((startLocation.Latitude + endLocation.Latitude) / 2, (startLocation.Longitude + endLocation.Longitude) / 2)
                    End If

                    Me.ActiveItinerary = route.Guidance.Instructions.[Select](Function(item) New DevExpress.DevAV.ViewModels.RoutePoint(item)).ToList()
                End If

                Me.isBusy = False
            End Sub, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext())
        End Sub

        Public Overridable Sub OnLoaded()
            Me.CalculateRouteDriving()
        End Sub

        Private Sub NewPushpinCreated(ByVal newPushpin As DevExpress.Xpf.Map.MapPushpin) Implements Global.DevExpress.DevAV.ViewModels.INavigatorMapViewModel.NewPushpinCreated
            AddHandler newPushpin.MouseLeftButtonDown, Sub(s, e)
                Dim pushpin As DevExpress.Xpf.Map.MapPushpin = TryCast(s, DevExpress.Xpf.Map.MapPushpin)
                If(pushpin IsNot Nothing) AndAlso (pushpin.State = DevExpress.Xpf.Map.MapPushpinState.Normal) Then
                    Dim locationInformation As DevExpress.Xpf.Map.LocationInformation = TryCast(pushpin.Information, DevExpress.Xpf.Map.LocationInformation)
                    Me.DestinationAddress = locationInformation.Address.FormattedAddress
                    Me.DestinationLocation = CType(pushpin.Location, DevExpress.Xpf.Map.GeoPoint)
                    pushpin.Text = "A"
                    Dim rx As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("(.*?), (.*?), (.*?) (.*)")
                    Dim match = rx.Match(locationInformation.Address.FormattedAddress)
                    Dim streetLine As String = match.Groups(CInt((1))).ToString().Trim()
                    Dim city As String = match.Groups(CInt((2))).ToString().Trim()
                    Dim state As String = match.Groups(CInt((3))).ToString().Trim()
                    Dim zipcode As String = match.Groups(CInt((4))).ToString().Trim()
                    Dim stateEnum As DevExpress.DevAV.StateEnum = DevExpress.DevAV.StateEnum.WY
                    System.[Enum].TryParse(state, stateEnum)
                    Me.Destination = New DevExpress.DevAV.Address With {.City = city, .Line = streetLine, .State = stateEnum, .ZipCode = zipcode, .Latitude = Me.DestinationLocation.Latitude, .Longitude = Me.DestinationLocation.Longitude}
                    Me.MapPushpinsService.Clear()
                    Me.CalculateRouteDriving()
                End If
            End Sub
        End Sub

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If Me.linksViewModelField Is Nothing Then Me.linksViewModelField = DevExpress.DevAV.ViewModels.LinksViewModel.Create()
                Return Me.linksViewModelField
            End Get
        End Property

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute(UseCommandManager:=False)>
        Public Sub Close()
            If Me.DocumentManagerService Is Nothing Then Return
            Dim document As DevExpress.Mvvm.IDocument = Me.DocumentManagerService.FindDocument(Me)
            If document IsNot Nothing Then document.Close()
        End Sub

        Private Sub OnClose(ByVal e As System.ComponentModel.CancelEventArgs) Implements Global.DevExpress.Mvvm.IDocumentContent.OnClose
        End Sub

        Private Sub OnDestroy() Implements Global.DevExpress.Mvvm.IDocumentContent.OnDestroy
        End Sub

        Private Property DocumentOwner As IDocumentOwner Implements Global.DevExpress.Mvvm.IDocumentContent.DocumentOwner

        Private ReadOnly Property Title As Object Implements Global.DevExpress.Mvvm.IDocumentContent.Title
            Get
                Return "DevAV - " & Me.DestinationAddress
            End Get
        End Property
    End Class

    Public Class RoutePoint

        Public ReadOnly Property ManeuverInstruction As String

        Public ReadOnly Property Distance As String

        Public ReadOnly Property Maneuver As AzureRouteGuidanceManeuver

        Public ReadOnly Property Location As GeoPoint

        Public Sub New(ByVal instruction As DevExpress.Xpf.Map.AzureRouteInstruction)
            Me.ManeuverInstruction = instruction.Message
            Const metersInMile As Double = 1609.344
            Dim distanceMiles As Double = instruction.RouteOffsetMeters / metersInMile
            Me.Distance = If((distanceMiles > 0.9), System.[String].Format("{0:0} mi", System.Math.Ceiling(distanceMiles)), System.[String].Format("{0:0} ft", System.Math.Ceiling(distanceMiles * 52.8) * 100))
            Me.Maneuver = instruction.Maneuver
            Me.Location = instruction.Point
        End Sub
    End Class
End Namespace
