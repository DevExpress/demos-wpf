using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace SankeyDemo {
    public class ProductTransfer {
        public string TypeFrom { get; set; }
        public string TypeTo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double TotalPrice { get; set; }
    }

    public class ProductTransfers {

        public static List<ProductTransfer> GetData() {
            string path = Utils.GetRelativePath("ProductTransfers.xml");
            using(Stream reader = new FileStream(path, FileMode.Open)) {
                var data = SafeXml.Deserialize<List<ProductTransfer>>(reader);
                return data;
            }
        }
    }
}
