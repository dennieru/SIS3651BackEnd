using System.Collections.Generic;

namespace Common.Views
{
    public class RecordView
    {
        public string Id { get; set; }
        
        public string PetId { get; set; }

        public string Name { get; set; }

        public string RecordNumber { get; set; }

        public string Notes { get; set; }

        public List<string> Tags { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public bool IsVaccine { get; set; }

        public List<VaccineView> Vaccines { get; set; }
    }
}
