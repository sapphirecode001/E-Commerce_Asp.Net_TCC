using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Libraries.Login;

namespace Site_SmartComfort.Libraries.Filtro
{
    public class FuncionarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginFuncionario _loginFuncionario;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginFuncionario = (LoginFuncionario)context.HttpContext.RequestServices.GetService(typeof(LoginFuncionario));
            Models.Funcionario funcionario = _loginFuncionario.GetFuncionario();
            if (funcionario == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }
    }
}
