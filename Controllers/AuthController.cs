using Microsoft.AspNetCore.Mvc;
using Fans.Data;
using Fans.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fans.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login/Login");
        }

        // GET: Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login", "Auth");
        }

        // GET: Auth/Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Register/Register");
        }

        // POST: Auth/Register
        [HttpPost]
        public async Task<IActionResult> RegisterProc(string username, string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
            {
                HttpContext.Session.SetString("error", "Os campos não podem estar vazios.");
                return RedirectToAction("Register", "Auth");
            }

            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var existingUserByEmail = await _context.Users.FindAsync(email);
            if (existingUserByEmail != null)
            {
                HttpContext.Session.SetString("error", "Este email já foi registado");
                return RedirectToAction("Register", "Auth");
            }

            var existingUserByUsername = _context.Users.SingleOrDefault(u => u.Username == username);
            if (existingUserByUsername != null)
            {
                HttpContext.Session.SetString("error", "Já existe um utilizador com este nome");
                return RedirectToAction("Register", "Auth");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(username, email, hashedPassword, "");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
            return RedirectToAction("Index", "Home");
        }

        // POST: Auth/LoginProc
        [HttpPost]
        public async Task<IActionResult> LoginProc(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                HttpContext.Session.SetString("error", "Os campos não podem estar vazios.");
                return RedirectToAction("Login", "Auth");
            }

            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _context.Users.FindAsync(username);
            if (user == null)
            {
                HttpContext.Session.SetString("error", "Utilizador não existe.");
                return RedirectToAction("Login", "Auth");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                HttpContext.Session.SetString("error", "Combinação de password ou email inválidos.");
                return RedirectToAction("Login", "Auth");
            }

            HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
            return RedirectToAction("Index", "Home");
        }
    }
}
