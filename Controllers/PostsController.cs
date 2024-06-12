using Microsoft.AspNetCore.Mvc;
using Fans.Data;
using Fans.Attributes;

namespace Fans.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /posts
        [HasSession]
        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        [HasSession]
        public IActionResult Create()
        {
            return View();
        }

    }
}
