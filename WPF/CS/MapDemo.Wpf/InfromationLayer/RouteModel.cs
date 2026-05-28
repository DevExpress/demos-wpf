using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Effects;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public enum RouteModelState {
        Normal,
        Drive
    }

    [POCOViewModel]
    public class RouteModel : ViewModelBase {
        ObservableCollection<MapItem> helpers;
        List<RouteWaypoint> waypoints;
        List<GeoPoint> routePath;
        int waypointIndex;
        int searchPushpinsCount;

        public ObservableCollection<MapItem> Helpers => helpers;
        public List<GeoPoint> RoutePath => routePath;
        public virtual bool IsCalculating { get; set; }
        public virtual RouteModelState State { get; set; }
        public virtual DriveModel DriveModel { get; set; }
        public virtual DirectionsNavigation Navigator { get; set; }
        public virtual double DrivePathDistance { get; set; }
        public virtual List<RouteInstruction> RouteInstructions { get; set; }
        public virtual List<MapPushpin> RoutePushpins { get; set; }
        public string ActionText {
            get {
                if(State == RouteModelState.Drive && DriveModel != null) {
                    return DriveModel.ActionText;
                }
                else {
                    if(waypoints.Count == 0) {
                        if(searchPushpinsCount > 0)
                            return "Click the  pushpin to set a start point.";
                        else
                            return "Click the map or use Search to find a location.";
                    }
                    else if(waypoints.Count == 1) {
                        return "Set a finish point to calculate a route.";
                    }
                    else {
                        return "Set another finish point or click Drive.";
                    }
                }
            }
        }
        public virtual CoordPoint MapCenter { get; set; }
        public virtual double MapAngle { get; set; }
        public virtual bool InMotion { get; set; }
        public virtual bool EnableRotation { get; set; }
        public List<RouteWaypoint> Waypoints {
            get { return waypoints; }
        }

        public RouteModel(DirectionsNavigation navigator) {
            MapCenter = new GeoPoint(34.158506, -118.255629);
            Navigator = navigator;
            helpers = new ObservableCollection<MapItem>();
            waypoints = new List<RouteWaypoint>();
            routePath = new List<GeoPoint>();
            RoutePushpins = new List<MapPushpin>();
            RouteInstructions = new List<RouteInstruction>();
            EnableRotation = true;
        }

        void SendRouteRequest() {
            IsCalculating = true;
            if(Waypoints.Count > 1) {
                AzureRouteOptions options = new AzureRouteOptions() {
                    InstructionsType = AzureRouteInstructionsType.Text,
                };
                Navigator.RouteProvider.CalculateRoute(Waypoints, options);
            }
        }
        string NextWaypointLetter() {
            string letter = "" + (char)((byte)'A' + waypointIndex % 26);
            waypointIndex++;
            return letter;
        }
        void ExtractRouteInstructions(AzureRouteResult result) {
            RouteInstructions.Clear();
            foreach(AzureRouteInstruction instruction in result.Guidance.Instructions)
                RouteInstructions.Add(new RouteInstruction(instruction.Point, instruction.Message));
        }
        void BeginDrive() {
            if(RoutePath.Count > 1) {
                StopDrive();
                DriveModel = new DriveModel(this);
                DriveModel.CurrentLocationChanged += OnCurrentLocationChanged;
                InMotion = true;
            }
        }
        void OnCurrentLocationChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            MapCenter = DriveModel.CurrentLocation;
            MapAngle = EnableRotation ? DriveModel.CurrentDirection + 90.0 : 0;
        }

        void StopDrive() {
            if(DriveModel != null) {
                DriveModel.Cleanup();
                DriveModel = null;
                MapCenter = (GeoPoint)MapCenter;
                MapAngle = 0;
            }
            InMotion = false;
        }
        protected void OnStateChanged() {
            switch(State) {
                case RouteModelState.Drive:
                    BeginDrive();
                    break;
                case RouteModelState.Normal:
                    StopDrive();
                    break;
            }
            RaisePropertyChanged("ActionText");
        }
        void CalculatePathDistance() {
            DrivePathDistance = 0;
            MapUnit? lastPoint = null;
            foreach(GeoPoint node in RoutePath) {
                if(lastPoint != null) {
                    MapUnit currentPoint = Navigator.RouteLayer.GeoPointToMapUnit(node);
                    DrivePathDistance += DriveModel.DistanceBetweenPoints(currentPoint, lastPoint.Value);
                    lastPoint = currentPoint;
                }
                else
                    lastPoint = Navigator.RouteLayer.GeoPointToMapUnit(node);
            }
        }

        internal void RaiseDriveModelChanged() {
            RaisePropertyChanged("ActionText");
        }

        internal void RaiseSearchPushpinsChanged(int count) {
            searchPushpinsCount = count;
            RaisePropertyChanged("ActionText");
        }

        public void AddWaypoint(string description, GeoPoint location) {
            RouteWaypoint waypoint = new RouteWaypoint(description, location);
            if(!waypoints.Contains(waypoint)) {
                MapPushpin pushpin = new MapPushpin();
                pushpin.Location = location;
                pushpin.Information = description;
                pushpin.Text = NextWaypointLetter();
                pushpin.TraceDepth = 0;
                pushpin.State = MapPushpinState.Busy;
                Helpers.Add(pushpin);
                waypoints.Add(waypoint);
                SendRouteRequest();
            }
            RaisePropertyChanged("ActionText");
            RaisePropertyChanged("Waypoints");
        }
        public void ProcessRouteItems(MapItem[] items) {
            waypointIndex = 0;
            RoutePushpins.Clear();
            foreach(MapItem item in items) {
                MapPushpin pushpin = item as MapPushpin;
                if(pushpin != null) {
                    pushpin.Text = NextWaypointLetter();
                    RoutePushpins.Add(pushpin);
                }
                MapPolyline polyline = item as MapPolyline;
                if(polyline != null)
                    polyline.Effect = new DropShadowEffect() { Direction = -90, ShadowDepth = 1 };
            }
            Helpers.Clear();
        }
        public void ProcessRouteResult(AzureRouteResult result) {
            if(result == null)
                return;
            ExtractRouteInstructions(result);
            RoutePath.Clear();
            foreach(AzureRouteLeg leg in result.Legs)
                RoutePath.AddRange(leg.Points);
            CalculatePathDistance();
        }
        public void Clear() {
            RoutePath.Clear();
            Helpers.Clear();
            RoutePushpins.Clear();
            RouteInstructions.Clear();
            DrivePathDistance = 0.0;
            waypoints.Clear();
            waypointIndex = 0;
            RaiseSearchPushpinsChanged(0);
            RaisePropertyChanged("Waypoints");
        }
        public void RotationStateChanged() {
            EnableRotation = !EnableRotation;
        }
    }

    public class RouteModelStateToButtonTextConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((value != null) && (value.GetType() == typeof(RouteModelState))) {
                RouteModelState state = (RouteModelState)value;
                switch(state) {
                    case RouteModelState.Drive:
                        return "Stop";
                    case RouteModelState.Normal:
                        return "Drive";
                }
            }
            return string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class RouteModelNormalStateToBoolConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((value != null) && (value.GetType() == typeof(RouteModelState)))
                return ((RouteModelState)value) == RouteModelState.Normal;
            return false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class WaypointsCountToDriveAbilityConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((value != null) && (value.GetType() == typeof(int)))
                return ((int)value) > 1;
            return false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class InvertDoubleConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is double ? (double)value * -1d : value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
