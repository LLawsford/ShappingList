using System.Collections.Generic;
using ShappingList.Entities;

public interface IItemService
{
    Item Create(Item item);

    Item GetById(int id);
    void Update(Item item);
    IEnumerable<Item> GetAll();
    void Delete(int id);
}