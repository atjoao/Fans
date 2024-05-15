using System;
using System.Data.Entity;

namespace fans.Models;

public class User {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public class UserDBContext : DbContext {
        public DbSet<User> Users { get; set; }
    }
}