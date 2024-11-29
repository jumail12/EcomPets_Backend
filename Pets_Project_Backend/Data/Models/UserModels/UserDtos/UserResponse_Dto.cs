namespace Pets_Project_Backend.Data.Models.UserModels.UserDtos
{
    public class UserResponse_Dto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public string? UserEmail { get; set; }
        public string? Role { get; set; }
        public string? Error { get; set; }
    }
}
