using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fans.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class HasSession : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if session contains "user"
            if (context.HttpContext.Session.GetString("user") == null)
            {
                // Redirect to Login page if no session
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
