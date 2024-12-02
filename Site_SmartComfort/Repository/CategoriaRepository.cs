using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Data;

namespace Site_SmartComfort.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _conexaoMySQL;

        public CategoriaRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update tbCategoria set IdCategoria=@IdCategoria, NomeCategoria=@NomeCategoria " +
                                                                    "Where IdCategoria=@IdCategoria ", conexao);

                cmd.Parameters.Add("@IdCategoria", MySqlDbType.Int64).Value = categoria.IdCategoria;
                cmd.Parameters.Add("@NomeCategoria", MySqlDbType.VarChar).Value = categoria.NomeCategoria;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void CadastrarCategoria(Categoria categoria)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Insert into tbCategoria (IdCategoria, NomeCategoria) values (@IdCategoria, @NomeCategoria)", conexao);

                cmd.Parameters.Add("@IdCategoria", MySqlDbType.Int64).Value = categoria.IdCategoria;
                cmd.Parameters.Add("@NomeCategoria", MySqlDbType.VarChar).Value = categoria.NomeCategoria;


                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbCategoria where IdCategoria=@IdCategoria", conexao);
                cmd.Parameters.AddWithValue("@IdCategoria", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public Categoria ObterCategoria(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbCategoria where IdCategoria = @IdCategoria", conexao);

                // Adicionando o parâmetro
                cmd.Parameters.AddWithValue("@IdCategoria", Id);

                Categoria categoria = null;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        categoria = new Categoria
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            NomeCategoria = dr["NomeCategoria"]?.ToString()
                        };
                    }
                }

                return categoria;
            }
        }


        public IEnumerable<Categoria> ObterTodosCategorias()
        {
            List<Categoria> Catlist = new List<Categoria>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCategoria;", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    Catlist.Add(
                        new Categoria
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            NomeCategoria = (string)(dr["NomeCategoria"]),
                        }
                        );
                }
                return Catlist;
            }
        }
    }
}
