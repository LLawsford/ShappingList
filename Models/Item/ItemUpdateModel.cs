using System.ComponentModel.DataAnnotations;

namespace ShappingList.Models.Item
{
  public class ItemUpdateModel
    {
        [Required]
        public string ItemName { get; set; }
    }
}