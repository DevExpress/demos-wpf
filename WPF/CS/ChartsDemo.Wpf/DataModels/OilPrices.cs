using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace ChartsDemo {

    [XmlRoot("OilPrices")]
    public class OilPrices : List<OilPricesByCompany> {
        public static OilPrices Load() {
            return SafeXml.Deserialize<OilPrices>(DataLoader.LoadFromResources("/Data/OilPrices.xml"));
        }
        public static List<OilPriceByDate> GetEuropeBrentPrices() {
            OilPrices prices = Load();
            return prices.First(x => x.CompanyName == "Europe Brent").Prices; 
        }
    }


    public class OilPricesByCompany {
        public string CompanyName { get; set; }
        public List<OilPriceByDate> Prices { get; set; }
    }


    public class OilPriceByDate {
        public DateTime Timestamp { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }

}
