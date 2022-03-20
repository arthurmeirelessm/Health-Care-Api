using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApi.Entities
{
    public class NoteForMedicalCare : BaseEntity
    {
        [ForeignKey("User")]
        public int PatientId { get; set; }

        [ForeignKey("Specialty")]
        public int SpecialtyId { get; set; }

        [RegularExpression(@"^([0-9]{1,2})([.,][0-9]{1})?$", ErrorMessage =
       "Digite o preço da consulta em forma de decimal.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Note { get; set; }

        public virtual User Patient { get; set; }

        public virtual Specialty Specialty { get; set; }

    }
}
