using DevExpress.Xpf.DemoBase.DataClasses;
using System.Xml;
using DevExpress.Utils;

namespace GridDemo {
    public partial class BindingToXML : GridDemoModule {
        public BindingToXML() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            XmlDocument document = SafeXml.CreateDocument(EmployeesWithPhotoData.GetDataStream());
            grid.ItemsSource = document.SelectNodes("/Employees/Employee");
        }
    }
}
