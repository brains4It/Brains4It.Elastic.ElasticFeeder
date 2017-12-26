using Nest;

namespace Brains4It.Elastic.ElasticFeeder.Models
{
    public class EsCountry
    {
        [Keyword(Name = "iso_code")]
        public string iso_code { get; set; }
        [Text(Name = "name")]
        public string name { get; set; }

    }
}
