using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.UserModels.UserDtos
{
    public class UserLogin_Dto
    {
        [Required]
        [EmailAddress]
        public string? UserEmail { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
