using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Office.Services;
using DevExpress.Xpf.RichEdit;

namespace RichEditDemo {
    public partial class MailMerge : RichEditDemoModule {
        IList<Employee> employees;
        IList<Employee> Employees {
            get {
                if (employees == null)
                    employees = DevExpress.DemoData.NWindDataProvider.Employees;
                return employees;
            }
        }
        public MailMerge() {
            InitializeComponent();

            MainWindow.CommonRibbon.Init(richEdit);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_Review = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_MailMerge;

            IUriStreamService uriService = this.richEdit.GetService<IUriStreamService>();
            uriService.RegisterProvider(new DBUriStreamProvider(Employees));

            var dataSource = CreateDataSource();
            this.gridControl1.ItemsSource = dataSource;
            this.richEdit.MailMergeOptions.DataSource = dataSource;
            this.gridControl1.View.FocusedRowHandleChanged += View_FocusedRowChanged;
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            RichEditControl.MailMergeOptions.ActiveRecord = this.gridControl1.GetListIndexByRowHandle(-1);
        }
        IEnumerable<object> CreateDataSource() {
            return (from e in Employees
                    from c in e.Customers
                    select new {
                        EmployeeID = e.EmployeeID,
                        LastName = e.LastName,
                        FirstName = e.FirstName,
                        Title = e.Title,
                        TitleOfCourtesy = e.TitleOfCourtesy,
                        BirthDate = e.BirthDate,
                        HireDate = e.HireDate,
                        Employees = new {
                            Address = e.Address,
                            City = e.City,
                            Region = e.Region,
                            PostalCode = e.PostalCode,
                            Country = e.Country,
                        },
                        HomePhone = e.HomePhone,
                        Extension = e.Extension,
                        Photo = e.Photo,
                        Notes = e.Notes,
                        ReportsTo = e.ReportsTo,
                        Email = e.Email,
                        CustomerID = c.CustomerID,
                        CompanyName = c.CompanyName,
                        ContactName = c.ContactName,
                        ContactTitle = c.ContactTitle,
                        Customers = new {
                            Address = c.Address,
                            City = c.City,
                            Region = c.Region,
                            PostalCode = c.PostalCode,
                            Country = c.Country,
                        },
                        Phone = c.Phone,
                        Fax = c.Fax
                    }).ToList();
        }
        void View_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowHandleChangedEventArgs e) {
            RichEditControl.MailMergeOptions.ActiveRecord = this.gridControl1.GetListIndexByRowHandle(e.RowData.RowHandle.Value);
        }
    }
}
