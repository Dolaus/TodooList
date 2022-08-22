using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodooList.Data;
using TodooList.Models;

namespace TodooList.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDBContext _context;
        public UserController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Todo = _context.ToDo.ToList();
            var User = _context.User.ToList();
            return View(await _context.User.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var user = _context.User.Include(u => u.TodoList).ToList().Find(u=>u.Id==id);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Create(User user)
        {
            return View(user);
        }
    }
}
