using DevExpress.Xpf.DemoBase.Helpers;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace GridDemo {
    [XmlRoot("Countries")]
    public class CountriesData : List<Country> {
        static List<Country> dataSource = null;
        public static List<Country> DataSource {
            get {
                if(dataSource != null)
                    return dataSource;
                Assembly assembly = typeof(CountriesData).Assembly;
                var stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) + "Countries.xml");
                dataSource = SafeXml.Deserialize<CountriesData>(stream);
                return dataSource;
            }
        }
    }

    public class Country {
        public string ActualNWindName { get { return NWindName ?? Name; } }
        public string ActualName { get { return Name ?? NWindName; } }
        public string Name { get; set; }
        public string NWindName { get; set; }
        public byte[] Flag { get; set; }
    }
}
