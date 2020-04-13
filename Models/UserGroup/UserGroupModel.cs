using System.ComponentModel.DataAnnotations;

namespace ShappingList.Models.UserGroup
{
  public class UserGroupModel
    {
        [Required]
        public string GroupName { get; set; }
    }
}