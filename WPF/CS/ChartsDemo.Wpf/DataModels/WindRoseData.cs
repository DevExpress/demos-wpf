using System.Collections.Generic;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace ChartsDemo {

    [XmlRoot("WindRoseData")]
    public class WindRoseData : List<WindInfo> {
        public static WindRoseData Load() {
            return SafeXml.Deserialize<WindRoseData>(DataLoader.LoadFromResources("/Data/WindRoseData.xml"));
        }
    }


    public class WindInfo {
        public string Direction { get; set; }
        public int Speed { get; set; }
    }

}
