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
    public class VaccineController: ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get([FromUri] string name)
        {
            List<RecordView> records = new List<RecordView>();
            records = VaccineManager.GetRecords(name);
            if (records != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(records), System.Text.Encoding.UTF8, "application/json") };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(JsonConvert.SerializeObject("Not found vaccines"), System.Text.Encoding.UTF8, "application/json") };
            }
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<RecordView> records = new List<RecordView>();
            records = VaccineManager.GetRecords("Tina");
            if (records != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(JsonConvert.SerializeObject(records), System.Text.Encoding.UTF8, "application/json") };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(JsonConvert.SerializeObject("Not found vaccines"), System.Text.Encoding.UTF8, "application/json") };
            }
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
