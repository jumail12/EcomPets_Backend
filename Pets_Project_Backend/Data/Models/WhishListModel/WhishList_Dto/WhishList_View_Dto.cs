namespace Pets_Project_Backend.Data.Models.WhishListModel.WhishList_Dto
{
    public class WhishList_View_Dto
    {
          public int? Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductImage { get; set; }
    }
}
