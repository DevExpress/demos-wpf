Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class AzureMapsProvider
        Inherits MapDemo.MapDemoModule

        Private demoValuesProvider As MapDemo.DemoValuesProvider

        Private additionalTilesetLayer As DevExpress.Xpf.Map.ImageLayer

        Private weatherTilesetLayer As DevExpress.Xpf.Map.ImageLayer

        Public Sub New()
            Me.InitializeComponent()
            Me.demoValuesProvider = CType(Me.Resources("DemoValuesProvider"), MapDemo.DemoValuesProvider)
            Me.UpdateAdditionalTilesetAppearance()
        End Sub

        Private Function InitMapDataProvider(ByVal tileset As DevExpress.Xpf.Map.AzureTileset) As AzureMapDataProvider
            Return New DevExpress.Xpf.Map.AzureMapDataProvider() With {.Tileset = tileset, .AzureKey = Me.demoValuesProvider.DevExpressAzureKey}
        End Function

        Private Sub RemoveImageLayer(ByVal layer As DevExpress.Xpf.Map.ImageLayer)
            If layer Is Nothing Then Return
            Me.mapControl.Layers.Remove(layer)
        End Sub

        Private Sub UpdateAdditionalTilesetAppearance()
            If Me.lbAdditionalLayers.EditValue Is Nothing Then
                Me.RemoveImageLayer(Me.additionalTilesetLayer)
                Me.additionalTilesetLayer = Nothing
            Else
                If Me.additionalTilesetLayer Is Nothing Then
                    Dim idx As Integer = If(Not Me.chkShowTerrain.IsChecked.Value, 1, 2)
                    Me.additionalTilesetLayer = New DevExpress.Xpf.Map.ImageLayer()
                    Me.mapControl.Layers.Insert(idx, Me.additionalTilesetLayer)
                End If

                If Me.lbAdditionalLayers.EditValue.Equals("Hybrid Road") Then
                    If Me.lbAppearance.EditValue.Equals("Light") Then
                        Me.additionalTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.BaseHybridRoad)
                    Else
                        Me.additionalTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.BaseHybridDarkgrey)
                    End If
                ElseIf Me.lbAdditionalLayers.EditValue.Equals("Labels") Then
                    If Me.lbAppearance.EditValue.Equals("Light") Then
                        Me.additionalTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.BaseLabelsRoad)
                    Else
                        Me.additionalTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.BaseLabelsDarkgrey)
                    End If
                End If
            End If
        End Sub

        Private Sub UpdateRoadTilesetAppearance()
            Me.RemoveImageLayer(Me.additionalTilesetLayer)
            Me.additionalTilesetLayer = Nothing
            If Me.lbAppearance.EditValue.Equals("Light") Then
                Me.baseTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.BaseRoad)
            Else
                Me.baseTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.BaseDarkGrey)
            End If
        End Sub

        Private Sub lbMapStyle_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Me.lbMapStyle.EditValue.Equals("Satellite") Then
                Me.baseTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.Imagery)
                Me.UpdateAdditionalTilesetAppearance()
            Else
                Me.UpdateRoadTilesetAppearance()
            End If
        End Sub

        Private Sub chkShowTerrain_EditValueChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            If Me.chkShowTerrain.IsChecked.Value Then
                Dim imageLayer As DevExpress.Xpf.Map.ImageLayer = New DevExpress.Xpf.Map.ImageLayer() With {.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.TerraMain)}
                Me.mapControl.Layers.Insert(1, imageLayer)
            Else
                Me.mapControl.Layers.RemoveAt(1)
            End If
        End Sub

        Private Sub lbAppearance_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Me.lbMapStyle.EditValue.Equals("Satellite") Then
                Me.UpdateAdditionalTilesetAppearance()
            Else
                Me.UpdateRoadTilesetAppearance()
            End If
        End Sub

        Private Sub lbAdditionalLayers_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            Me.UpdateAdditionalTilesetAppearance()
        End Sub

        Private Sub lbWeather_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Me.lbWeather.EditValue Is Nothing Then
                Me.RemoveImageLayer(Me.weatherTilesetLayer)
                Me.weatherTilesetLayer = Nothing
            Else
                If Me.weatherTilesetLayer Is Nothing Then
                    Dim idx As Integer = If(Not Me.chkShowTerrain.IsChecked.Value, 1, 2)
                    If Me.additionalTilesetLayer IsNot Nothing Then idx += 1
                    Me.weatherTilesetLayer = New DevExpress.Xpf.Map.ImageLayer()
                    Me.mapControl.Layers.Insert(idx, Me.weatherTilesetLayer)
                End If

                If Me.lbWeather.EditValue.Equals("Radar") Then
                    Me.weatherTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.WeatherRadarMain)
                Else
                    Me.weatherTilesetLayer.DataProvider = Me.InitMapDataProvider(DevExpress.Xpf.Map.AzureTileset.WeatherInfraredMain)
                End If
            End If
        End Sub
    End Class
End Namespace
