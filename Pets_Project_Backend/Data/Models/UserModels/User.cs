using Pets_Project_Backend.Data.Models.CartModel;
using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.UserModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required!")]
        [MaxLength(30,ErrorMessage ="Name shold not execeed 30 charecters!")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="Email is required!")]
        [EmailAddress(ErrorMessage ="Invalid email address!")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        [MinLength(8,ErrorMessage ="Password must be at least 8 characters long!")]
        public string? Password { get; set; }

        public string? Role { get; set; }
        public bool? isBlocked { get; set; }

        //nav
        public virtual Cart? _Cart { get; set; }


        //nav properties
    }
}
