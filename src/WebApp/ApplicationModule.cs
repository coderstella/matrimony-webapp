using Autofac;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.Data.Interfaces;
using WebApp.Data.Repository;
using WebApp.Data.Resolvers;
using WebApp.Handlers;
using WebApp.Services;
using WebApp.Services.Interfaces;

namespace WebApp
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register automapper profile
            builder.RegisterType<DataProfile>().As<Profile>();

            // Register Repository
            builder.RegisterType<PortfolioRepository>().As<IPortfolioRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PortfolioTypeRepository>().As<IPortfolioTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MemberInterestRepository>().As<IMemberInterestRepository>().InstancePerLifetimeScope();

            // Register Services
            builder.RegisterType<PortfolioService>().As<IPortfolioService>().InstancePerLifetimeScope();
            builder.RegisterType<DashboardService>().As<IDashboardService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            // Extensions
            builder.RegisterType<FileHandler>().As<IFileHandler>().InstancePerLifetimeScope();
            
        }
    }
}
