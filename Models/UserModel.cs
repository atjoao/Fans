using System.ComponentModel.DataAnnotations;

namespace Fans.Models;

public class User(string username, string email, string passwordHash, string ProfileDescription)
{
    [Key]
    [MaxLength(20)]
    public string Username { get; set; } = username;

    [Required]
    public string Email { get; set; } = email;

    [Required]
    public string PasswordHash { get; set; } = passwordHash;

    public string ProfileDescription { get; set; } = ProfileDescription ?? "";
}