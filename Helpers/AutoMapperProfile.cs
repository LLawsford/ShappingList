using AutoMapper;
using ShappingList.Entities;
using ShappingList.Models.Item;
using ShappingList.Models.User;

namespace ShappingList.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //? later on these maps should be splitted into separate files.

            //user maps
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UserUpdateModel, User>();

            //item maps
            CreateMap<Item, ItemModel>().ReverseMap();
            CreateMap<ItemUpdateModel, Item>();
        }
    }
}