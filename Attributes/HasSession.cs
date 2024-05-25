using System.Text.Json;
using Fans.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fans.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class HasSession : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetString("user");
            if ( user == null)
            {
                // Redirect to Login page if no session
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
            // add user to ViewData (or HttpContext.Items yk)
            else
            {
                var json_user = JsonSerializer.Deserialize<User>(user);
                context.HttpContext.Items["user"] = json_user;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
