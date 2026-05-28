using System.Collections.Generic;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace ChartsDemo {

    [XmlRoot("CountriesInfo")]
    public class CountriesInfo : List<CountryStatisticInfo> {
        public static CountriesInfo Load() {
            return SafeXml.Deserialize<CountriesInfo>(DataLoader.LoadFromResources("/Data/Top10LargestCountriesInfo.xml"));
        }
    }


    [XmlType("CountryInfo")]
    public class CountryStatisticInfo {
        public string Name { get; set; }
        [XmlArray("Statistic")]
        [XmlArrayItem("PopulationStatisticByYear")]
        public List<PopulationStatisticByYear> PopulationDynamic { get; set; }
        public double AreaSqrKilometers { get; set; }
        public double AreaMSqrKilometers { get { return AreaSqrKilometers / 1000000; } }
    }


    public class PopulationStatisticByYear {
        public int Year { get; set; }
        public double Population { get; set; }
        public double UrbanPercent { get; set; }
        public double PopulationMillionsOfPeople { get { return Population / 1000000; } }
        public double RuralPercent { get { return 100 - UrbanPercent; } }
    }

}
