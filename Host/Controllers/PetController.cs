namespace Host.Controllers
{
    using BusinessLayer.Managers;
    using Common.Views;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    //[RoutePrefix("api/pet")]
    public class PetController: ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get([FromUri] int id)
        {
            var pet = new PetView { Id = id.ToString(), Name = "John Doe", BirthDate = System.DateTime.Now };
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pet), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri] string nickName)
        {
            List<PetView> pets = new List<PetView>();
            UserView user = UserManager.GetUser(nickName);
            if (user != null)
            {
                pets = PetManager.GetPets(user.Id);
            }

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pets), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var pets = PetManager.GetPets();

            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(pets), System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] PetView pet, HttpRequestMessage request)
        {
            try
            {
                PetManager.SavePet(pet);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
