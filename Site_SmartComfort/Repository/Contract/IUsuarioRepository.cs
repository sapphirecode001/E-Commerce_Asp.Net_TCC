using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IUsuarioRepository
    {
        bool UsuarioExiste(string EmailUsu); 
        IEnumerable<Usuario> ObterTodosUsuarios();
        Usuario ObterUsuario(int Id);
        Usuario LoginUsuario(string EmailUsu, string SenhaUsu);
        void AtualizarUsuario(Usuario usuario);
     
        void CadastrarUsuarioPF(Usuario usuario);

        void CadastrarUsuarioPJ(Usuario usuario);
    }
}
