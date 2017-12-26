using Nest;

namespace Brains4It.Elastic.ElasticFeeder.Models
{
    [ElasticsearchType(Name = "airport")]
    public class EsAirport
    {
        [Keyword(Name="id")]
        public string id { get; set; }
        [Keyword(Name = "type")]
        public string type { get; set; }
        [Text(Name="name")]
        public string name { get; set; }
        [GeoPoint(Name="coordinates")]
        public GeoCoordinate coordinates { get; set; }
        [Number(Name="elevation")]
        public double? elavation { get; set; }
        [Keyword(Name = "continent")]
        public string continent { get; set; }
        [Nested(Name = "country")]
        public EsCountry country { get;set; }
        [Keyword(Name="region")]
        public string region { get; set; }
        [Keyword(Name= "municipality")]
        public string municipality { get; set; }
        [Keyword(Name = "gps_code")]
        public string gps_code { get; set; }
        [Keyword(Name = "iata_code")]
        public string iata_code { get; set; }
        [Keyword(Name = "local_code")]
        public string local_code { get; set; }
    }
}
