using System;
using System.IO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;

namespace MVVMDemo.Services {
    public class GridExportService : ServiceBase, IExportService {
        void IExportService.Export(Stream stream, ExportFormat format) {
            GridControl grid = (GridControl)AssociatedObject;
            switch(format) {
                case ExportFormat.Xlsx:
                    grid.View.ExportToXlsx(stream);
                    break;
                case ExportFormat.Pdf:
                    grid.View.ExportToPdf(stream);
                    break;
                default:
                    throw new InvalidOperationException();
            }

        }
    }
}
