using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IPedidoRepository
    {
        IEnumerable<Pedido> ObterTodosPedidos();

        void Cadastrar(Pedido pedido);

        void Atualizar(Pedido pedido);

        Pedido ObterPedidos(int Id);

        void buscaIdPed(Pedido pedido);

        void Excluir(int Id);
    }
}
