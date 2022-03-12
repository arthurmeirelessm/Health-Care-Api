using System.ComponentModel.DataAnnotations;

namespace HealthCareApi.Entities
{
    public class NoteForMedicalCare : BaseEntity
    {
        [RegularExpression(@"^([0-9]{1,2})([.,][0-9]{1})?$", ErrorMessage =
       "Digite o preço da consulta em forma de decimal.")]
        public decimal Note { get; set; }

    }
}
