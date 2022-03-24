using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApi.Dto.User
{
    public class NoteForMedicalCareRequest
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SpecialtyId { get; set; }

        [RegularExpression(@"^([0-9]{1,2})([.,][0-9]{1})?$", ErrorMessage =
       "Digite o preço da consulta em forma de decimal.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Note { get; set; }
    }
}
