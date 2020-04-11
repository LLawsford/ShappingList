using System.Collections.Generic;
using ShappingList.Entities;

public interface IItemListService
{
    ItemList Create(ItemList itemList);
    void Update(ItemList itemList);
    IEnumerable<ItemList> GetAll();
    ItemList GetById(int id);
    void Delete(int id);


}