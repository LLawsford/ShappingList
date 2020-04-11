using System.ComponentModel.DataAnnotations;

namespace ShappingList.Models.ItemList
{
  public class ItemListUpdateModel
    {
        [Required]
        public string ListName { get; set; }
    }
}