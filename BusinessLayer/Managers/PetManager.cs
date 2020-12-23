namespace BusinessLayer.Managers
{
    using Common;
    using Common.Views;
    using DataAccessLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class PetManager
    {
        public static void SavePet(PetView petToSave)
        {
            PetDAL petHandler = new PetDAL();
            Pet pet = new Pet
            {
                name = petToSave.Name,
                birthDate = petToSave.BirthDate,
                breed = petToSave.Breed,
                description = petToSave.Description,
                specie = petToSave.SpecieName,
                createdDate = DateTime.Now,
                createdBy = Constant.ADMIN_EMAIL,
                userId = new Guid(petToSave.UserId),
            };
            petHandler.Post(pet);
        }

        public static PetView GetPet(Guid id)
        {
            PetDAL petHandler = new PetDAL();
            List<PetView> petList = new List<PetView>();
            List<Pet> pets = petHandler.GetList();
            Pet pet = pets.Where(u => u.id.Equals(id)).FirstOrDefault();
            int age = DateTime.Today.Year - (pet.birthDate != null ? pet.birthDate.Value.Year : DateTime.Today.Year);
            if (pet != null)
            {
                return new PetView
                {
                    Name = pet.name.Trim(),
                    BirthDate = pet.birthDate != null ? pet.birthDate.Value : DateTime.Today,
                    Age = age,
                    Description = pet.description.Trim(),
                    Breed = pet.breed.Trim(),
                    SpecieName = pet.specie.Trim(),
                    Specie = GetSpecie(pet.specie),
                    ImageSrc = string.Format("pets/{0}.jpg", pet.name.Trim()),
                };
            }
            else
            {
                return null;
            }
        }

        public static List<PetView> GetPets()
        {
            PetDAL petHandler = new PetDAL();
            List<PetView> petList = new List<PetView>();
            List<Pet> pets = petHandler.GetList();

            foreach (var pet in pets)
            {
                int age = DateTime.Today.Year - (pet.birthDate != null ? pet.birthDate.Value.Year : DateTime.Today.Year);
                petList.Add(new PetView
                {
                    Name = pet.name.Trim(),
                    BirthDate = pet.birthDate != null ? pet.birthDate.Value : DateTime.Today,
                    Age = age,
                    Description = pet.description.Trim(),
                    Breed = pet.breed.Trim(),
                    SpecieName = pet.specie.Trim(),
                    Specie = GetSpecie(pet.specie),
                    ImageSrc = string.Format("pets/{0}.jpg", pet.name.Trim()),
                });
            }

            return petList;
        }

        public static List<PetView> GetPets(Guid userId)
        {
            PetDAL petHandler = new PetDAL();
            List<PetView> petList = new List<PetView>();
            List<Pet> pets = petHandler.GetList();
            pets = pets.Where(p => p.userId == userId).ToList();

            foreach (var pet in pets)
            {
                int age = DateTime.Today.Year - (pet.birthDate != null ? pet.birthDate.Value.Year : DateTime.Today.Year);
                petList.Add(new PetView
                {
                    Name = pet.name.Trim(),
                    BirthDate = pet.birthDate != null ? pet.birthDate.Value : DateTime.Today,
                    Age = age,
                    Description = pet.description.Trim(),
                    Breed = pet.breed.Trim(),
                    SpecieName = pet.specie.Trim(),
                    Specie = GetSpecie(pet.specie),
                    ImageSrc = string.Format("pets/{0}.jpg", pet.name.Trim()),
                });
            }

            return petList;
        }

        private static Species GetSpecie(string specie) 
        {
            Species returnSpecie = Species.Other;
            string pivot = Regex.Replace(specie, @"\s+", "").ToLower();

            switch (pivot)
            {
                case "cat":
                    returnSpecie = Species.Cat;
                    break;
                case "dog":
                    returnSpecie = Species.Dog;
                    break;
                case "bird":
                    returnSpecie = Species.Bird;
                    break;
                case "mouse":
                    returnSpecie = Species.Mouse;
                    break;
            }

            return returnSpecie;
        }
    }
}
