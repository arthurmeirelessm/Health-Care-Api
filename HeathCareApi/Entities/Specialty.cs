namespace HealthCareApi.Entities
{
    public class Specialty : BaseEntity
    {
        public string NameForSpecialty { get; set; }
        public decimal PriceOfConsult { get; set; }
    }
}
