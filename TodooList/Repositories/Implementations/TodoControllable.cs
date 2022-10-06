using TodooList.Models;
using TodooList.Repositories.Interfaces;
using TodooList.Data;

namespace TodooList.Repositories.Implementations
{
    public class TodoControllable : ITodoControllable
    {
        private readonly AppDBContext _context;
        public TodoControllable(AppDBContext context)
        {
            _context = context;
        }

        public void EditTodo(ToDo toDo)
        {
            _context.Update(toDo);
            _context.SaveChanges();
        }

        public ToDo FindTodoById(int id)
        {
            return _context.ToDo.Find(id);
        }

        public void RemoveTodo(ToDo toDo)
        {
            _context.Remove(toDo);
            _context.SaveChanges();
        }
        public void AddTodo(ToDo toDo)
        {
            _context.Add(toDo);
            _context.SaveChanges();
        }
    }
}
