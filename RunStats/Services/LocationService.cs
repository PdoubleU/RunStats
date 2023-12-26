using System.Xml.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RunStats.Services
{
    public class LocationService
    {
        public class Address
        {
            [DataMember]
            public string road { get; set; }
            [DataMember]
            public string suburb { get; set; }
            [DataMember]
            public string city { get; set; }
            [DataMember]
            public string state_district { get; set; }
            [DataMember]
            public string state { get; set; }
            [DataMember]
            public string postcode { get; set; }
            [DataMember]
            public string country { get; set; }
            [DataMember]
            public string country_code { get; set; }
        }
        public class LocationData
        {
            [DataMember]
            public string place_id { get; set; }
            [DataMember]
            public string licence { get; set; }
            [DataMember]
            public string osm_type { get; set; }
            [DataMember]
            public string osm_id { get; set; }
            [DataMember]
            public string lat { get; set; }
            [DataMember]
            public string lon { get; set; }
            [DataMember]
            public string display_name { get; set; }
            [DataMember]
            public Address address { get; set; }
        }
        public static LocationData getAddress(string lat, string lng)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lng);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LocationData));
            LocationData locationData = (LocationData)ser.ReadObject(new MemoryStream(jsonData));

            return locationData;
        }
    }
}
