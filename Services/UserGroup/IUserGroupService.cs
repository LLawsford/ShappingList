using System.Collections.Generic;
using ShappingList.Entities;

public interface IUserGroupService
{
    IEnumerable<UserGroup> GetAll();
    UserGroup GetById(int id);
    UserGroup Create(UserGroup userGroup);
    void Update(UserGroup userGroup);
    void Delete(int id);

    UserGroup AddItemList(int userGroupId, ItemList itemList);
    UserGroup AddUser(int userGroupId, int userId);
    UserGroup RemoveUser(int userGroupId, int userId);

}