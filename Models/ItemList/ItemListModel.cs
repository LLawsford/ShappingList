using System.ComponentModel.DataAnnotations;

namespace ShappingList.Models.ItemList
{
  public class ItemListModel
    {
        [Required]
        public string ListName { get; set; }
    }
}