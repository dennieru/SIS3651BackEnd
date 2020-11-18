namespace Host.Controllers
{
    using BusinessLayer.BusinessObjects;
    using BusinessLayer.Managers;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    //[RoutePrefix("api/pet")]
    public class PetController: ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get([FromUri] int id)
        {
            var pet = new Pet { Id = id, Name = "John Doe", BirthDate = System.DateTime.Now };
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pet), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri] string name)
        {
            Client client = ClientManager.GetClient(name);
            var pets = PetManager.GetPets(client.Id);

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pets), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var pets = PetManager.GetPets();

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pets), System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
