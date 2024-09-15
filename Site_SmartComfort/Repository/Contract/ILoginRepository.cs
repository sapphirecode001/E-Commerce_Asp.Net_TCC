using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface ILoginRepository
    {
        IEnumerable<Usuario> Validacao(string EmailUsu, string SenhaUsu);
    }
}
