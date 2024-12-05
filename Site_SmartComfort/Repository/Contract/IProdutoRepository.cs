using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IProdutoRepository
    {
        
        IEnumerable<Produto> ObterTodosProdutos();
        Produto ObterProduto(int Id);
       
        void AtualizarProduto(Produto produto);

        IEnumerable<Produto> ObterProdutosPorCategoria(int idCategoria);

        void CadastrarProduto(Produto produto);
        IEnumerable<Produto> BuscarProdutos(string termo);

        void Excluir(int Id);

    }
}
