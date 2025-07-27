using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Proyecto.UI.Models
{
    public class AuthorizeSession : ActionFilterAttribute
    {
        public string Rol { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rolEnSesion = context.HttpContext.Session.GetString("Rol");

            if (string.IsNullOrEmpty(rolEnSesion) || rolEnSesion != Rol)
            {
                context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
            }
        }
    }
}
