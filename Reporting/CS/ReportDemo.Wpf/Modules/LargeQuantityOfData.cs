using System;
using System.Data.SQLite;
using System.IO;
using DevExpress.Data.Utils;
using DevExpress.XtraPrinting.Caching;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;

namespace ReportWpfDemo {
    public class LargeQuantityOfData : ReportModuleViewModelBase {
        DbDocumentStorageHelper documentStorageHelper;

        string FolderPath {
            get { return Path.Combine(SafeEnvironment.LocalApplicationData, "DevExpress\\ReportCache"); }
        }

        public override void Clean() {
            base.Clean();
            documentStorageHelper.Dispose();
        }

        protected override IReport CreateReport() {
            documentStorageHelper = new DbDocumentStorageHelper(FolderPath);
            DocumentStorage storage = documentStorageHelper.CreateStorage();
            XtraReport report = new XtraReportsDemos.CachedDocumentSourceReport.Report();
            return new CachedReportSource(report, storage);
        }

        protected override bool CanShowDesigner() {
            return false;
        }
    }

    class DbDocumentStorageHelper : IDisposable {
        const string dbFileName = "ReportCache";
        const string dbFileExtension = ".db";

        string path;
        public DbDocumentStorageHelper(string path) {
            this.path = path;
        }
        public DocumentStorage CreateStorage() {
            Clear();
            CreateDirectory();

            string dbFileName = GetDbFileName();
            SQLiteConnection.CreateFile(dbFileName);
            return new DbDocumentStorage("XpoProvider=SQLite;Data Source=" + dbFileName + ";Version=3;");
        }
        public void Clear() {
            SQLiteConnection.ClearAllPools();

            if(Directory.Exists(path))
                ClearFiles(path);
        }
        static void ClearFiles(string path) {
            foreach(string file in Directory.GetFiles(path, dbFileName + "*"))
                try {
                    File.Delete(file);
                }
                catch { }
        }
        string GetDbFileName() {
            string dbFile;
            int i = 0;
            do {
                dbFile = Path.Combine(path, dbFileName + i++ + dbFileExtension);
            } while(File.Exists(dbFile));
            return dbFile;
        }
        void CreateDirectory() {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public void Dispose() {
            Clear();
        }
    }
}
