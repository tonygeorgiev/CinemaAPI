﻿namespace CinemAPI
{
    using CinemAPI.Infrastructure;
    using CinemAPI.IoCContainer;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Packaging;

    using System.Timers;
    using System.Web.Http;
    public class WebApiApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            IPackage[] packages = new IPackage[]
            {
                new DataPackage(),
                new DomainPackage()
            };
          
            foreach (IPackage package in packages)
            {
                package.RegisterServices(container);
            }

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Just some spaghetti code here
            Timer timer = new Timer();
            ProjectionResolver projectionResolver = new ProjectionResolver(timer);
            projectionResolver.StartEvents(10);
        }
    }
}