using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodooList.Data;
using TodooList.Models;
using TodooList.Repositories.Implementations;
using TodooList.Repositories.Interfaces;

namespace TodooList.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDBContext _context;
        private readonly ITodoControllable _todoControllable;
        public TodoController(AppDBContext context, ITodoControllable todoControllable)
        {
            _context = context;
            _todoControllable= todoControllable;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ToDo todo, int id)
        {
            ToDo newTodo = new ToDo()
            {
                IsComplete = todo.IsComplete,
                Description = todo.Description,
                UserId = id
            };
            _todoControllable.AddTodo(newTodo);
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Details", "User", new { id = id });
            }
            else
            {
                return RedirectToAction("AboutUser", "User");
            }
            
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todo = _todoControllable.FindTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            _todoControllable.RemoveTodo(todo);
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Details", "User", new { id = todo.UserId });
            }
            else
            {
                return RedirectToAction("AboutUser", "User");
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todo = _context.ToDo.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(ToDo todo,int id)
        {
            var todos = _todoControllable.FindTodoById(id);
            todos.Description=todo.Description;
            todos.IsComplete = todo.IsComplete;
            //if (ModelState.IsValid)
            {
                _todoControllable.EditTodo(todos);
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Details", "User", new { id = todos.UserId });
                }
                else
                {
                    return RedirectToAction("AboutUser", "User");
                }
            }
            return View();
        }
    }
}
