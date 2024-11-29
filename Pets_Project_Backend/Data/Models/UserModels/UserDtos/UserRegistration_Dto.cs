using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.UserModels.UserDtos
{
    public class UserRegistration_Dto
    {
        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(30, ErrorMessage = "Name should not execeed 30 character!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!.")]
        [EmailAddress(ErrorMessage = "Invalid email address!.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required!.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!.")]
        public string? Password { get; set; }
    }
}
