using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class OLAPKPI : PivotGridDemoModule {
        static string sampleFileName;

        public OLAPKPI() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid;
            if(UseXmlaConnection)
                pivotGrid.OlapDataProvider = OlapDataProvider.Xmla;
            pivotGrid.SetOlapConnectionStringAsync("Provider=msolap;Data Source=" + SampleFileName + ";Initial Catalog=" + SampleCatalogName + ";Cube Name=Adventure Works");
        }

        static string SampleFileName {
            get {
                if(UseXmlaConnection)
                    return "https://demos.devexpress.com/Services/OLAP/msmdpump.dll";
                if(string.IsNullOrEmpty(sampleFileName)) {
                    sampleFileName = Path.GetFullPath(DataFilesHelper.FindFile("AdventureWorks.cub", DataFilesHelper.DataPath));
                    if(File.Exists(sampleFileName))
                        try {
                            File.SetAttributes(sampleFileName, FileAttributes.Normal);
                        }
                        catch { }
                }
                return sampleFileName;
            }
        }

        static string SampleCatalogName {
            get {
                if(UseXmlaConnection)
                    return "Adventure Works DW Standard Edition 2026";
                return "Adventure Works";
            }
        }

        static bool UseXmlaConnection {
            get { return !DevExpress.XtraPivotGrid.Data.OLAPMetaGetter.IsProviderAvailable; }
        }
    }
}
