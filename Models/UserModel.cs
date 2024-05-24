using System.ComponentModel.DataAnnotations;

namespace Fans.Models;

public class User(string username, string email, string passwordHash, string desc)
{
    [Key]
    public string Username { get; set; } = username;

    [Required]
    public string Email { get; set; } = email;

    [Required]
    public string PasswordHash { get; set; } = passwordHash;

    public string Desc { get; set; } = desc ?? "Descrição de utilizador";
}