
using Ninject.Web.Common;
using VehicleProjectWeb.App_Start;
using Ninject;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;

using Ninject.Web.Common.WebHost;
using VehicleProject.Data;
using Vehicle.Service;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon),"Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon),"Stop")]


namespace VehicleProjectWeb.App_Start
{
    using System.Web;
    public static class NinjectWebCommon
    {

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns></returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }


        /// <summary>
        /// Load your modules or register your services here!
            /// </summary>
            /// <param name="kernel">The kernel.</param>
            private static void RegisterServices(IKernel kernel)
            {
                kernel.Bind<IDbContext>().To<IocDbContext>().InRequestScope();
                kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
                kernel.Bind<IVehicleService>().To<VehicleService>();
            }
    }
}
