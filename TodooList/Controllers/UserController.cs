using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodooList.Data;
using TodooList.Models;
using TodooList.Models.ViewModels;
using TodooList.Repositories.Interfaces;
using TodooList.Services.Interface;

namespace TodooList.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserControllable _userControllable;
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IFiltrator<User> _filtrator;
        private readonly IPaginator<User> _paginator;
        public UserController(IUserControllable userControllable,AppDBContext context, IWebHostEnvironment webHostEnviroment, IFiltrator<User> filtrator, IPaginator<User> paginator)
        {
            _webHostEnviroment = webHostEnviroment;
            _context = context;
            _filtrator = filtrator;
            _paginator = paginator;
            _userControllable = userControllable;
        }

        public async Task<IActionResult> Index(string searchstring, SortState sortState, int page = 1)
        {
            IQueryable<User> source = _context.User.Include(i => i.TodoList);

            source = _filtrator.Filter(source, searchstring);

            ViewData["NameSort"] = sortState == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["YearSort"] = sortState == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;

            source = sortState switch
            {
                SortState.NameAsc => source.OrderBy(s => s.Name),
                SortState.NameDesc => source.OrderByDescending(s => s.Name),
                SortState.YearDesc => source.OrderByDescending(s => s.Year),
                SortState.YearAsc => source.OrderBy(s => s.Year),
                _ => source.OrderBy(s => s.Id)
            };
            return View(await _paginator.Pagination(5, source, page));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _userControllable.FindUserById(id);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateUserViewModel model = new CreateUserViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var fileimages = HttpContext.Request.Form.Files;
                if (fileimages.Count > 0)
                {
                    var webpath = _webHostEnviroment.WebRootPath;
                    string upload = webpath + URL.ImageUserURL;
                    string imageName = Guid.NewGuid().ToString();
                    string imageextension = Path.GetExtension(fileimages[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                    {
                        fileimages[0].CopyTo(fileStream);
                    }
                    model.Image = imageName + imageextension;
                }
                Role role = _context.Role.FirstOrDefault(u => u.Name == "user");
                if (role != null)
                {
                    User user = new User();
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Year = model.Year;
                    user.Role = role;
                    user.Id = model.Id;
                    user.Password = model.Password;

                    _userControllable.AddUser(user);
                }
                return RedirectToAction("Index");
            }
            else { return View(); }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, string? email)
        {
            var user = _userControllable.FindUserById(id);
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
            var webpath = _webHostEnviroment.WebRootPath;
            string upload = webpath + URL.ImageUserURL;

            if (user.Image != null)
            {
                var oldFile = Path.Combine(upload, user.Image);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
            }
            _userControllable.RemoveUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id, string? email)
        {
            if (id == null&& email == null || id == 0)
            {
                return NotFound();
            }
            User user = new User();
            if (id!=null)
            {
                user = _context.User.Find(id);
            }
            if (email != null)
            {
                user = _userControllable.FindUserByEmail(email);
            }

            if (user == null)
            {
                return NotFound();
            }
            var model = new EditUserViewModel()
            {
                Id = user.Id,
                Image = user.Image,
                Name = user.Name,
                Year = user.Year
            };

            return View(model);
        }
      
        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            var objFromdb = _context.User.AsNoTracking().FirstOrDefault(u => u.Id == model.Id);
            if (objFromdb == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var fileimages = HttpContext.Request.Form.Files;
                if (fileimages.Count > 0)
                {
                    var webpath = _webHostEnviroment.WebRootPath;
                    string upload = webpath + URL.ImageUserURL;
                    string imageName = Guid.NewGuid().ToString();
                    string imageextension = Path.GetExtension(fileimages[0].FileName);

                    if (objFromdb.Image != null)
                    {
                        var oldFile = Path.Combine(upload, objFromdb.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                            using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                            {
                                fileimages[0].CopyTo(fileStream);
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(upload, imageName + imageextension), FileMode.Create))
                        {
                            fileimages[0].CopyTo(fileStream);
                        }
                    }
                    model.Image = imageName + imageextension;
                }
                User qwer = _context.User.Find(model.Id);
                qwer.Image = model.Image;
                qwer.Year = model.Year;
                qwer.Name = model.Name;

                _userControllable.UpdateUser(qwer);
                return RedirectToAction("index");
            }
            return View();
        }
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public IActionResult AboutUser()
        {
            User user = _context.User.Include(u => u.TodoList).FirstOrDefault(u=> u.Email == User.Identity.Name);
            
            if (user!=null)
            {
                return View(user);
            }

            return RedirectToAction("Index");
        }
    }
}
