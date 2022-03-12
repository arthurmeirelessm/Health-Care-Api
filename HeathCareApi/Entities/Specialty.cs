using System.ComponentModel.DataAnnotations;

namespace HealthCareApi.Entities
{
    public class Specialty : BaseEntity
    {
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

        [RegularExpression(@"^([0-9]{1,3})([.,][0-9]{1,2})?$", ErrorMessage =
        "Digite o preço da consulta em forma de decimal.")]
        public decimal PriceOfConsult { get; set; }
    }
}
