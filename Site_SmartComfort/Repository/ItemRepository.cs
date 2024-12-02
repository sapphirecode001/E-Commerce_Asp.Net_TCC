using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _conexaoMySQL;
        public ItemRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Cadastrar(Item item)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbItemPedido(QtdIte, PrecoIte, IdPed, IdPro) values(@QtdIte, @PrecoIte, @IdPed, @IdPro)", conexao);

                cmd.Parameters.Add("@QtdIte", MySqlDbType.Int32).Value = item.QtdIte;
                cmd.Parameters.Add("@PrecoIte", MySqlDbType.Decimal).Value = item.PrecoIte;
                cmd.Parameters.Add("@IdPed", MySqlDbType.VarChar).Value = item.IdPed;
                cmd.Parameters.Add("@IdPro", MySqlDbType.VarChar).Value = item.Id;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Item item)
        {
            throw new NotImplementedException();
        }



        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Item ObterItens(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> ObterTodosItens()
        {
            throw new NotImplementedException();
        }
    }
}
