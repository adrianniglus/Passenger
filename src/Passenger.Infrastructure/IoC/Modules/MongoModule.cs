﻿using Autofac;
using MongoDB.Driver;
using Passenger.Infrastructure.Mongo;
using Passenger.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class MongoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
            {
                var settings = c.Resolve<MongoSettings>();

                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();


            builder.Register((c, p) =>
            {
                var client = c.Resolve<MongoClient>();
                var settings = c.Resolve<MongoSettings>();
                var database = client.GetDatabase(settings.Database);


                return database;
            }).As<IMongoDatabase>();

            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IMongoRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
