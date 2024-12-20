﻿using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Libraries.Filtro;
using Site_SmartComfort.Libraries.Login;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Linq;

namespace Site_SmartComfort.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private LoginUsuario _loginUsuario;

        // Injeta o repositório de usuário e a classe de gerenciamento de sessão
        public UsuarioController(IUsuarioRepository usuarioRepository, LoginUsuario loginUsuario)
        {
            _usuarioRepository = usuarioRepository;
            _loginUsuario = loginUsuario;
        }

        public IActionResult CadastrarPF()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPF(Usuario usuario)
        {
            _usuarioRepository.CadastrarUsuarioPF(usuario);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult CadastrarPJ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPJ(Usuario usuario)
        {
            _usuarioRepository.CadastrarUsuarioPJ(usuario);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult EsqueceuSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.EmailUsu) || string.IsNullOrEmpty(usuario.SenhaUsu))
            {
                TempData["ErrorMessage"] = "Email ou senha não informados.";
                return RedirectToAction("Login");
            }

            // Chama o repositório para verificar as credenciais do usuário
            var usuarioLogado = _usuarioRepository.LoginUsuario(usuario.EmailUsu, usuario.SenhaUsu);

            if (usuarioLogado == null)
            {
                TempData["ErrorMessage"] = "Credenciais inválidas. Tente novamente.";
                return RedirectToAction("Login");
            }

            // Armazena o usuário na sessão usando a classe LoginUsuario
            _loginUsuario.Login(usuarioLogado);  // Armazena o usuário logado

            // Redireciona para o Painel do Usuário após o login bem-sucedido
            return RedirectToAction(nameof(PainelUsuario));
        }

        // Ação de Painel de Usuário - Exibe o painel do usuário logado
        [HttpGet]

        [UsuarioAutorizacao]
        public IActionResult PainelUsuario()
        {
            return View();  // Passa o usuário para a view
        }

        [HttpPost]
        public IActionResult AlterarDados(Usuario usuario)
        {
            // Obtém o usuário autenticado
            var usuarioAutenticado = _loginUsuario.GetUsuario();

            // Atribui os valores do usuário autenticado ao objeto que veio do formulário
            usuario.IdPF = usuarioAutenticado.IdPF;
            usuario.IdPJ = usuarioAutenticado.IdPJ;
            usuario.IdUsu = usuarioAutenticado.IdUsu;
                
            _usuarioRepository.AtualizarUsuario(usuario);

            var usuarioAtualizado = _usuarioRepository.ObterUsuario(usuario.IdUsu);

            _loginUsuario.Logout();
            _loginUsuario.Login(usuarioAtualizado);
            

            return View("PainelUsuario", usuarioAtualizado);

        }

        [UsuarioAutorizacao]
        public IActionResult LogoutUsuario()
        {
            // Limpa a sessão do usuário
            _loginUsuario.Logout();

            // Redireciona para a página de login
            return RedirectToAction(nameof(Login));
        }
    }
}
