using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class TrafficAndIncidentsModel : ViewModelBase {
        readonly TrafficAndIncidents mainModule;
        readonly ObservableCollection<MapItem> items;
        readonly ObservableCollection<RouteWaypoint> waypoints;
        readonly DispatcherTimer trafficIncidentTimer = new DispatcherTimer();
        readonly DispatcherTimer routePropertiesTimer = new DispatcherTimer();
        MapBounds viewport;
        string requestErrorMessage;

        public ObservableCollection<MapItem> Items => items;
        public ObservableCollection<MapItem> Waypoints => items;
        public CoordPoint MapCenterPoint { get; set; }
        public string RequestErrorMessage => requestErrorMessage;
        public bool RequestErrorMessageVisible => requestErrorMessage != null;

        public TrafficAndIncidentsModel(TrafficAndIncidents module) {
            mainModule = module;
            MapCenterPoint = new GeoPoint(38.90507, -77.01909);
            items = new ObservableCollection<MapItem>();
            waypoints = new ObservableCollection<RouteWaypoint>();

            AddWaypoint("A", new GeoPoint(39.025538, -77.203833));
            AddWaypoint("B", new GeoPoint(38.960899, -76.98296));
            AddWaypoint("C", new GeoPoint(38.905435, -77.127861));
            AddWaypoint("D", new GeoPoint(38.8501856, -76.9276787));

            trafficIncidentTimer.Interval = TimeSpan.FromSeconds(2.5);
            trafficIncidentTimer.Tick += (s, e) => SendTrafficIncidentsRequest();
            routePropertiesTimer.Interval = TimeSpan.FromSeconds(0.5);
            routePropertiesTimer.Tick += (s, e) => SendRouteRequest();
        }
        void SendTrafficIncidentsRequest() {
            trafficIncidentTimer.Stop();
            mainModule.IncidentLayer.ClearResults();
            SearchBoundingBox boundingBox = new SearchBoundingBox() {
                WestLongitude = viewport.Left,
                NorthLatitude = viewport.Top,
                EastLongitude = viewport.Right,
                SouthLatitude = viewport.Bottom
            };
            AzureTrafficIncidentOptions options = new AzureTrafficIncidentOptions();
            mainModule.IncidentDataProvider.RequestTrafficIncidents(boundingBox, (int)mainModule.mapControl.ZoomLevel, -1, options);
        }

        public void AddWaypoint(string description, GeoPoint location) {
            if(waypoints.Count == 4)
                waypoints.Clear();
            char pinLetter = Convert.ToChar(Convert.ToInt32('A') + waypoints.Count);
            string name = new string(pinLetter, 1);
            waypoints.Add(new RouteWaypoint(description, location));
            Items.Add(new MapPushpin() {
                Location = location,
                Information = description,
                TraceDepth = 0,
                Text = name
            });
            SendRouteRequest();
        }
        public void ProcessRouteItems(MapItem[] items) {
            int pushpinIndex = 0;
            foreach(MapItem item in items) {
                MapPushpin pushpin = item as MapPushpin;
                if(pushpin != null) {
                    char pinLetter = Convert.ToChar(Convert.ToInt32('A') + pushpinIndex++);
                    pushpin.Text = new string(pinLetter, 1);
                }
                MapPolyline polyline = item as MapPolyline;
                if(polyline != null)
                    polyline.Effect = new DropShadowEffect() { Direction = -90, ShadowDepth = 1 };
            }
            Items.Clear();
        }
        public void UpdateViewport(MapBounds newViewport) {
            viewport = newViewport;
            trafficIncidentTimer.Stop();
            trafficIncidentTimer.Start();
        }
        public void UpdateRoute() {
            requestErrorMessage = null;
            RaisePropertyChanged(nameof(RequestErrorMessage));
            RaisePropertyChanged(nameof(RequestErrorMessageVisible));
            routePropertiesTimer.Stop();
            routePropertiesTimer.Start();
        }
        public void UpdateErrorMessage(Exception exception) {
            string message = exception.Message;
            const string startPattern = "\"message\":\"";
            int startIndex = message.IndexOf(startPattern);
            if(startIndex < 0)
                return;
            message = message.Substring(startIndex + startPattern.Length);
            message = message.Substring(0, message.IndexOf("\""));
            requestErrorMessage = message;
            RaisePropertyChanged(nameof(RequestErrorMessage));
            RaisePropertyChanged(nameof(RequestErrorMessageVisible));
        }
        public void SendRouteRequest() {
            routePropertiesTimer.Stop();
            if(waypoints.Count == 4) {
                AzureRouteOptions options = new AzureRouteOptions() {
                    TravelMode = (AzureTravelMode)mainModule.travelModeRadioGroup.EditValue,
                    AvoidTypes = (AzureRouteAvoidType)DemoUtils.ConvertListObjectToEnumFlags(mainModule.avoidTypesComboBox.EditValue),
                    SectionTypes = (AzureRouteSectionType)DemoUtils.ConvertListObjectToEnumFlags(mainModule.sectionTypesComboBox.EditValue),
                    MaxAlternatives = (int)mainModule.maxAlternativesSpinEdit.Value,
                    OptimizeWaypoints = (bool)mainModule.optimizeWaypointsCheckBox.EditValue,
                };
                mainModule.RouteDataProvider.CalculateRoute(new List<RouteWaypoint>(waypoints), options);
            }
        }
    }
}
