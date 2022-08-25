using Microsoft.AspNetCore.Mvc;
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

            return RedirectToAction("Details", "User",new {id=id});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id==null)
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
            return RedirectToAction("Details", "User",new { id = todo.UserId }) ; 
        }
    }
}
