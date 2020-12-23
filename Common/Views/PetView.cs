namespace Common.Views
{
    using System;

    public class PetView
    {
        public string UserId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Species Specie { get; set; }

        public string SpecieName { get; set; }

        public string Breed { get; set; }

        public string ImageSrc { get; set; }

        public int Age { get; set; }

        public string Description { get; set; }
    }

    public enum Species
    {
        Cat,
        Dog,
        Mouse,
        Bird,
        Other,
    }
}
