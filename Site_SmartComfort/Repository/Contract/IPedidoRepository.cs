using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface IPedidoRepository
    {
        void Cadastrar(Pedido pedido);
        void buscaIdPed(Pedido pedido);
    }
}
