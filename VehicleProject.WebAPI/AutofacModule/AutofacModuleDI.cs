using Autofac;
using VehicleProject.Repository;
using VehicleProject.Repository.Common;
using VehicleProject.Data;
using VehicleProject.Common;
using AutoMapper;
using VehicleProject.Common.Helper;
using VehicleProject.Service.Common;
using VehicleProject.Service;
namespace VehicleProject.WEB.AutofacModule
{
    public class AutofacModuleDI : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
                
            builder.RegisterType<IocDbContext>().As<IDbContext>().SingleInstance();
            builder.RegisterType<VehicleService>().As<IVehicleService>().SingleInstance();  


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
