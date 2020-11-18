namespace Host.Controllers
{
    using System.Web.Http;

    public class OwnerController : ApiController
    {
        [HttpGet]
        public Owner Get()
        {
            System.Console.WriteLine("Method called");
            return new Owner { Id = 25, Name = "John Doe" };
        }

        public string Get(int id)
        {
            return $"my: {id}";
        }
    }

    public class Owner 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
