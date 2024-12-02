using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Libraries.Filtro;
using Site_SmartComfort.Libraries.Login;
using Site_SmartComfort.Models;

namespace Site_SmartComfort.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class HomeController : Controller
    {
        private IFuncionarioRepository _repositoryFuncionario;
        private LoginFuncionario _loginFuncionario;

        public HomeController(IFuncionarioRepository repositoryFuncionario, LoginFuncionario loginFuncionario)
        {
            _repositoryFuncionario = repositoryFuncionario;
            _loginFuncionario = loginFuncionario;
        }

        [FuncionarioAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Login([FromForm] Models.Funcionario funcionario)
        {
            Models.Funcionario funcionarioDB = _repositoryFuncionario.Login(funcionario.EmailFunc, funcionario.SenhaFunc);


            if (funcionarioDB.EmailFunc != null && funcionarioDB.SenhaFunc != null)
            {
                _loginFuncionario.Login(funcionarioDB);

                return RedirectToAction("Produto", "Index");
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";
                return View();
            }

        }
        
        [HttpPost]
        public IActionResult Login1([FromForm] Models.Funcionario funcionario)
        {
            if (funcionario == null || string.IsNullOrEmpty(funcionario.EmailFunc) || string.IsNullOrEmpty(funcionario.SenhaFunc))
            {
                TempData["ErrorMessage"] = "Email ou senha não informados.";
                return RedirectToAction("Login");
            }

            // Chama o repositório para verificar as credenciais do usuário
            var funcionarioLogado = _repositoryFuncionario.Login(funcionario.EmailFunc, funcionario.SenhaFunc);

            if (funcionarioLogado == null)
            {
                TempData["ErrorMessage"] = "Credenciais inválidas. Tente novamente.";
                return RedirectToAction("Login");
            }

            // Armazena o usuário na sessão usando a classe LoginUsuario
            _loginFuncionario.Login(funcionarioLogado);  // Armazena o usuário logado

            // Redireciona para o Painel do Usuário após o login bem-sucedido
            return RedirectToAction(nameof(Painel));
        }

        [FuncionarioAutorizacao]
        public IActionResult Painel()
        {
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
