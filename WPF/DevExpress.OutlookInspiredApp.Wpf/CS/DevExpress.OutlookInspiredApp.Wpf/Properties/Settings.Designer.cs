









namespace DevExpress.DevAV.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.14.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
    <FilterCriteria />
    <ImageUri />
  </FilterInfo>
</ArrayOfFilterInfo>")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList TasksCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["TasksCustomFilters"]));
            }
            set {
                this["TasksCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>On probation </Name>
              <FilterCriteria>Not IsNull([ProbationReason])</FilterCriteria>
              <ImageUri>Resources/Accordion/OnProbation.svg</ImageUri>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList EmployeesCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["EmployeesCustomFilters"]));
            }
            set {
                this["EmployeesCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>Active</Name>
              <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.CustomerStatus,Active#</FilterCriteria>
              <ImageUri>Resources/Accordion/Active.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Suspended</Name>
              <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.CustomerStatus,Suspended#</FilterCriteria>
              <ImageUri>Resources/Accordion/Suspended.svg</ImageUri>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList CustomersCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["CustomersCustomFilters"]));
            }
            set {
                this["CustomersCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>All</Name>
              <ImageUri>Resources/Accordion/All.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Stores &gt; 10 Locations</Name>
              <FilterCriteria>[TotalStores] &gt; 10</FilterCriteria>
              <ImageUri>Resources/Accordion/Store.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Revenues &gt; 100 Billion</Name>
              <FilterCriteria>[AnnualRevenue] &gt; 100000000000.0m</FilterCriteria>
              <ImageUri>Resources/Accordion/Revenues.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Employees &gt; 10000</Name>
              <FilterCriteria>[TotalEmployees] &gt; 10000</FilterCriteria>
              <ImageUri>Resources/Accordion/Employees.svg</ImageUri>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList CustomersStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["CustomersStaticFilters"]));
            }
            set {
                this["CustomersStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>Discontinued</Name>
              <ImageUri>Resources/Accordion/Discontinued.svg</ImageUri>
              <FilterCriteria>[Available] = False</FilterCriteria>
            </FilterInfo>
            <FilterInfo>
              <Name>Out of Stock</Name>
              <ImageUri>Resources/Accordion/OutOfStock.svg</ImageUri>
              <FilterCriteria>[CurrentInventory] = 0</FilterCriteria>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList ProductsCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["ProductsCustomFilters"]));
            }
            set {
                this["ProductsCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\r\n          <ArrayOfFilterInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instan" +
            "ce\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n            <FilterInfo>\r\n   " +
            "           <Name>All</Name>\r\n              <ImageUri>Resources/Accordion/All.svg" +
            "</ImageUri>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n              " +
            "<Name>Video Players</Name>\r\n              <ImageUri>Resources/Accordion/VideoPla" +
            "yer.svg</ImageUri>\r\n              <FilterCriteria>[Category] = ##Enum#DevExpress" +
            ".DevAV.ProductCategory,VideoPlayers#</FilterCriteria>\r\n            </FilterInfo>" +
            "\r\n            <FilterInfo>\r\n              <Name>Automation</Name>\r\n             " +
            " <ImageUri>Resources/Accordion/Automation.svg</ImageUri>\r\n              <FilterC" +
            "riteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Automation#</Filter" +
            "Criteria>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n              <N" +
            "ame>Monitors</Name>\r\n              <ImageUri>Resources/Accordion/Monitor.svg</Im" +
            "ageUri>\r\n              <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.Prod" +
            "uctCategory,Monitors#</FilterCriteria>\r\n            </FilterInfo>\r\n            <" +
            "FilterInfo>\r\n              <Name>Projectors</Name>\r\n              <ImageUri>Reso" +
            "urces/Accordion/Projector.svg</ImageUri>\r\n              <FilterCriteria>[Categor" +
            "y] = ##Enum#DevExpress.DevAV.ProductCategory,Projectors#</FilterCriteria>\r\n     " +
            "       </FilterInfo>\r\n            <FilterInfo>\r\n              <Name>Televisions<" +
            "/Name>\r\n              <ImageUri>Resources/Accordion/Television.svg</ImageUri>\r\n " +
            "             <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategor" +
            "y,Televisions#</FilterCriteria>\r\n            </FilterInfo>\r\n          </ArrayOfF" +
            "ilterInfo>\r\n        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList ProductsStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["ProductsStaticFilters"]));
            }
            set {
                this["ProductsStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>Sales &gt; $5000</Name>
              <FilterCriteria>[TotalAmount] &gt; 5000.0m</FilterCriteria>
              <ImageUri>Resources/Accordion/SalesAsc.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Sales &lt; $5000</Name>
              <FilterCriteria>[TotalAmount] &lt; 5000.0m</FilterCriteria>
              <ImageUri>Resources/Accordion/SalesDesc.svg</ImageUri>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList OrdersCustomFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["OrdersCustomFilters"]));
            }
            set {
                this["OrdersCustomFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\r\n          <ArrayOfFilterInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instan" +
            "ce\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n            <FilterInfo>\r\n   " +
            "           <Name>All</Name>\r\n              <ImageUri>Resources/Accordion/All.svg" +
            "</ImageUri>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n              " +
            "<Name>Today</Name>\r\n              <ImageUri>Resources/Accordion/Today.svg</Image" +
            "Uri>\r\n              <FilterCriteria>IsOutlookIntervalToday([OrderDate])</FilterC" +
            "riteria>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n              <Na" +
            "me>Yesterday</Name>\r\n              <ImageUri>Resources/Accordion/Yesterday.svg</" +
            "ImageUri>\r\n              <FilterCriteria>IsOutlookIntervalYesterday([OrderDate])" +
            "</FilterCriteria>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n        " +
            "      <Name>This Month</Name>\r\n              <ImageUri>Resources/Accordion/ThisM" +
            "onth.svg</ImageUri>\r\n              <FilterCriteria>IsOutlookIntervalEarlierThisM" +
            "onth([OrderDate])</FilterCriteria>\r\n            </FilterInfo>\r\n            <Filt" +
            "erInfo>\r\n              <Name>This Year</Name>\r\n              <ImageUri>Resources" +
            "/Accordion/ThisYear.svg</ImageUri>\r\n              <FilterCriteria>IsOutlookInter" +
            "valEarlierThisYear([OrderDate])</FilterCriteria>\r\n            </FilterInfo>\r\n   " +
            "         <FilterInfo>\r\n              <Name>Unpaid Orders</Name>\r\n              <" +
            "ImageUri>Resources/Accordion/Unpaid.svg</ImageUri>\r\n              <FilterCriteri" +
            "a>[PaymentTotal] = 0.0m</FilterCriteria>\r\n            </FilterInfo>\r\n           " +
            " <FilterInfo>\r\n              <Name>Refunds</Name>\r\n              <ImageUri>Resou" +
            "rces/Accordion/Refund.svg</ImageUri>\r\n              <FilterCriteria>[PaymentTota" +
            "l] &gt; 0.0m AND [RefundTotal]  = [PaymentTotal]</FilterCriteria>\r\n            <" +
            "/FilterInfo>\r\n          </ArrayOfFilterInfo>\r\n        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList OrdersStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["OrdersStaticFilters"]));
            }
            set {
                this["OrdersStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>All</Name>
              <ImageUri>Resources/Accordion/All.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Last Year</Name>
              <ImageUri>Resources/Accordion/LastYear.svg</ImageUri>
              <FilterCriteria>GetYear([Date])=(GetYear(LocalDateTimeNow())-1)</FilterCriteria>
            </FilterInfo>
            <FilterInfo>
              <Name>This Year</Name>
              <ImageUri>Resources/Accordion/ThisYear.svg</ImageUri>
              <FilterCriteria>IsOutlookIntervalEarlierThisYear([Date])</FilterCriteria>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList QuotesStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["QuotesStaticFilters"]));
            }
            set {
                this["QuotesStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\r\n          <ArrayOfFilterInfo xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns" +
            ":xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n            <FilterInfo>\r\n   " +
            "           <Name>All</Name>\r\n              <ImageUri>Resources/Accordion/All.svg" +
            "</ImageUri>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n              " +
            "<Name>Salaried</Name>\r\n              <FilterCriteria>[Status] = ##Enum#DevExpres" +
            "s.DevAV.EmployeeStatus,Salaried#</FilterCriteria>\r\n              <ImageUri>Resou" +
            "rces/Accordion/Salaried.svg</ImageUri>\r\n            </FilterInfo>\r\n            <" +
            "FilterInfo>\r\n              <Name>Commission</Name>\r\n              <FilterCriteri" +
            "a>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Commission#</FilterCriteria>" +
            "\r\n              <ImageUri>Resources/Accordion/Commission.svg</ImageUri>\r\n       " +
            "     </FilterInfo>\r\n            <FilterInfo>\r\n              <Name>Contract</Name" +
            ">\r\n              <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStat" +
            "us,Contract#</FilterCriteria>\r\n              <ImageUri>Resources/Accordion/Contr" +
            "act.svg</ImageUri>\r\n            </FilterInfo>\r\n            <FilterInfo>\r\n       " +
            "       <Name>Terminated</Name>\r\n              <FilterCriteria>[Status] = ##Enum#" +
            "DevExpress.DevAV.EmployeeStatus,Terminated#</FilterCriteria>\r\n              <Ima" +
            "geUri>Resources/Accordion/Terminated.svg</ImageUri>\r\n            </FilterInfo>\r\n" +
            "            <FilterInfo>\r\n              <Name>On Leave</Name>\r\n              <Fi" +
            "lterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,OnLeave#</FilterC" +
            "riteria>\r\n              <ImageUri>Resources/Accordion/OnLeave.svg</ImageUri>\r\n  " +
            "          </FilterInfo>\r\n          </ArrayOfFilterInfo>\r\n        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList EmployeesStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["EmployeesStaticFilters"]));
            }
            set {
                this["EmployeesStaticFilters"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>All</Name>
              <FilterCriteria />
              <ImageUri>Resources/Accordion/All.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>Overdue Tasks</Name>
              <ImageUri>Resources/Accordion/OverdueTask.svg</ImageUri>
              <FilterCriteria>(Not IsNull([DueDate]))AND([DueDate] &lt; LocalDateTimeNow())AND(Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#))</FilterCriteria>
              <ImageUri />
            </FilterInfo>
            <FilterInfo>
              <Name>Incomplete Tasks</Name>
              <FilterCriteria>Not([Status] = ##Enum#DevExpress.DevAV.EmployeeTaskStatus,Completed#)</FilterCriteria>
              <ImageUri>Resources/Accordion/IncompleteTask.svg</ImageUri>
            </FilterInfo>
            <FilterInfo>
              <Name>High Priority Tasks</Name>
              <ImageUri>Resources/Accordion/HighPriorityTask.svg</ImageUri>
              <FilterCriteria>[Priority] = ##Enum#DevExpress.DevAV.EmployeeTaskPriority,High#</FilterCriteria>
              <ImageUri />
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")]
        public global::DevExpress.DevAV.ViewModels.FilterInfoList TasksStaticFilters {
            get {
                return ((global::DevExpress.DevAV.ViewModels.FilterInfoList)(this["TasksStaticFilters"]));
            }
            set {
                this["TasksStaticFilters"] = value;
            }
        }
    }
}
