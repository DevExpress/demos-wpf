using DevExpress.DemoData.Models.Vehicles;
#if NET
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif
using System.Collections.Generic;

using System.Linq;
using VehicleModel = DevExpress.DemoData.Models.Vehicles.Model;

namespace DevExpress.DemoData {
    public class VehiclesData {
        VehiclesContext context = new VehiclesContext();
        IEnumerable<VehicleModel> models;
        public IEnumerable<VehicleModel> Models {
            get {
                if(models == null) {
                    var list = new List<VehicleModel>();
                    models = list;
                    context.Models.Load();
                    context.Trademarks.Load();
                    list.AddRange(context.Models.Local);
                }
                return models;
            }
        }
    }
}
