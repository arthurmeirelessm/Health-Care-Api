namespace HealthCareApi.Entities
{
    public class PatientSpecialty
    {
        public int PatientId { get; set; }
        public int SpecialtyId { get; set; }
        public User Patient { get; set; }
        public Specialty Specialty { get; set; }

    }
}
