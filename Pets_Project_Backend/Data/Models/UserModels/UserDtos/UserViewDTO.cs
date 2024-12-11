namespace Pets_Project_Backend.Data.Models.UserModels.UserDtos
{
    public class UserViewDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Role { get; set; }
        public bool? isBlocked { get; set; }
    }
}
