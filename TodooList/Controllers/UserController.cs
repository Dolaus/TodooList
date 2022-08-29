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

        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 3;
            IQueryable<User> source= _context.User.Include(i => i.TodoList);
            var count= await source.CountAsync();
            var items= await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };
            return View(viewModel);

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
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else { return View(); } 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _context.User.Include(i=>i.TodoList).FirstOrDefault(u=>u.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.User.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id==null||id==0)
            {
               return NotFound();
            }
            var user = _context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _context.User.Update(user);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
