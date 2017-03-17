using System.Web.Http;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json.Serialization;
using RESTPlayground01.Infrastructure;

namespace RESTPlayground01
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(
                typeof(IHttpControllerSelector),
                new NamespaceHttpControllerSelector(config));

            // Use camelCase notation for JSON serialization
            config.Formatters.JsonFormatter.SerializerSettings
                .ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Setup dependency injection container
            var container = WindsorConfig.BuildUpContainer();
            config.DependencyResolver = new WindsorDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultRoute",
                routeTemplate: "{namespace}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DiffRoute_WithSideContent",
                routeTemplate: "{namespace}/{controller}/{id}/{side}",
                defaults: new { id = RouteParameter.Optional, side = RouteParameter.Optional }
            );
        }
    }
}
