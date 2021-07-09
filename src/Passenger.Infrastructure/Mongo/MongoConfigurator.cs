using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Mongo
{
    public static class MongoConfigurator
    {
        private static bool _initialized;
        public static void Initialize()
        {
            if(_initialized)
            {
                return;
            }
            _initialized = true;
        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register("PassengerConvenstions", new MongoConventions(), x => true);
        }

        private class MongoConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
