using Newtonsoft.Json;
using Site_SmartComfort.Models;

namespace Site_SmartComfort.Libraries.Login
{
    public class LoginFuncionario
    {
        private string Key = "Login.Funcionario";
        private Sessao.Sessao _sessao;

        public LoginFuncionario(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        // Método que converte o objeto usuario para Json **Serializa**
        public void Login(Funcionario funcionario)
        {
            string funcionarioJSONString = JsonConvert.SerializeObject(funcionario);

            _sessao.Cadastrar(Key, funcionarioJSONString);
        }

        //Reverter Json para objeto usuario ** Deserializar **
        public Funcionario GetFuncionario()
        {
            if (_sessao.Existe(Key))
            {
                string funcionarioJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Funcionario>(funcionarioJSONString);

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
