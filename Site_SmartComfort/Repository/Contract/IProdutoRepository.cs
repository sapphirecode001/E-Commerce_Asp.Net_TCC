using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IProdutoRepository
    {
        
        IEnumerable<Produto> ObterTodosProdutos();
        Produto ObterProduto(int Id);
       
        void AtualizarProduto(Produto produto);

        void CadastrarProduto(Produto produto);
        void Excluir(int Id);

    }
}
