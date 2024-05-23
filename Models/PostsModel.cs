using System.ComponentModel.DataAnnotations;
using Fans.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Fans.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; private set; }

        [Required]
        public required User User { get; set; }

        [Required]
        [StringLength(280)]
        public required string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime EditedAt { get; set; }

        public static int globalPostId;

        public Posts() { }

        public Posts(User user, string content)
        {
            User = user;
            Content = content;
            PostId = Interlocked.Increment(ref globalPostId);
            CreatedAt = DateTime.Now;
        }
    }
}