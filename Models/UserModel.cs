using System.ComponentModel.DataAnnotations;

namespace Fans.Models;

public class User
{
    [Key]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public User(string username, string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
        Username = username;
    }
}