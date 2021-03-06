﻿using System.Data.Entity;
using MegaStore.Persistence;
using MegaStore.Service;
using StructureMap;
using StructureMap.Pipeline;
using Whisperer.Models;
using Whisperer.Service;
using ConfigurationModel = Whisperer.Models.Configuration;
using ConfigurationService = Whisperer.Service.Configuration;

namespace Whisperer.DependencyResolution
{
    public static class Ioc
    {
        public static Container Container;
        public static void Initialize()
        {
            Container = new Container(x =>
            {
                x.For(typeof(AppContext)).LifecycleIs(Lifecycles.Transient);
                x.For<DbContext>().LifecycleIs(Lifecycles.Transient).Use<AppContext>();

                x.For<IRepository<Answer>>().Use<Repository<Answer, AppContext>>();
                x.For<IRepository<Meeting>>().Use<Repository<Meeting, AppContext>>();
                x.For<IRepository<Question>>().Use<Repository<Question, AppContext>>();
                x.For<IRepository<Team>>().Use<Repository<Team, AppContext>>();
                x.For<IRepository<Channel>>().Use<Repository<Channel, AppContext>>();
                x.For<IRepository<User>>().Use<Repository<User, AppContext>>();

                x.For<IService<Answer>>().Use<BaseService<Answer>>();
                x.For<IService<Meeting>>().Use<BaseService<Meeting>>();
                x.For<IService<Question>>().Use<BaseService<Question>>();
                x.For<IService<Team>> ().Use<BaseService<Team>> ();
                x.For<IService<Channel>>().Use<BaseService<Channel>>();
                x.For<IService<User>>().Use<BaseService<User>>();

                x.For<IRepository<ConfigurationModel>>().Use<NotTrackedRepository<ConfigurationModel, AppContext>>();
                x.For<IService<ConfigurationModel>>().Use<BaseService<ConfigurationModel>>();
                x.For<IReadOnlyService<ConfigurationModel>>().Use<ReadOnlyService<ConfigurationModel>>();

                x.For<IPostBackService>().Use<PostBackService>();

                x.Scan(sc =>
                {
                    sc.LookForRegistries();
                    sc.TheCallingAssembly();
                    sc.WithDefaultConventions();
                });
            });
        }

    }
}