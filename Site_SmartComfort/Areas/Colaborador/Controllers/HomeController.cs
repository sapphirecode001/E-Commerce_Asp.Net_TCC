using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Libraries.Filtro;
using Site_SmartComfort.Libraries.Login;

namespace Site_SmartComfort.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IFuncionarioRepository _repositoryFuncionario;
        private LoginFuncionario _loginFuncionario;

        public HomeController(IFuncionarioRepository repositoryFuncionario, LoginFuncionario loginFuncionario)
        {
            _repositoryFuncionario = repositoryFuncionario;
            _loginFuncionario = loginFuncionario;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateHttpReferer]
        public IActionResult Login([FromForm] Models.Funcionario colaborador)
        {
            Models.Funcionario colaboradorDB = _repositoryFuncionario.Login(colaborador.EmailFunc, colaborador.SenhaFunc);

            if (colaboradorDB.EmailFunc != null && colaboradorDB.SenhaFunc != null)
            {
                _loginFuncionario.Login(colaboradorDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";
                return View();
            }
        }

        [FuncionarioAutorizacao]
        public IActionResult Painel()
        {
            ViewBag.Nome = _loginFuncionario.GetFuncionario().NomeFunc;
            //    ViewBag.Tipo = _loginColaborador.GetColaborador().Tipo;
            ViewBag.Email = _loginFuncionario.GetFuncionario().EmailFunc;
            return View();
        }

        [FuncionarioAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginFuncionario.Logout();
            return RedirectToAction(nameof(Login));

        }

    }
}
