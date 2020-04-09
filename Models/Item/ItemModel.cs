using System.ComponentModel.DataAnnotations;

namespace ShappingList.Models.Item
{
  public class ItemModel
    {
        [Required]
        public string ItemName { get; set; }
        
    }
}