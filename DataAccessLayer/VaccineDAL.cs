namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VaccineDAL : IDAL<Vaccine>
    {
        private PetControlEntities context;

        public VaccineDAL()
        {
        }

        public VaccineDAL(PetControlEntities context)
        {
            this.context = context;
        }

        public bool Delete(string id)
        {
            try
            {
                using (this.context = new PetControlEntities())
                {
                    var vaccine = new Vaccine { id = new Guid(id) };
                    this.context.Vaccines.Attach(vaccine);
                    this.context.Vaccines.Remove(vaccine);
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

        public Vaccine Get(string id)
        {
            try
            {
                Vaccine vaccine;
                using (this.context = new PetControlEntities())
                {
                    vaccine = this.context.Vaccines.SingleOrDefault(u => u.id == new Guid(id));
                }

                return vaccine;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Vaccine> GetList()
        {
            using (this.context = new PetControlEntities())
            {
                return this.context.Vaccines.ToList();
            }
        }

        public bool Post(Vaccine vaccine)
        {
            try
            {
                vaccine.id = Guid.NewGuid();
                using (this.context = new PetControlEntities())
                {
                    this.context.Vaccines.Add(vaccine);
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

        public bool Update(ref Vaccine newVaccine)
        {
            try
            {
                var id = newVaccine.id;
                using (this.context = new PetControlEntities())
                {
                    var oldVaccine = this.context.Vaccines.SingleOrDefault(b => b.id == id);
                    if (oldVaccine != null)
                    {
                        oldVaccine.cost = newVaccine.cost;
                        oldVaccine.description = newVaccine.description;
                        oldVaccine.disease = newVaccine.disease;
                        oldVaccine.living = newVaccine.living;
                        oldVaccine.name = newVaccine.name;
                        oldVaccine.preparation = newVaccine.preparation;
                        oldVaccine.type = newVaccine.type;
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
