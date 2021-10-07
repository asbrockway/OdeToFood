using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Data.Services;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace OdeToFood.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterType<InMemoryRestaurantData>()
            //    .As<IRestaurantData>()
            //    .SingleInstance();
            builder.RegisterType<SqlRestaurantData>()
                .As<IRestaurantData>()
                .InstancePerRequest();
            builder.RegisterType<OdeToFoodDbContext>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}