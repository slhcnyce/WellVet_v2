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

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            if (comment != null)
            {


                // Kullanıcıyı veritabanında bul
                var user = UsersController.user;

                if (user != null)
                {
                    // Kullanıcı bulundu, gönderi bilgilerini kaydet
                    comment.UserId = user.Id;
                    _context.Add(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Kullanıcı bulunamadı, hata ekleyin
                    ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                }
            }

            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> Comments([FromQuery] int postId)
        {
            var comments = await _context.Comments.Include(x => x.User).Where(x => x.PostId == postId && !x.IsDeleted).ToListAsync();

            return Ok(comments);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int commentId)
        {

            var currentCommnet = await _context.Comments.Where(x => x.Id == commentId).FirstOrDefaultAsync();

            if (currentCommnet != null)
            {
                currentCommnet.IsDeleted = true;
                _context.Update(currentCommnet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int postId)
        {

            var currentCommnet = await _context.Posts.Where(x => x.Id == postId).FirstOrDefaultAsync();

            if (currentCommnet != null)
            {
                _context.Remove(currentCommnet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View();
        }

    }
}
