using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class AzureMapsProvider : MapDemoModule {
        DemoValuesProvider demoValuesProvider;
        ImageLayer additionalTilesetLayer;
        ImageLayer weatherTilesetLayer;

        public AzureMapsProvider() {
            InitializeComponent();
            demoValuesProvider = (DemoValuesProvider)Resources["DemoValuesProvider"];
            UpdateAdditionalTilesetAppearance();
        }
        AzureMapDataProvider InitMapDataProvider(AzureTileset tileset) {
            return new AzureMapDataProvider() {
                Tileset = tileset,
                AzureKey = demoValuesProvider.DevExpressAzureKey,
            };
        }
        void RemoveImageLayer(ImageLayer layer) {
            if(layer == null)
                return;
            mapControl.Layers.Remove(layer);
        }
        void UpdateAdditionalTilesetAppearance() {
            if(lbAdditionalLayers.EditValue == null) {
                RemoveImageLayer(additionalTilesetLayer);
                additionalTilesetLayer = null;
            }
            else {
                if(additionalTilesetLayer == null) {
                    int idx = !chkShowTerrain.IsChecked.Value ? 1 : 2;
                    additionalTilesetLayer = new ImageLayer();
                    mapControl.Layers.Insert(idx, additionalTilesetLayer);
                }
                if(lbAdditionalLayers.EditValue.Equals("Hybrid Road")) {
                    if(lbAppearance.EditValue.Equals("Light"))
                        additionalTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.BaseHybridRoad);
                    else
                        additionalTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.BaseHybridDarkgrey);
                }
                else if(lbAdditionalLayers.EditValue.Equals("Labels")) {
                    if(lbAppearance.EditValue.Equals("Light"))
                        additionalTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.BaseLabelsRoad);
                    else
                        additionalTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.BaseLabelsDarkgrey);
                }
            }
        }
        void UpdateRoadTilesetAppearance() {
            RemoveImageLayer(additionalTilesetLayer);
            additionalTilesetLayer = null;
            if(lbAppearance.EditValue.Equals("Light"))
                baseTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.BaseRoad);
            else
                baseTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.BaseDarkGrey);
        }
        void lbMapStyle_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(lbMapStyle.EditValue.Equals("Satellite")) {
                baseTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.Imagery);
                UpdateAdditionalTilesetAppearance();
            }
            else
                UpdateRoadTilesetAppearance();
        }
        void chkShowTerrain_EditValueChanged(object sender, System.Windows.RoutedEventArgs e) {
            if(chkShowTerrain.IsChecked.Value) {
                ImageLayer imageLayer = new ImageLayer() {
                    DataProvider = InitMapDataProvider(AzureTileset.TerraMain),
                };
                mapControl.Layers.Insert(1, imageLayer);
            }
            else
                mapControl.Layers.RemoveAt(1);
        }
        void lbAppearance_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(lbMapStyle.EditValue.Equals("Satellite"))
                UpdateAdditionalTilesetAppearance();
            else
                UpdateRoadTilesetAppearance();
        }
        void lbAdditionalLayers_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            UpdateAdditionalTilesetAppearance();
        }
        void lbWeather_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(lbWeather.EditValue == null) {
                RemoveImageLayer(weatherTilesetLayer);
                weatherTilesetLayer = null;
            }
            else {
                if(weatherTilesetLayer == null) {
                    int idx = !chkShowTerrain.IsChecked.Value ? 1 : 2;
                    if(additionalTilesetLayer != null)
                        idx += 1;
                    weatherTilesetLayer = new ImageLayer();
                    mapControl.Layers.Insert(idx, weatherTilesetLayer);
                }
                if(lbWeather.EditValue.Equals("Radar"))
                    weatherTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.WeatherRadarMain);
                else
                    weatherTilesetLayer.DataProvider = InitMapDataProvider(AzureTileset.WeatherInfraredMain);
            }
        }
    }
}
