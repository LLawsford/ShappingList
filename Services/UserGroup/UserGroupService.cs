using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShappingList.Entities;
using ShappingList.Helpers;

namespace ShappingList.Services
{
    public class UserGroupService : IUserGroupService
    {
        private DataContext _context;
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly IItemListService _itemListService;

        public UserGroupService(DataContext context, IOptions<AppSettings> appSettings, IUserService userService, IItemListService itemListService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _userService = userService;
            _itemListService = itemListService;
        }

        public IEnumerable<UserGroup> GetAll()
        {
            return _context.UserGroups.Include(ug => ug.Users).Include(ug => ug.ItemList);
        }

        public UserGroup GetById(int id)
        {
            var group = _context.UserGroups.Where(ug => ug.Id == id).Include(ug => ug.Users).Include(ug => ug.ItemList).FirstOrDefault();

            if (group == null)
                throw new AppException("Group not found");

            return group;
        }

        public UserGroup Create(UserGroup userGroup)
        {
            _context.UserGroups.Add(userGroup);
            _context.SaveChanges();

            return userGroup;
        }

        public void Update(UserGroup userGroup)
        {
            var group = _context.UserGroups.Update(userGroup);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var toRemove = _context.UserGroups.Find(id);
            _context.UserGroups.Remove(toRemove);

            _context.SaveChanges();
        }

        public UserGroup AddItemList(int userGroupId, ItemList itemList)
        {
            var group = _context.UserGroups.Find(userGroupId);
            group.ItemList = itemList;

            _context.SaveChanges();
            return group;
        }

        public UserGroup AddUser(int userGroupId, int userId)
        {
            var group = _context.UserGroups.Find(userGroupId);
            var user = _context.Users.Find(userId);

            if (user == null || group == null)
                throw new AppException("no such user or group");

            //check if list of users exists?? 
            if (group.Users == null)
                group.Users = new List<User>();

            group.Users.Add(user);

            _context.SaveChanges();
            return group;
        }

        public UserGroup RemoveUser(int userGroupId, int userId)
        {
            var group = _context.UserGroups.Find(userGroupId);
            var user = _context.Users.Find(userId);


            if (user == null || group == null)
                throw new AppException("no such user or group");

            group.Users.Remove(user);

            _context.SaveChanges();
            return group;
        }
    }
}