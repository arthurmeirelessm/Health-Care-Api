using System.ComponentModel.DataAnnotations;

namespace HealthCareApi.Dto.User
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
