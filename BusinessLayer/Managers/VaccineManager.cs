namespace BusinessLayer.Managers
{
    using Common;
    using Common.Views;
    using DataAccessLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class VaccineManager
    {
        public static void SaveUser(UserView userToSave)
        {
            UserDAL userHandler = new UserDAL();
            User pet = new User
            {
                firstName = userToSave.FirstName,
                lastName = userToSave.LastName,
                address = userToSave.Address,
                description = userToSave.Description,
                email = userToSave.Email,
                nickName = userToSave.NickName,
                createdBy = Constant.ADMIN_EMAIL,
                createdDate = DateTime.Now,
            };
            userHandler.Post(pet);
        }

        public static List<RecordView> GetRecords(string name)
        {
            List<RecordView> recordViews = new List<RecordView>();
            RecordDAL recordHandler = new RecordDAL();
            PetDAL petHandler = new PetDAL();
            List<Record> records = recordHandler.GetList();
            List<Pet> petList = petHandler.GetList();
            Pet pet = petList.Where(p => p.name.Trim() == name).FirstOrDefault();
            if (pet != null && !string.IsNullOrEmpty(pet.name))
            {
                records = records.Where(r => r.petId == pet.id).ToList();
                foreach (Record record in records)
                {
                    RecordView recordView = new RecordView()
                    {
                        Id = record.id.ToString(),
                        IsVaccine = record.isVaccine.Value,
                        Name = record.recordNumber == null ? "" : record.recordNumber.Trim(),
                        Notes = record.notes == null ? "" : record.notes.Trim(),
                        RecordNumber = record.recordNumber.Trim(),
                        Status = record.status == null ? "" : record.status.Trim(),
                        Tags = record.tags != null ? record.tags.Split(',').ToList() : new List<string>(),
                        Type = record.type == null ? "" : record.type.Trim(),
                    };
                    if (record.isVaccine.Value)
                    {
                        VaccineDAL vaccineHandler = new VaccineDAL();
                        List<Vaccine> vaccines = vaccineHandler.GetList();
                        vaccines = vaccines.Where(v => v.id == record.vaccineId).ToList();
                        List<VaccineView> vaccineViews = new List<VaccineView>();
                        foreach (var vaccine in vaccines)
                        {
                            vaccineViews.Add(new VaccineView
                            {
                                Cost = vaccine.cost.ToString(),
                                Description = vaccine.description == null ? "" : vaccine.description.Trim(),
                                Disease = vaccine.disease == null ? "" : vaccine.disease.Trim(),
                                Id = vaccine.id.ToString(),
                                Living = vaccine.living == true ? "Is living" : "Not living",
                                Name = vaccine.name == null ? "" : vaccine.name.Trim(),
                                Preparation = vaccine.preparation == null ? "" : vaccine.preparation.Trim(),
                                Type = vaccine.type == null ? "" : vaccine.type.Trim(),
                            });
                        }
                        recordView.Vaccines = vaccineViews;
                    }
                    recordView.Tags = recordView.Tags.Select(s => s.Trim()).ToList();

                    recordViews.Add(recordView);
                }

            }

            if (recordViews.Count >= 1)
            {
                return recordViews;
            }
            else
            {
                return null;
            }
        }
    }
}
