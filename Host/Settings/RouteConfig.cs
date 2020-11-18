namespace Host.Settings
{
    using System.Web.Http;

    public static class RouteConfig
    {
        public static void RegisterRoute(HttpConfiguration route)
        {/*
            route.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            route.Routes.MapHttpRoute(
                name: "MethodOne",
                routeTemplate: "api/{controller}/{action}/{id}/{type}",
                defaults: new { id = RouteParameter.Optional, type = RouteParameter.Optional }
            );*/

            // Controller with ID
            // To handle routes like "/api/VTRouting/1"
            route.Routes.MapHttpRoute(
                name: "ControllerAndId",
                routeTemplate: "api/{controller}/{id}",
                defaults: null,
                constraints: new { id = @"^\d+$" } // Only integers 
            );

            // Controllers with Actions
            // To handle routes like "/api/VTRouting/route"
            route.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );

            // Controller Only
            // To handle routes like "/api/VTRouting"
            route.Routes.MapHttpRoute(
                name: "ControllerOnly",
                routeTemplate: "api/{controller}"
            );

            route.Routes.MapHttpRoute(
                name: "ControllerAndName",
                routeTemplate: "api/{controller}/{name}"
            );
        }
    }
}
