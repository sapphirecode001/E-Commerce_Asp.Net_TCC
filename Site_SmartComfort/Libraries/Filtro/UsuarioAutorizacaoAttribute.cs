using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySqlX.XDevAPI;
using Site_SmartComfort.Libraries.Login;
using Site_SmartComfort.Models;

namespace Site_SmartComfort.Libraries.Filtro
{
    public class UsuarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginUsuario _loginUsuario;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginUsuario = (LoginUsuario)context.HttpContext.RequestServices.GetService(typeof(LoginUsuario));
            Usuario usuario = _loginUsuario.GetUsuario();
            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }
    }
}
