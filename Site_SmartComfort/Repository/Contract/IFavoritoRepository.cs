using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IFavoritoRepository
    {
        void AdicionarFavorito(int usuarioId, int produtoId);
        void RemoverFavorito(int usuarioId, int produtoId);
        bool VerificarFavorito(int usuarioId, int produtoId);
        IEnumerable<Favoritos> ObterFavoritosDoUsuario(int usuarioId);
    }
}
