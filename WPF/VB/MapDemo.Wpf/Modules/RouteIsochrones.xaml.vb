Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class RouteIsochrones
        Inherits MapDemoModule

        Private model As RouteIsochronesModel

        Public Sub New()
            InitializeComponent()
            Dim center As GeoPoint = CType(mapControl.CenterPoint, GeoPoint)
            model = ViewModelSource.Create(Function() New RouteIsochronesModel(routeIsochroneDataProvider, center))
            DataContext = model
            geocodeDataProvider.RequestLocationInformation(center)
        End Sub

        Private Sub GeoCodeItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            model.OnGeoCodeItemsGenerating(args.Items)
        End Sub

        Private Sub LayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            geocodeInformationLayer.ClearResults()
            model.OnIsochroneGenerating(args.Items)
        End Sub
    End Class

    <POCOViewModel>
    Public Class RouteIsochronesModel
        Inherits ViewModelBase

        Private ReadOnly provider As AzureRouteIsochroneDataProvider

        Private parameterIndexField As Integer = 0

        Private parameterValueField As Integer = 15

        Private currentPushpin As MapPushpin

        Private origin As GeoPoint = New GeoPoint(42.3589935302734, -71.0586318969727)

        Public Property ParameterIndex As Integer
            Get
                Return parameterIndexField
            End Get

            Set(ByVal value As Integer)
                If parameterIndexField <> value Then
                    parameterIndexField = value
                    If currentPushpin IsNot Nothing Then currentPushpin.State = MapPushpinState.Busy
                    UpdateAndCalculateIsochrone()
                End If
            End Set
        End Property

        Public Property ParameterValue As Double
            Get
                Return parameterValueField
            End Get

            Set(ByVal value As Double)
                If parameterValueField <> CInt(value) Then
                    parameterValueField = CInt(value)
                    If currentPushpin IsNot Nothing Then currentPushpin.State = MapPushpinState.Busy
                    UpdateAndCalculateIsochrone()
                End If
            End Set
        End Property

        Public Sub New(ByVal provider As AzureRouteIsochroneDataProvider, ByVal origin As GeoPoint)
            Me.provider = provider
            Me.origin = origin
        End Sub

        Private Sub UpdateAndCalculateIsochrone()
            Dim options As AzureRouteIsochroneOptions = New AzureRouteIsochroneOptions()
            If parameterIndexField = 0 Then
                Dim timeSeconds As Double = parameterValueField * 60
                provider.CalculateIsochroneByTime(New RouteWaypoint("", origin), timeSeconds, options)
            Else
                Const metersInMile As Double = 1609.344
                Dim distanceMeters As Double = If(parameterIndexField = 1, parameterValueField * 1000, parameterValueField * metersInMile)
                provider.CalculateIsochroneByDistance(New RouteWaypoint("", origin), distanceMeters, options)
            End If
        End Sub

        Public Sub OnGeoCodeItemsGenerating(ByVal items As MapItem())
            Dim pushpin As MapPushpin = TryCast(items.FirstOrDefault(Function(x) TypeOf x Is MapPushpin), MapPushpin)
            If pushpin IsNot Nothing Then
                origin = CType(pushpin.Location, GeoPoint)
                pushpin.State = MapPushpinState.Busy
                UpdateAndCalculateIsochrone()
            End If
        End Sub

        Public Sub OnIsochroneGenerating(ByVal items As MapItem())
            currentPushpin = TryCast(items.FirstOrDefault(Function(x) TypeOf x Is MapPushpin), MapPushpin)
        End Sub
    End Class
End Namespace
