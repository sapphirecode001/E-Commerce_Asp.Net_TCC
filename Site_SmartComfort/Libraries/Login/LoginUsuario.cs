using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.CRUD;
using Newtonsoft.Json;
using Site_SmartComfort.Models;

namespace Site_SmartComfort.Libraries.Login
{
    public class LoginUsuario
    {
        private string Key = "Login.Usuario";
        private Sessao.Sessao _sessao;

        public LoginUsuario(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        // Método que converte o objeto usuario para Json **Serializa**
        public void Login(Usuario usuario)
        {
            string usuarioJSONString = JsonConvert.SerializeObject(usuario);

            _sessao.Cadastrar(Key, usuarioJSONString);
        }

        //Reverter Json para objeto usuario ** Deserializar **
        public Usuario GetUsuario()
        {
            if (_sessao.Existe(Key))
            {
                string usuarioJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Usuario>(usuarioJSONString);

            } 
            else
            {
                return null;
            }
        }

        //remover sessao, desloga o usuario
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
