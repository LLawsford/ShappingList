using System.Collections.Generic;
using ShappingList.Entities;

public interface IItemService
{
    Item Create(Item item);
    void Update(Item item);
    IEnumerable<Item> GetAll();
    void Delete(int id);
}