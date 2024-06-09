using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WellVet_v2.Data;
using WellVet_v2.Models;

namespace WellVet_v2.Controllers
{
    public class UsersController : Controller
    {
        private readonly WellVet_v2AppContext _context;
        public static User? user { get; set; }

        public UsersController(WellVet_v2AppContext context)
        {
            _context = context;

        }
        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FirstName,LastName,Email,Password,IsVeterinarian")] User user)
        {
            if (user != null)
            {
                user.UserTypeId = user.IsVeterinarian ? 5 : 3;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Users");
            }
            return View(user);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User loginUser)
        {
            if (loginUser != null)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

                if (user != null)
                {
                    UsersController.user = user;
                    // Oturum işlemleri burada yapılabilir.
                    return RedirectToAction("Index", "Posts",user);
                }
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
            }
            return View(loginUser);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
    }
}
