using Fans.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fans.ViewComponents
{
    public class ViewPost : ViewComponent
    {
        private readonly AppDbContext _context;

        public ViewPost(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int PostId)
        {
            var post = await _context.Posts.FindAsync(PostId);
            if (post == null)
            {
                return View("Error");
            }
            return View(post);
        }
    }
}
