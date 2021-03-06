﻿namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PetDAL : IDAL<Pet>
    {
        private PetControlEntities context;

        public PetDAL()
        {
        }

        public PetDAL(PetControlEntities context)
        {
            this.context = context;
        }

        public bool Delete(string id)
        {
            try
            {
                using (this.context = new PetControlEntities())
                {
                    Pet pet = new Pet { id = new Guid(id) };
                    this.context.Pets.Attach(pet);
                    this.context.Pets.Remove(pet);
                    this.context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public Pet Get(string id)
        {
            try
            {
                Pet pet;
                using (this.context = new PetControlEntities())
                {
                    pet = this.context.Pets.SingleOrDefault(u => u.id == new Guid(id));
                }

                return pet;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Pet> GetList()
        {
            using (this.context = new PetControlEntities())
            {
                return this.context.Pets.ToList();
            }
        }

        public bool Post(Pet pet)
        {
            try
            {
                pet.id = Guid.NewGuid();
                using (this.context = new PetControlEntities())
                {
                    this.context.Pets.Add(pet);
                    this.context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(ref Pet newPet)
        {
            try
            {
                var id = newPet.id;
                using (this.context = new PetControlEntities())
                {
                    var oldPet = this.context.Pets.SingleOrDefault(b => b.id == id);
                    if (oldPet != null)
                    {
                        oldPet.name = newPet.name;
                        oldPet.specie = newPet.specie;
                        oldPet.birthDate = newPet.birthDate;
                        oldPet.breed = newPet.breed;
                        oldPet.description = newPet.description;
                        this.context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
