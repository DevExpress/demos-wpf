using System.Collections.Generic;
using System.Xml.Serialization;
using DevExpress.Utils;

namespace ChartsDemo {

    [XmlRoot("Countries")]
    public class CountriesAreaData : List<Country> {
        static int CompareCountriesByArea(Country country1, Country country2) {
            return country2.Area.CompareTo(country1.Area);
        }

        public static CountriesAreaData Load() {
            CountriesAreaData data = SafeXml.Deserialize<CountriesAreaData>(DataLoader.LoadFromResources("/Data/CountryAreas.xml"));
            data.Sort(CompareCountriesByArea);
            return data;
        }
    }


    public class Country {
        public double Area { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
    }

}
