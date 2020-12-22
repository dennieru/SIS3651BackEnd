namespace DataAccessLayer
{
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            //PetControlEntities context = new PetControlEntities();
            UserDAL petHandler = new UserDAL();
            var pets = petHandler.GetList();
            var pet = pets.FirstOrDefault();
            pet.firstName = "Daniel";
            var user = petHandler.Update(ref pet);
        }
    }
}
