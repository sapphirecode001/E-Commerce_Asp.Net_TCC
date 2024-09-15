using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Repository.Contract;
using System.Linq;
using System.Threading.Tasks;

namespace Site_SmartComfort.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        // Injeta o repositório de login no controlador
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        // Ação Index que exibe a página de login
        public IActionResult Index()
        {
            return View();
        }

        // Ação para processar o login
        [HttpPost]
        public IActionResult Login(string EmailUsu, string SenhaUsu)
        {
            // Verifica se o usuário existe no banco de dados
            var usuarios = _loginRepository.Validacao(EmailUsu, SenhaUsu).ToList();

            if (usuarios.Any())
            {
                // Supondo que a autenticação foi bem-sucedida
                // Você pode configurar cookies ou tokens para autenticar o usuário
                // Redireciona para uma página inicial ou dashboard após o login bem-sucedido
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticação falhou
                ViewBag.Message = "Usuário ou senha inválidos. Por favor, tente novamente.";
                return View("Index");
            }
        }
    }
}
