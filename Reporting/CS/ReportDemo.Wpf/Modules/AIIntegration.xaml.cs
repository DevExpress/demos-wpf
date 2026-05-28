using System;
using System.ClientModel;
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using DevExpress.Xpf.Core;
using DevExpress.XtraReports;
using Microsoft.Extensions.AI;
using ReportWpfDemo.AIOperations;

namespace ReportWpfDemo {
    public partial class AIIntegration : ReportModuleBase {
        public AIIntegration() {
            InitializeComponent();
            WarningText = Properties.Resources.EndpointWarning;
        }
    }

    public class AIIntegrationModuleViewModel : ReportModuleViewModelBase {
        static AIIntegrationModuleViewModel() {
            AIServicesRegistration.RegisterIfNeeded();
        }

        protected override IReport CreateReport() => new XtraReportsDemos.NorthwindTraders.InvoiceReport();
    }
}
