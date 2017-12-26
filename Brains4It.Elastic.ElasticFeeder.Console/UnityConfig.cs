using Brains4It.Elastic.ElasticFeeder.DataReader;
using Brains4It.Library.DataAccess;
using Microsoft.Extensions.Configuration;
using System.IO;
using Unity;

namespace Brains4It.Console.ElasticFeeder
{
    public class UnityConfig
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    RegisterTypes();
                }
                return _container;

            }
        }

        private static void RegisterTypes()
        {
            _container = new UnityContainer();


            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            IConfigurationRoot configuration = builder.Build();

            _container.RegisterType<ICsvReader, CsvReader>();
            _container.RegisterInstance<IConfiguration>(configuration);
            _container.RegisterType<IDataAccess, DataAccess>();
        }

    }
}
