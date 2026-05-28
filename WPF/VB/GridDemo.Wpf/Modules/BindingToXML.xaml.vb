Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Xml
Imports DevExpress.Utils

Namespace GridDemo

    Public Partial Class BindingToXML
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            Dim document As XmlDocument = SafeXml.CreateDocument(EmployeesWithPhotoData.GetDataStream())
            grid.ItemsSource = document.SelectNodes("/Employees/Employee")
        End Sub
    End Class
End Namespace
