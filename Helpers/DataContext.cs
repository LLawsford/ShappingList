using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShappingList.Entities;

namespace ShappingList.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("ShappingListDatabase"));

        }

       

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemList> ItemLists  { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
    }
}