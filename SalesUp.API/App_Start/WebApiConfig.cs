using System.Web.Http;
using System.Web.Http.Cors;

namespace SalesUp.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services de l'Web API 
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Itinéraires de l'Web API 
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
