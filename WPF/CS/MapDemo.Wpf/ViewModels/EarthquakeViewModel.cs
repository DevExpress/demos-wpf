using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace MapDemo {
    public class EarthquakeViewModel {
        [XmlElement("yr")]
        public int Year { get; set; }
        [XmlElement("mon")]
        public int Month { get; set; }
        [XmlElement("day")]
        public int Day { get; set; }
        [XmlElement("hr")]
        public int Hour { get; set; }
        [XmlElement("min")]
        public int Minute { get; set; }
        [XmlElement("sec")]
        public double Second { get; set; }
        [XmlElement("glat")]
        public double Latitude { get; set; }
        [XmlElement("glon")]
        public double Longitude { get; set; }
        [XmlElement("dep")]
        public double Depth { get; set; }
        [XmlElement("mag")]
        public double Magnitude { get; set; }
    }
    [XmlRoot("Data")]
    public class EarthquakesData {
        static EarthquakesData instance;

        public static List<EarthquakeViewModel> DataItems {
            get { return Instance.Items; }
        }

        public static EarthquakesData Instance {
            get { return instance ?? (instance = CreateInstance()); }
        }

        static EarthquakesData CreateInstance() {
            Stream documentStream = DataLoader.LoadStreamFromResources("/Data/Earthquakes.xml");
            return SafeXml.Deserialize<EarthquakesData>(documentStream);
        }

        [XmlElement("Row")]
        public List<EarthquakeViewModel> Items { get; set; }

        EarthquakesData() {
        }
    }
}
