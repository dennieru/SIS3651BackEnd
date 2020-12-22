namespace Host.Controllers
{
    using BusinessLayer.Managers;
    using Common.Views;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class OwnerController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<UserView> pets = new List<UserView>();
            UserView user = UserManager.GetUser("");
            if (user != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pets), System.Text.Encoding.UTF8, "application/json") };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(JsonConvert.SerializeObject("NoData"), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri] string name)
        {
            UserView user = UserManager.GetUser(name);
            if (user != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, "application/json") };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(JsonConvert.SerializeObject("NoData"), System.Text.Encoding.UTF8, "application/json") };
            }
        }
    }

    public class Owner 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
