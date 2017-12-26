using Brains4It.Elastic.ElasticFeeder.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.Library.DataAccess
{
    public interface IDataAccess
    {
        IElasticClient AirportElasticClient { get; }

        IEnumerable<EsCountry> GetAllCountries();

        IEnumerable<EsAirport> GetAllAirports();

    }
}
