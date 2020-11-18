namespace BusinessLayer.Managers
{
    using BusinessLayer.BusinessObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class PetManager
    {
        public static List<Pet> GetPets(int clientId = 0) 
        {
            return new List<Pet> 
            {
                new Pet
                {
                    Id = 123456,
                    Name = "Menuy",
                    BirthDate = new DateTime()
                },
                new Pet
                {
                    Id = 789012,
                    Name = "Broo",
                    BirthDate = new DateTime()
                },
            };
        }
    }
}
