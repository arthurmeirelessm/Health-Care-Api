using HealthCareApi.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApi.Dto.User
{
    public class UserRequestUpdate : UserRequest
    {
        public string CurrentPassword { get; set; }

    }
}
