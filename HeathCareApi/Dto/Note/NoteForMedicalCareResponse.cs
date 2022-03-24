using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApi.Dto.Note
{
    public class NoteForMedicalCareResponse
    {
         public int Id { get; set; }
        public int PatientId { get; set; }
        public int SpecialtyId { get; set; }

        [RegularExpression(@"^([0-9]{1,2})([.,][0-9]{1})?$", ErrorMessage =
       "Digite o preço da consulta em forma de decimal.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Note { get; set; }
        public DateTime CreatedId { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual Entities.User Patient { get; set; }
        public virtual Entities.Specialty Specialty { get; set; }

    }
}
