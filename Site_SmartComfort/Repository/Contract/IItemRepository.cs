using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IItemRepository
    {
        //CRUD
        IEnumerable<Item> ObterTodosItens();

        void Cadastrar(Item item);

        void Atualizar(Item item);

        Item ObterItens(int Id);

        void Excluir(int Id);
    }
}
