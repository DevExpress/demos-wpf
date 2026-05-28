Namespace DevExpress.DevAV.Properties

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.14.0.0")>
    Friend NotInheritable Partial Class Settings
        Inherits Global.System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As DevExpress.DevAV.Properties.Settings = CType((Global.System.Configuration.ApplicationSettingsBase.Synchronized(New DevExpress.DevAV.Properties.Settings())), DevExpress.DevAV.Properties.Settings)

        Public Shared ReadOnly Property [Default] As Settings
            Get
                Return DevExpress.DevAV.Properties.Settings.defaultInstance
            End Get
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <FilterInfo>
    <Name>All</Name>
    <FilterCriteria />
    <ImageUri />
  </FilterInfo>
</ArrayOfFilterInfo>")>
        Public Property TasksCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("TasksCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <FilterInfo>
              <Name>On probation </Name>
              <FilterCriteria>Not IsNull([ProbationReason])</FilterCriteria>
              <ImageUri>Resources/Accordion/OnProbation.svg</ImageUri>
            </FilterInfo>
          </ArrayOfFilterInfo>
        ")>
        Public Property EmployeesCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("EmployeesCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
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
        ")>
        Public Property CustomersCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("CustomersCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
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
        ")>
        Public Property CustomersStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("CustomersStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("CustomersStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
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
        ")>
        Public Property ProductsCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("ProductsCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute(Global.Microsoft.VisualBasic.Constants.vbCrLf & "          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instan" & "ce"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "   " & "           <Name>All</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/All.svg" & "</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              " & "<Name>Video Players</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/VideoPla" & "yer.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[Category] = ##Enum#DevExpress" & ".DevAV.ProductCategory,VideoPlayers#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Automation</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "             " & " <ImageUri>Resources/Accordion/Automation.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterC" & "riteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategory,Automation#</Filter" & "Criteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <N" & "ame>Monitors</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/Monitor.svg</Im" & "ageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.Prod" & "uctCategory,Monitors#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <" & "FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Projectors</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Reso" & "urces/Accordion/Projector.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[Categor" & "y] = ##Enum#DevExpress.DevAV.ProductCategory,Projectors#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "     " & "       </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Televisions<" & "/Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/Television.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & " " & "             <FilterCriteria>[Category] = ##Enum#DevExpress.DevAV.ProductCategor" & "y,Televisions#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "          </ArrayOfF" & "ilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "        ")>
        Public Property ProductsStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("ProductsStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("ProductsStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
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
        ")>
        Public Property OrdersCustomFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("OrdersCustomFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("OrdersCustomFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute(Global.Microsoft.VisualBasic.Constants.vbCrLf & "          <ArrayOfFilterInfo xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instan" & "ce"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "   " & "           <Name>All</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/All.svg" & "</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              " & "<Name>Today</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/Today.svg</Image" & "Uri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>IsOutlookIntervalToday([OrderDate])</FilterC" & "riteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Na" & "me>Yesterday</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/Yesterday.svg</" & "ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>IsOutlookIntervalYesterday([OrderDate])" & "</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "        " & "      <Name>This Month</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/ThisM" & "onth.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>IsOutlookIntervalEarlierThisM" & "onth([OrderDate])</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <Filt" & "erInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>This Year</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources" & "/Accordion/ThisYear.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>IsOutlookInter" & "valEarlierThisYear([OrderDate])</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "   " & "         <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Unpaid Orders</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <" & "ImageUri>Resources/Accordion/Unpaid.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteri" & "a>[PaymentTotal] = 0.0m</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "           " & " <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Refunds</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resou" & "rces/Accordion/Refund.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[PaymentTota" & "l] &gt; 0.0m AND [RefundTotal]  = [PaymentTotal]</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <" & "/FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "          </ArrayOfFilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "        ")>
        Public Property OrdersStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("OrdersStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("OrdersStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
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
        ")>
        Public Property QuotesStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("QuotesStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("QuotesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute(Global.Microsoft.VisualBasic.Constants.vbCrLf & "          <ArrayOfFilterInfo xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns" & ":xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "   " & "           <Name>All</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/All.svg" & "</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              " & "<Name>Salaried</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[Status] = ##Enum#DevExpres" & "s.DevAV.EmployeeStatus,Salaried#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resou" & "rces/Accordion/Salaried.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <" & "FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Commission</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteri" & "a>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,Commission#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/Commission.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "       " & "     </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>Contract</Name" & ">" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStat" & "us,Contract#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/Contr" & "act.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "       " & "       <Name>Terminated</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <FilterCriteria>[Status] = ##Enum#" & "DevExpress.DevAV.EmployeeStatus,Terminated#</FilterCriteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Ima" & "geUri>Resources/Accordion/Terminated.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "            <FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Name>On Leave</Name>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <Fi" & "lterCriteria>[Status] = ##Enum#DevExpress.DevAV.EmployeeStatus,OnLeave#</FilterC" & "riteria>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "              <ImageUri>Resources/Accordion/OnLeave.svg</ImageUri>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "  " & "          </FilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "          </ArrayOfFilterInfo>" & Global.Microsoft.VisualBasic.Constants.vbCrLf & "        ")>
        Public Property EmployeesStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("EmployeesStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("EmployeesStaticFilters") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute()>
        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.Configuration.DefaultSettingValueAttribute("
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
        ")>
        Public Property TasksStaticFilters As Global.DevExpress.DevAV.ViewModels.FilterInfoList
            Get
                Return CType((Me("TasksStaticFilters")), Global.DevExpress.DevAV.ViewModels.FilterInfoList)
            End Get

            Set(ByVal value As Global.DevExpress.DevAV.ViewModels.FilterInfoList)
                Me("TasksStaticFilters") = value
            End Set
        End Property
    End Class
End Namespace
