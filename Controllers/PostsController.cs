using Microsoft.AspNetCore.Mvc;
using Fans.Data;

namespace Fans.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

    }
}
