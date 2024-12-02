using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Site_SmartComfort.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _conexaoMySQL;

        public ProdutoRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // Ajuste da consulta SQL
                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE tbProdutoAutomacao SET NomePro = @NomePro, CodBar = @CodBar, PrecoPro = @PrecoPro, ImgUrlPro = @ImgUrlPro, QtdEstoquePro = @QtdEstoquePro, GarantiaPro = @GarantiaPro, Voltagem = @Voltagem WHERE Id = @Id",
                    conexao);

                // Parâmetros corretamente adicionados
                cmd.Parameters.AddWithValue("@Id", produto.Id);
                cmd.Parameters.AddWithValue("@CodBar", produto.CodBar);
                cmd.Parameters.AddWithValue("@NomePro", produto.NomePro);
                cmd.Parameters.AddWithValue("@PrecoPro", produto.PrecoPro);
                cmd.Parameters.AddWithValue("@ImgUrlPro", produto.ImgUrlPro);
                cmd.Parameters.AddWithValue("@QtdEstoquePro", produto.QtdEstoquePro);
                cmd.Parameters.AddWithValue("@GarantiaPro", produto.GarantiaPro);
                cmd.Parameters.AddWithValue("@Voltagem", produto.Voltagem);

                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbProdutoAutomacao  (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, ImgUrlPro, Voltagem, IdCategoria) " +
                    "values (@CodBar, @NomePro, @PrecoPro, @QtdEstoquePro, @GarantiaPro, @ImgUrlPro, @Voltagem, @IdCategoria)", conexao);

                // cmd.Parameters.Add("@Id", MySqlDbType.Int64).Value = produto.Id;
                cmd.Parameters.Add("@CodBar", MySqlDbType.Decimal).Value = produto.CodBar;
                cmd.Parameters.Add("@nomePro", MySqlDbType.VarChar).Value = produto.NomePro;
                cmd.Parameters.Add("@PrecoPro", MySqlDbType.Decimal).Value = produto.PrecoPro;
                cmd.Parameters.Add("@QtdEstoquePro", MySqlDbType.Int64).Value = produto.QtdEstoquePro;
                cmd.Parameters.Add("@GarantiaPro", MySqlDbType.DateTime).Value = produto.GarantiaPro;
                cmd.Parameters.Add("@ImgUrlPro", MySqlDbType.VarChar).Value = produto.ImgUrlPro;
                cmd.Parameters.Add("@Voltagem", MySqlDbType.VarChar).Value = produto.Voltagem;
                cmd.Parameters.Add("@IdCategoria", MySqlDbType.Int64).Value = produto.RefCategoria.IdCategoria;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbProdutoAutomacao where Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public Produto ObterProduto(int id)
        {
            Produto produto = null;
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbProdutoAutomacao WHERE Id = @Id", conexao);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        produto = new Produto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            CodBar = Convert.ToInt64(reader["CodBar"]),
                            NomePro = reader["NomePro"].ToString(),
                            PrecoPro = Convert.ToDecimal(reader["PrecoPro"]),
                            ImgUrlPro = reader["ImgUrlPro"].ToString(),
                            QtdEstoquePro = Convert.ToInt32(reader["QtdEstoquePro"]),
                            GarantiaPro = Convert.ToDateTime(reader["GarantiaPro"]),
                            Voltagem = reader["Voltagem"].ToString(),
                        };
                    }
                }
            }
            return produto;
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            List<Produto> produtos = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbProdutoAutomacao", conexao);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new Produto
                        {
                            Id = Convert.ToInt32(reader["Id"]), // Certifique-se de que o Id está sendo lido
                            CodBar = Convert.ToInt64(reader["CodBar"]), // Adicionando o CodBar
                            NomePro = reader["NomePro"].ToString(),
                            PrecoPro = Convert.ToDecimal(reader["PrecoPro"]),
                            ImgUrlPro = reader["ImgUrlPro"].ToString(),
                            QtdEstoquePro = Convert.ToInt32(reader["QtdEstoquePro"]),
                            GarantiaPro = Convert.ToDateTime(reader["GarantiaPro"]),
                            Voltagem = reader["Voltagem"].ToString()

                        });
                    }
                }
            }
            return produtos;
        }
    }
}