using Brains4It.Elastic.ElasticFeeder.DataReader;
using Brains4It.Elastic.ElasticFeeder.Models;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Brains4It.Library.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly ICsvReader csvReader;
        private readonly IConfiguration configuration;
        private readonly IElasticClient airportElasticClient;
        public DataAccess(ICsvReader csvReader, IConfiguration configuration)
        {
            this.csvReader = csvReader;
            this.configuration = configuration;
            var uri = new Uri(configuration.GetSection("Elastic").GetSection("Host").Value);
            var pool = new Elasticsearch.Net.SingleNodeConnectionPool(uri);

            var transportsConnectionSettings = new Nest.ConnectionSettings(pool, new Elasticsearch.Net.HttpConnection(), new SerializerFactory((settings, values) =>
            {
                //settings.Converters.Add(new GeometryConverter());
                //settings.Converters.Add(new CoordinateConverter());
            }))
            .DefaultIndex(configuration.GetSection("Elastic").GetSection("Index").Value)
            .DisableDirectStreaming();

            airportElasticClient = new Nest.ElasticClient(transportsConnectionSettings);

        }

        public IElasticClient AirportElasticClient { get { return airportElasticClient; } }

        public IEnumerable<EsCountry> GetAllCountries()
        {
            return csvReader.GetCountries(configuration.GetSection("Path").GetSection("country").Value).Select(c =>
                new EsCountry
                {
                    iso_code = c.iso,
                    name = c.name

                });
        }

        public IEnumerable<EsAirport> GetAllAirports()
        {
            var allCountries = GetAllCountries();
            return csvReader.GetAirports(configuration.GetSection("Path").GetSection("airport").Value).Select(a =>
                new EsAirport
                {
                    id = a.id,
                    type = a.type,
                    name = a.name,
                    coordinates = string.IsNullOrEmpty(a.lat) || string.IsNullOrEmpty(a.lng) ? null : new Nest.GeoCoordinate(Convert.ToDouble(a.lat.Replace(".", ",")), Convert.ToDouble(a.lng.Replace(".", ","))),
                    elavation = string.IsNullOrEmpty(a.elavation) ? (double?)null : Convert.ToDouble(a.elavation.Replace(".", ",")),
                    continent = a.continent,
                    country = allCountries.FirstOrDefault(c => c.iso_code.Equals(a.country)),
                    region = a.region,
                    municipality = a.municipality,
                    gps_code = a.gps_code,
                    iata_code = a.iata_code,
                    local_code = a.local_code
                });

        }
    }
}
