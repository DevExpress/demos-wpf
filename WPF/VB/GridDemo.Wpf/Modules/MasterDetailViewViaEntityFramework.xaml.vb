Imports DevExpress.DemoData.Models
#If NET
Imports Microsoft.EntityFrameworkCore

#Else
using System.Data.Entity;
#End If
Namespace GridDemo

    Public Partial Class MasterDetailViewViaEntityFramework
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsMasterDetailViewViaEntityFramework
            Dim customers = New NWindContext().Customers
            customers.Load()
            grid.ItemsSource = customers.Local
        End Sub
    End Class
End Namespace
