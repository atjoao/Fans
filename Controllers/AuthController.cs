using Microsoft.AspNetCore.Mvc;
using Fans.Data;
using Fans.Models;
using NuGet.Protocol;
using System.Text.Json;

namespace Fans.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserContext _context;

        public AuthController(UserContext context)
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
        public ActionResult RegisterProc(
            string username,
            string email,
            string password
        )
        {
            var session = this.HttpContext.Session;
            try
            {
                if (email == null || password == null || email == "" || password == "" || username == "" || username == null)
                {
                    session.SetString("error", "Os campos não podem estar vazios.");
                    session.CommitAsync();

                    return RedirectToAction("Register", "Auth");
                }

                // check if session exists
                if (session.GetString("user") != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                // check if email and username are existent
                var db = _context;
                var _user = db.User.Where(u => u.Email == email).FirstOrDefault();
                if (_user != null)
                {
                    session.SetString("error", "Este email já foi registado");
                    session.CommitAsync();

                    return RedirectToAction("Register", "Auth");
                }

                _user = db.User.Where(u => u.Username == username).FirstOrDefault();
                if (_user != null)
                {
                    session.SetString("error", "Já existe um utilizador com este nome");
                    session.CommitAsync();

                    return RedirectToAction("Register", "Auth");
                }

                var _password = BCrypt.Net.BCrypt.HashPassword(password).ToString();

                // this most likely will stop the thread from working idk
                var user = new User(username, email, _password);
                db.User.Add(user);
                // deficiencia...
                db.SaveChanges();

                // create session // this needs to be parsed later aaaa
                // have to find better way to create session array
                // ill keep this for now
                session.SetString("user", JsonSerializer.Serialize(user));
                session.CommitAsync();


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new BadHttpRequestException("Não consegui completar o processo: " + ex.Message);
            }
        }


        // POST: Auth/LoginProc
        [HttpPost]
        public ActionResult LoginProc(
            string email, string password
        )
        {
            var session = HttpContext.Session;
            try
            {
                if (email == null || password == null || email == "" || password == "")
                {
                    session.SetString("error", "Os campos não podem estar vazios.");
                    session.CommitAsync();

                    return RedirectToAction("Login", "Auth");
                }

                // check if session exists
                if (session.GetString("email") != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                // check if email and password are correct
                var db = _context;
                var user = db.User.Where(u => u.Email == email).FirstOrDefault();
                if (user == null)
                {
                    session.SetString("error", "Combinação de password ou email invalidos.");
                    session.CommitAsync();

                    return RedirectToAction("Login", "Auth");
                }

                var verify = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                if (!verify)
                {
                    session.SetString("error", "Combinação de password ou email invalidos.");
                    session.CommitAsync();

                    return RedirectToAction("Login", "Auth");
                }

                // set session
                // nice static typing lmao
                user = new User(user.Username, user.Email, user.PasswordHash);
                // this will be a pain ita 
                session.SetString("user", JsonSerializer.Serialize(user));
                session.CommitAsync();

                return RedirectToAction("Index", "Home");

            }
            catch (System.Exception)
            {
                // throw bad request
                throw new BadHttpRequestException("Não consegui completar o processo");
            }
        }

    }
}
