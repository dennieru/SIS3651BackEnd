namespace BusinessLayer.Managers
{
    using Common;
    using Common.Views;
    using DataAccessLayer;
    using System;
    using System.Collections.Generic;
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
            };
            petHandler.Post(pet);
        }

        public static List<PetView> GetPets(string  userId= "")
        {
            PetDAL petHandler = new PetDAL();
            List<PetView> petList = new List<PetView>();
            var pets = petHandler.GetList();

            foreach (var pet in pets)
            {
                int age = DateTime.Today.Year - (pet.birthDate != null? pet.birthDate.Value.Year: DateTime.Today.Year);
                petList.Add(new PetView
                {
                    Name = pet.name.Trim(),
                    BirthDate = pet.birthDate != null ? pet.birthDate.Value : DateTime.Today,
                    Age = age,
                    Description = pet.description.Trim(),
                    Breed = pet.breed.Trim(),
                    SpecieName = pet.specie.Trim(),
                    Specie = GetSpecie(pet.specie),
                    ImageSrc = GetImgSrc(pet.specie),
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

        private static string GetImgSrc(string specie)
        {
            string returnSpecie = "pony-icon";
            string pivot = Regex.Replace(specie, @"\s+", "").ToLower();

            switch (pivot)
            {
                case "cat":
                    returnSpecie = "cat-icon";
                    break;
                case "dog":
                    returnSpecie = "dog-icon";
                    break;
                case "bird":
                    returnSpecie = "parrot-icon";
                    break;
                case "mouse":
                    returnSpecie = "mouse-icon";
                    break;
            }

            return returnSpecie;
        }
    }
}
