using Unity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System;
using Brains4It.Library.DataAccess;
using Brains4It.Elastic.ElasticFeeder.Models;

namespace Brains4It.Console.ElasticFeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = UnityConfig.Container.Resolve<IConfiguration>();
            var dataAccess = UnityConfig.Container.Resolve<IDataAccess>();

            string airportIndexName = configuration.GetSection("Elastic").GetSection("Index").Value;

            if (dataAccess.AirportElasticClient.IndexExists(airportIndexName).Exists)
                dataAccess.AirportElasticClient.DeleteIndex(airportIndexName);

            if (!dataAccess.AirportElasticClient.IndexExists(airportIndexName).Exists)
            {
                dataAccess.AirportElasticClient.CreateIndex(airportIndexName, c => c
                    .Settings(s => s
                        .NumberOfShards(3)
                        .NumberOfReplicas(2)
                        .Setting("index.max_result_window", 1000000))
                    .Mappings(ms => ms
                        .Map<EsAirport>(m => m.AutoMap()
                            .Properties(ps => ps
                                    .Nested<EsCountry>(n => n
                                        .Name(p => p.country)
                                        .AutoMap())
                            )
                        )
                    )
                );
            }

            Stopwatch sAirport = new Stopwatch();
            sAirport.Start();

            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("--------------- Airports ---------------");
            System.Console.ResetColor();
            // Fetch Zones from DB
            int cntAirports = 0;
            
            foreach (var airport in dataAccess.GetAllAirports())
            {
                var res = dataAccess.AirportElasticClient.Index(airport);
                if (!res.IsValid)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("airport : " + airport.id);
                    System.Console.WriteLine("Something happened : " + res.ToString());
                    System.Console.ResetColor();
                }
                else
                {
                    cntAirports++;
                }
            }
            sAirport.Stop();
            System.Console.WriteLine($"{cntAirports} airports indexed in {sAirport.ElapsedMilliseconds} ms");

            
        }
    }
}
