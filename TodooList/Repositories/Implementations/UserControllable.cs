using Microsoft.EntityFrameworkCore;
using TodooList.Data;
using TodooList.Models;
using TodooList.Repositories.Interfaces;

namespace TodooList.Repositories.Implementations
{
    public class UserControllable: IUserControllable
    {
        private readonly AppDBContext _context;
        public UserControllable(AppDBContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
           _context.User.Add(user);
            _context.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }

        public User FindUserById(int? id)
        {
            return _context.User.Include(u => u.TodoList).ToList().Find(u => u.Id == id);
        }

        public void UpdateUser(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public User FindUserByEmail(string? email)
        {
            return _context.User.FirstOrDefault(u => u.Email == email);
        }
    }
}
