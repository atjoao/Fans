using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace fans.Controllers;

public class Login : Controller {
    public IActionResult Index() {
        return View("Login");
    }
}