using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> ObterTodosCategorias();
        Categoria ObterCategoria(int Id); // Aqui corrige para Categoria, não Produto
        void AtualizarCategoria(Categoria categoria);
        void CadastrarCategoria(Categoria categoria);
        void Excluir(int Id);
    }
}
