using Autofac;
using System.Numerics;
using System.Reflection;
using Vehicle.Service;
using VehicleProject.Data;
using VehicleProject.Data.Data;
using VehicleProject.Data.Interfaces;
using VehicleProject.Entity;
using AutoMapper;
using VehicleProject.Data.Helper;

namespace VehicleProject.WEB.AutofacModule
{
    public class AutofacModuleDI : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
                
            builder.RegisterType<IocDbContext>().As<IDbContext>().SingleInstance();
            //  builder.RegisterType<VehicleService>().As<IVehicleService>().SingleInstance();



            builder.RegisterType<AutoMapperProfiles>().As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
