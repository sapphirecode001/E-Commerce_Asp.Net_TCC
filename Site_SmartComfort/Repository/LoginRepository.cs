using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _connectionString = conf.GetConnectionString("ConexaoMySQL");
        }

        public IEnumerable<Usuario> Validacao(string EmailUsu, string SenhaUsu)
        {
            var usuarios = new List<Usuario>();

            // Use o construtor MySqlConnection corretamente para criar uma instância
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string query = "SELECT IdUsu, EmailUsu, SenhaUsu FROM tbUsuario WHERE " +
                               "EmailUsu = @EmailUsu AND SenhaUsu = @SenhaUsu";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@EmailUsu", EmailUsu);
                    cmd.Parameters.AddWithValue("@SenhaUsu", SenhaUsu);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                IdUsu = reader.GetInt32("IdUsu"),
                                EmailUsu = reader.GetString("EmailUsu"),
                                SenhaUsu = reader.GetString("SenhaUsu")
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
    }
}
