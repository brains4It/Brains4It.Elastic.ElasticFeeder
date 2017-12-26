using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.Elastic.ElasticFeeder.DataReader
{
    public interface ICsvReader
    {
        IEnumerable<Airport> GetAirports(string fileName);
        IEnumerable<Country> GetCountries(string fileName);
    }
}
