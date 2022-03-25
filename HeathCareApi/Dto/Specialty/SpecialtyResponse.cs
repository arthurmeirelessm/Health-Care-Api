using System.ComponentModel.DataAnnotations;

namespace HealthCareApi.Dto.Specialty
{
    public class SpecialtyResponse
    {

        public int Id { get; set; }
        public int DoctorId { get; set; }

        [RegularExpression(@"^Cardiologia|Ginecologia|Cirurgia Geral|Pediatria|Clínica Geral|Urologia|Otorrinolaringologia|Ortopedia", ErrorMessage =
         "Tente digitar a especialidade começando com letra maiúscula ou escolher uma dessas especialidades disponíveis em nosso banco: " +
            "Cardiologia, " +
            "Ginecologia, " +
            "Cirurgia Geral, " +
            "Pediatria, " +
            "Clínica Geral, " +
            "Urologia, " +
            "Otorrinolaringologia, " +
            "Ortopedia.")]
        [StringLength(70, MinimumLength = 2, ErrorMessage =
          "O Primeiro nome deve ter no mínimo 2 e no máximo 70 caracteres.")]
        public string NameForSpecialty { get; set; }

        [RegularExpressionAttribute(@"^([0-9]{1,3})([.,][0-9]{1,2})?$", ErrorMessage =
        "Digite o preço da consulta em forma de decimal.")]
        public decimal PriceOfConsult { get; set; }
        public Entities.User Doctor { get; set; }
        public DateTime CreatedId { get; set; }
        public DateTime UpdateAt { get; set; }
        public ICollection<Entities.User> Patients { get; set; }

    }
}
