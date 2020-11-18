namespace Host
{
    using Host.Settings;
    using Microsoft.AspNet.SignalR;
    using Microsoft.Owin.Cors;
    using Owin;
    using System.Web.Http;

    public partial class Startup
    {
        public void InitConfig(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            RouteConfig.RegisterRoute(config);
            app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr", map =>
            {
                HubConfiguration hcf = new HubConfiguration();
                map.RunSignalR();
            });
            app.UseWebApi(config);
        }
    }
}
