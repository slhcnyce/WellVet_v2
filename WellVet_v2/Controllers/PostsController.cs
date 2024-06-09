using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WellVet_v2.Data;
using WellVet_v2.Models;

namespace WellVet_v2.Controllers
{
    public class PostsController : Controller
    {
        private readonly WellVet_v2AppContext _context;

        public PostsController(WellVet_v2AppContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts.Include(p => p.User).ToListAsync();
            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content")] Post post)
        {
            if (post != null)
            {


                // Kullanıcıyı veritabanında bul
                var user = UsersController.user;

                if (user != null)
                {
                    // Kullanıcı bulundu, gönderi bilgilerini kaydet
                    post.UserId = user.Id;
                    post.CreatedAt = DateTime.Now;
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Kullanıcı bulunamadı, hata ekleyin
                    ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                }
            }
            return View(post);
        }

    }
}
