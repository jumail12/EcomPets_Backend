namespace Pets_Project_Backend.Data.Models.AddressModel.Address_Dtos
{
    public class GetAddress_dto
    {
        public int? AddressId { get; set; }
        public string? CustomerName { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? HomeAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PostalCode { get; set; }
    }
}
