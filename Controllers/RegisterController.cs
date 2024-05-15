using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace fans.Controllers;

public class Register : Controller {
    public IActionResult Index(){
        return View("Register");
    }
}