using TodooList.Models;
namespace TodooList.Repositories.Interfaces
{
    public interface IUserControllable
    {
        User FindUserById(int? id);
        User FindUserByEmail(string? email);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
    }
}
