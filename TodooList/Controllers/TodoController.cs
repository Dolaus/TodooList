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
        public IActionResult Create(ToDo todo,int id)
        {
            User user = _context.User.Find(id);
            ToDo newTodo2= new ToDo()
            {
                IsComplete = todo.IsComplete,
                Description = todo.Description,
                User=user
            };
            _context.ToDo.Add(newTodo2);
            _context.SaveChanges();

            return RedirectToAction("Index","User");
        }
    }
}
