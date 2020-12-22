namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RecordDAL : IDAL<Record>
    {
        private PetControlEntities context;

        public RecordDAL()
        {
        }

        public RecordDAL(PetControlEntities context)
        {
            this.context = context;
        }

        public bool Delete(string id)
        {
            try
            {
                using (this.context = new PetControlEntities())
                {
                    Record record = new Record { id = new Guid(id) };
                    this.context.Records.Attach(record);
                    this.context.Records.Remove(record);
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

        public Record Get(string id)
        {
            try
            {
                Record record;
                using (this.context = new PetControlEntities())
                {
                    record = this.context.Records.SingleOrDefault(u => u.id == new Guid(id));
                }

                return record;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Record> GetList()
        {
            using (this.context = new PetControlEntities())
            {
                return this.context.Records.ToList();
            }
        }

        public bool Post(Record record)
        {
            try
            {
                record.id = Guid.NewGuid();
                using (this.context = new PetControlEntities())
                {
                    this.context.Records.Add(record);
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

        public bool Update(ref Record newRecord)
        {
            try
            {
                var id = newRecord.id;
                using (this.context = new PetControlEntities())
                {
                    var oldRecord = this.context.Records.SingleOrDefault(b => b.id == id);
                    if (oldRecord != null)
                    {
                        oldRecord.isVaccine = newRecord.isVaccine;
                        oldRecord.notes = newRecord.notes;
                        oldRecord.recordNumber = newRecord.recordNumber;
                        oldRecord.status = newRecord.status;
                        oldRecord.tags = newRecord.tags;
                        oldRecord.type = newRecord.type;
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
