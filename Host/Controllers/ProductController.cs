namespace Host.Controllers
{
    using System.Web.Http;

    public class ProductController : ApiController
    {
        [HttpGet]
        public Product Get()
        {
            System.Console.WriteLine("Method called");
            return new Product { Id = 25, Name = "John Doe" };
        }

        public string Get(int id)
        {
            return $"my: {id}";
        }
    }

    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
