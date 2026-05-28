Imports DevExpress.DemoData.Models.Vehicles
Imports System.Linq

Namespace GridDemo

    Public Partial Class CardView
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            grid.ItemsSource = New VehiclesContext().Models.ToList()
        End Sub
    End Class
End Namespace
