using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Models; 
using Site_SmartComfort.Repository.Contract;
using Site_SmartComfort.ViewModels;
using System.Linq;

namespace Site_SmartComfort.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        // Injeta o repositório de usuário no controlador
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Ação Index que exibe a página de login
        public IActionResult Login()
        {
            return View();
        }

        // Ação para processar o login
        [HttpPost]
        public IActionResult Login(string emailUsu, string senhaUsu)
        {
            // Verifica se o usuário existe no banco de dados
            var usuarios = _usuarioRepository.Validacao(emailUsu, senhaUsu).ToList();

            if (usuarios.Any())
            {
                // Autenticação bem-sucedida
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticação falhou
                ViewBag.Message = "Usuário ou senha inválidos. Por favor, tente novamente.";
                return View();
            }
        }

        // Ação para exibir a página de cadastro de Pessoa Física
        public IActionResult CadastrarPF()
        {
            return View();
        }

        // Ação para processar o cadastro de um novo usuário Pessoa Física
        [HttpPost]
        public IActionResult CadastrarPF(CadastroPFViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verifica se o usuário já existe
                if (_usuarioRepository.UsuarioExiste(model.EmailUsu))
                {
                    ViewBag.Message = "Este e-mail já está cadastrado. Tente outro.";
                    return View(model); // Retorna o modelo com erro
                }

                // Criação de instâncias das classes PF e Usuario
                var usuario = new Usuario
                {
                    EmailUsu = model.EmailUsu,
                    TelefoneUsu1 = model.TelefoneUsu1,
                    TelefoneUsu2 = model.TelefoneUsu2,
                    SenhaUsu = model.SenhaUsu
                };

                var pf = new PF
                {
                    NomeCompleto = model.NomeCompleto,
                    Cpf = model.Cpf,
                    // O IdUsu será definido após o cadastro do usuário
                };

                // Salva o usuário e a pessoa física no banco de dados
                _usuarioRepository.CadastrarPessoaFisica(pf, usuario);

                return RedirectToAction("Index", "Home"); // Redireciona após o cadastro bem-sucedido
            }

            // Se o modelo não for válido, retorne a mesma view para mostrar os erros
            return View(model);
        }

        // Ação para exibir a página de cadastro de Pessoa Jurídica
        public IActionResult CadastrarPJ()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }

        // Ação para processar o cadastro de um novo usuário Pessoa Jurídica
        [HttpPost]
        public IActionResult CadastrarPJ(CadastroPJViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verifica se o usuário já existe
                if (_usuarioRepository.UsuarioExiste(model.EmailUsu))
                {
                    ViewBag.Message = "Este e-mail já está cadastrado. Tente outro.";
                    return View(model); // Retorna o modelo com erro
                }

                // Criação de instâncias das classes PJ e Usuario
                var usuario = new Usuario
                {
                    EmailUsu = model.EmailUsu,
                    TelefoneUsu1 = model.TelefoneUsu1,
                    TelefoneUsu2 = model.TelefoneUsu2,
                    SenhaUsu = model.SenhaUsu
                };

                var pj = new PJ
                {
                    Cnpj = model.Cnpj,
                    RazaoSocial = model.RazaoSocial,
                    NomeResponsavel = model.NomeResponsavel,
                };

                // Salva o usuário e a pessoa jurídica no banco de dados
                _usuarioRepository.CadastrarPessoaJuridica(pj, usuario);

                return RedirectToAction("Index", "Home"); // Redireciona após o cadastro bem-sucedido
            }

            // Se o modelo não for válido, retorne a mesma view para mostrar os erros
            return View(model);
        }
    }
}
