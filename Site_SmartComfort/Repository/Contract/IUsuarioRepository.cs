using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Validacao(string EmailUsu, string SenhaUsu);
        void CadastrarPessoaFisica(PF pf, Usuario usuario); // Método para cadastrar Pessoa Física
        void CadastrarPessoaJuridica(PJ pj, Usuario usuario);
        bool UsuarioExiste(string EmailUsu); // Método para verificar se o usuário já existe
    }
}
