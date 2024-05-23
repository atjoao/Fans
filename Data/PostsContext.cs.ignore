using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fans.Models;

namespace Fans.Data
{
    public class PostsContext : DbContext
    {
        public PostsContext (DbContextOptions<PostsContext> options)
            : base(options)
        {
        }

        public DbSet<Fans.Models.Posts> Posts { get; set; } = default!;
    }
}
