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
        public static void SaveRecord(RecordView recordToSave)
        {
            RecordDAL recordHandler = new RecordDAL();
            Random _random = new Random();
            VaccineView recordVaccine = recordToSave.Vaccines.FirstOrDefault();
            if (recordVaccine != null)
            {
                VaccineView vaccine = VaccineManager.GetVaccine(recordVaccine.Name);
                if (vaccine != null)
                {
                    Record record = new Record
                    {
                        isVaccine = true,
                        recordNumber = "RCRD" + _random.Next(20, 300),
                        notes = "",
                        petId = new Guid(recordToSave.PetId),
                        status = "open",
                        tags = GetAnualTags(recordToSave.Tags, recordToSave.Vaccines),
                        type = "vaccine",
                        vaccineId = new Guid(vaccine.Id),
                        createdDate = DateTime.Now,
                        createdBy = Constant.ADMIN_EMAIL,
                    };
                    recordHandler.Post(record);
                }
            }
        }

        private static VaccineView GetVaccine(string name)
        {
            VaccineDAL vaccineHandler = new VaccineDAL();
            List<Vaccine> vaccines = vaccineHandler.GetList();
            Vaccine vaccine = vaccines.Where(v => v.name == name).FirstOrDefault();
            if (vaccine != null)
            {
                return new VaccineView
                {
                    Cost = vaccine.cost.ToString(),
                    Id = vaccine.id.ToString(),
                    Description = vaccine.description.Trim(),
                    Name = vaccine.name.Trim(),
                    Type = vaccine.type.Trim(),
                    Disease = vaccine.disease.Trim(),
                    Living = vaccine.living.ToString(),
                    Preparation = vaccine.preparation.Trim(),
                };
            }
            else
            {
                return null;
            }
        }

        private static string GetAnualTags(List<string> tags, List<VaccineView> vaccines)
        {
            string tag = string.Join(",", tags);
            foreach (var vaccine in vaccines)
            {
                if (vaccine.Type == "annual")
                {
                    tag += DateTime.Now.Year.ToString();
                    break;
                }
            }

            return tag;
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
