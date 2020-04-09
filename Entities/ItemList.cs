using System;
using System.Collections.Generic;

namespace ShappingList.Entities
{
    public class ItemList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public ICollection<Item> Items { get; set; }

    }

    
}