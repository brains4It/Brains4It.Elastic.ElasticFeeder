using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.Console.ElasticFeeder
{
    public class EsConnectionFactory
    {
        protected EsConnectionFactory() { }

        public static ElasticClient CreateEsClient(string host, string indexName)
        {
            var uri = new Uri(host);
            var pool = new Elasticsearch.Net.SingleNodeConnectionPool(uri);

            var transportsConnectionSettings = new Nest.ConnectionSettings(pool, new Elasticsearch.Net.HttpConnection(), new SerializerFactory((settings, values) =>
            {
                //settings.Converters.Add(new GeometryConverter());
                //settings.Converters.Add(new CoordinateConverter());
            }))
            .DefaultIndex(indexName)
            .DisableDirectStreaming();

            return new Nest.ElasticClient(transportsConnectionSettings);
        }
    }
}
