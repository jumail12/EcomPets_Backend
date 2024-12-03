using System.ComponentModel.DataAnnotations;

namespace Pets_Project_Backend.Data.Models.WhishListModel.WhishList_Dto
{
    public class WhishList_Dto
    {
        [Required]
        public int? pro_id { get; set; }
        [Required]
        public int ? user_id { get; set; }
    }
}
