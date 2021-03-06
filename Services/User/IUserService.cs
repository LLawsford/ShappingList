using System.Collections.Generic;
using ShappingList.Entities;

public interface IUserService
{
    User Authenticate(string username, string password);
    IEnumerable<User> GetAll();
    User GetById(int id);
    User Create(User user, string password);
    void Update(User user, string password = null);
    void Delete(int id);

    void AcceptInvitation(int invitationId);
    void DeclineInvitation(int invitationId);
    IEnumerable<Invitation> ShowAllInvitations(int userId);
}