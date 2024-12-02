using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _conexaoMySQL;
        // construtor com paramentro injeção da conexao do banco
        public PedidoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {

                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbPedido(TotalPed, IdUsu) values(@TotalPed, @IdUsu)", conexao);

                cmd.Parameters.Add("@TotalPed", MySqlDbType.Decimal).Value = pedido.TotalPed;
                cmd.Parameters.Add("@IdUsu", MySqlDbType.Int32).Value = pedido.IdUsu;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        //selecionar o ultimo emprestimo inserido 
        public void buscaIdPed(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlDataReader dr;
                MySqlCommand cmd = new MySqlCommand("SELECT IdPed FROM tbPedido ORDER BY IdPed DESC limit 1", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pedido.IdPed = dr[0].ToString();
                }
                conexao.Close();
            }
        }


        public void Atualizar(Pedido pedido)
        {
            throw new NotImplementedException();
        }


        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }
        public Pedido ObterPedidos(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> ObterTodosPedidos()
        {
            throw new NotImplementedException();
        }
    }
}
