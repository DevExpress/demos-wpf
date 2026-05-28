using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;

namespace EditorsDemo {
    [XmlRoot("Countries")]
    public class CountriesData : List<Country> {
        static List<Country> dataSource = null;
        public static List<Country> DataSource {
            get {
                if(dataSource != null)
                    return dataSource;
                Assembly assembly = Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("EditorsDemo.Data.", assembly) + "Countries.xml");
                dataSource = SafeXml.Deserialize<CountriesData>(stream);
                return dataSource;
            }
        }
    }

    public class Country {
        public string ActualName { get { return NWindName ?? Name; } }
        public string Name { get; set; }
        public string NWindName { get; set; }
        public byte[] Flag { get; set; }
    }
}
