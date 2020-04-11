using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShappingList.Entities;
using ShappingList.Helpers;

namespace ShappingList.Services
{
    public class ItemService : IItemService
    {
        private DataContext _context;
        private readonly AppSettings _appSettings;

        public ItemService(DataContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        
        public Item Create(Item item)
        {
            if(string.IsNullOrWhiteSpace(item.ItemName))
                throw new AppException("Item name is required");

            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

       

        public IEnumerable<Item> GetAll()
        {
            return _context.Items;
        }


        public void Update(Item itemParam)
        {
            var item = _context.Items.Find(itemParam.Id);

            if(item == null)
                throw new AppException("Item not found");

            if(!string.IsNullOrWhiteSpace(itemParam.ItemName))
                item.ItemName = itemParam.ItemName;

            _context.Items.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Items.Find(id);

            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        public Item GetById(int id)
        {

            var item = _context.Items.Where(i => i.Id == id).FirstOrDefault();
            
            if(item == null)
                throw new AppException("No item with this id");

            return item;


        }
    }
}