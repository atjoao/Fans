using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fans.Attributes
{
    // Implementing IActionFilter interface
    public class HasSession : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if session contains "User"
            if (context.HttpContext.Session.GetString("user") == null)
            {
                // Redirect to Login page if no session
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing after the action executes
        }
    }
}
