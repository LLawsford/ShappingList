using System.Collections.Generic;

namespace ShappingList.Entities
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public ICollection<User> Users { get; set; }
        public ItemList ItemList { get; set; }
    }
}