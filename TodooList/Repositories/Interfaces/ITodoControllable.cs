using TodooList.Models;

namespace TodooList.Repositories.Interfaces
{
    public interface ITodoControllable
    {
        public ToDo FindTodoById(int id);

        public void RemoveTodo(ToDo toDo);
        public void EditTodo(ToDo toDo);
        public void AddTodo(ToDo toDo);
    }
}
