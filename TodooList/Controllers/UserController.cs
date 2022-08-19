using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodooList.Data;

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
            return View(await _context.User.ToListAsync());
        }
    }
}
