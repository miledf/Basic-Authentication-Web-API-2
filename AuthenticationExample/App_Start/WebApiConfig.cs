using System.Web.Http;
using AuthenticationExample.Filters;

namespace AuthenticationExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressHostPrincipal();
            config.Filters.Add(new BasicAuthenticationAttribute());            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
