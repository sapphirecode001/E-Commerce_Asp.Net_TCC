using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly string _conexaoMySQL;

        public FavoritoRepository(IConfiguration configuration)
        {
            _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        }

        // Adiciona um produto aos favoritos
        public void AdicionarFavorito(int usuarioId, int produtoId)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "INSERT INTO tbFavoritos (IdUsu, Id) VALUES (@UsuarioId, @ProdutoId)";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Remove um produto dos favoritos
        public void RemoverFavorito(int usuarioId, int produtoId)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "DELETE FROM tbFavoritos WHERE IdUsu = @UsuarioId AND Id = @ProdutoId";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Verifica se o produto está nos favoritos do usuário
        public bool VerificarFavorito(int usuarioId, int produtoId)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT COUNT(*) FROM tbFavoritos WHERE IdUsu = @UsuarioId AND Id = @ProdutoId";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                    var result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
        }

        // Obtém todos os favoritos do usuário
        public IEnumerable<Favoritos> ObterFavoritosDoUsuario(int usuarioId)
        {
            List<Favoritos> favoritos = new List<Favoritos>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string query = "SELECT * FROM tbFavoritos WHERE IdUsu = @UsuarioId";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var favorito = new Favoritos
                            {
                                IdFav = Convert.ToInt32(reader["IdFav"]),
                                IdUsu = Convert.ToInt32(reader["IdUsu"]),
                                Id = Convert.ToInt32(reader["Id"])
                            };
                            favoritos.Add(favorito);
                        }
                    }
                }
            }

            return favoritos;
        }
    }
}
