using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShappingList.Entities;
using ShappingList.Helpers;

namespace ShappingList.Services
{
    public class ItemListService : IItemListService
    {
        private DataContext _context;
        private readonly AppSettings _appSettings;
        private readonly IItemService _itemService;

        public ItemListService(DataContext context, IOptions<AppSettings> appSettings, IItemService itemService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _itemService = itemService;
        }

        
        public ItemList Create(ItemList itemList)
        {
            if(string.IsNullOrWhiteSpace(itemList.ListName))
                throw new AppException("Item list name is required");

            _context.ItemLists.Add(itemList);
            _context.SaveChanges();
            return itemList;
        }

        public IEnumerable<ItemList> GetAll()
        {
            return _context.ItemLists.Include(il => il.Items);
        }


        public void Update(ItemList itemListParam)
        {
            var itemList = _context.ItemLists.Find(itemListParam.Id);

            if(itemList == null)
                throw new AppException("Item not found");

            if(!string.IsNullOrWhiteSpace(itemListParam.ListName))
                itemList.ListName = itemListParam.ListName;

            _context.ItemLists.Update(itemList);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var itemList = _context.ItemLists.Find(id);

            _context.ItemLists.Remove(itemList);
            _context.SaveChanges();
        }

        public ItemList GetById(int id)
        {
            var itemList = _context.ItemLists.Where(il => il.Id == id).Include(il => il.Items).FirstOrDefault();

            if(itemList == null)
                throw new AppException("no item list with this id");

            return itemList;
        }

    }
}