using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Brains4It.Elastic.ElasticFeeder.DataReader
{
    public class CsvReader : BaseCsvReader, ICsvReader
    {
        public IEnumerable<Airport> GetAirports(string fileName)
        {
            List<Airport> results = new List<Airport>();
            string[] allLines = ParseCsv(fileName);

            foreach (var line in allLines.Skip(1))
            {
                var items = line.Split(';');

                Airport item = new Airport
                {
                    id = items[0],
                    type = items[1],
                    name = items[2],
                    lng = !string.IsNullOrEmpty(items[3]) && items[3].Split(",").Count() == 2 ? items[3].Split(",")[0].Replace("\"", string.Empty) : null,
                    lat = !string.IsNullOrEmpty(items[3]) && items[3].Split(",").Count() == 2 ? items[3].Split(",")[1].Replace("\"", string.Empty) : null,
                    elavation = items[4],
                    continent = items[5],
                    country = items[6],
                    region = items[7],
                    municipality = items[8],
                    gps_code = items[9],
                    iata_code = items[10],
                    local_code = items[11]
                };

                results.Add(item);
            }

            return results;
        }

        public IEnumerable<Country> GetCountries(string fileName)
        {
            List<Country> results = new List<Country>();
            string[] allLines = ParseCsv(fileName);

            foreach (var line in allLines.Skip(1))
            {
                var items = line.Split(',');

                Country item = new Country
                {
                    name = items[0],
                    iso = items[1]
                };

                results.Add(item);
            }

            return results;
        }
        
    }

    public class Airport
    {

        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string elavation { get; set; }
        public string continent { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string municipality { get; set; }
        public string gps_code { get; set; }
        public string iata_code { get; set; }
        public string local_code { get; set; }
    }


    public class Country
    {
        public string iso { get; set; }
        public string name { get; set; }
    }
}
