using HealthCareApi.Enuns;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApi.Entities
{
    public class User : BaseEntity
    {
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
          "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(70, MinimumLength = 2, ErrorMessage =
           "O Primeiro nome deve ter no mínimo 2 e no máximo 70 caracteres.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
          "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(70, MinimumLength = 2, ErrorMessage =
           "O Segundo nome deve ter no mínimo 2 e no máximo 70 caracteres.")]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-z0-9.]+@[gmail|outlook|hotmail]+\.[a-z]+", ErrorMessage =
          "Não padronização e caracteres especiais não são permitidos no campo de email.")]
        [StringLength(70, MinimumLength = 10, ErrorMessage =
           "O Email deve ter no mínimo 10 e no máximo 70 caracteres.")]
        public string Email { get; set; }

        [Range(1, 100, ErrorMessage =
          "A idade deve ser entre 7 e 100 anos.")]
        public int Age { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage =
         "Não padronização e caracteres especiais não são permitidos no campo de email.")]
        [StringLength(15, MinimumLength = 7, ErrorMessage =
          "O UserName deve ter no mínimo 7 e no máximo 15 caracteres.")]
        public string UserName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,40}$", ErrorMessage =
         "Não padronização e caracteres especiais não são permitidos no campo de email.")]
        [StringLength(15, MinimumLength = 7, ErrorMessage =
          "O Email deve ter no mínimo 7 e no máximo 15 caracteres.")]
        public string Password { get; set; }
        public TypeUser TypeUser { get; set; }
    }
}
