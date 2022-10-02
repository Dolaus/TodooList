using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodooList.Data;
using TodooList.Models;

namespace TodooList.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDBContext _context;
        public TodoController(AppDBContext context)
        {
            _context = context;
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
            _context.ToDo.Add(newTodo);
            _context.SaveChanges();
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
            var todo = _context.ToDo.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.ToDo.Remove(todo);
            _context.SaveChanges();
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
        public IActionResult Edit(int? id)
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
            var todos = _context.ToDo.Find(id);
            todos.Description=todo.Description;
            todos.IsComplete = todo.IsComplete;
            //if (ModelState.IsValid)
            {
                _context.ToDo.Update(todos);
                _context.SaveChanges();
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Details", "User", new { id = todo.UserId });
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
