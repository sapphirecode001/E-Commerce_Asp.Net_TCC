using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _connectionString = conf.GetConnectionString("ConexaoMySQL");
        }

        public void CadastrarPessoaFisica(PF pf, Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();

                // Insira o usuário na tabela tbUsuario
                string usuarioQuery = "INSERT INTO tbUsuario (EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2) VALUES (@EmailUsu, @SenhaUsu, @TelefoneUsu1, @TelefoneUsu2)";

                using (var cmd = new MySqlCommand(usuarioQuery, conexao))
                {
                    cmd.Parameters.AddWithValue("@EmailUsu", usuario.EmailUsu);
                    cmd.Parameters.AddWithValue("@SenhaUsu", usuario.SenhaUsu);
                    cmd.Parameters.AddWithValue("@TelefoneUsu1", usuario.TelefoneUsu1);
                    cmd.Parameters.AddWithValue("@TelefoneUsu2", usuario.TelefoneUsu2);

                    cmd.ExecuteNonQuery();
                    long lastInsertedId = cmd.LastInsertedId; // Captura o último ID inserido

                    // Insira a pessoa física na tabela tbPF
                    string query = "INSERT INTO tbPF (Cpf, NomeCompleto, IdUsu) VALUES (@Cpf, @NomeCompleto, @IdUsu)";
                    using (var pfCmd = new MySqlCommand(query, conexao))
                    {
                        pfCmd.Parameters.AddWithValue("@Cpf", pf.Cpf);
                        pfCmd.Parameters.AddWithValue("@NomeCompleto", pf.NomeCompleto);
                        pfCmd.Parameters.AddWithValue("@IdUsu", lastInsertedId);
                        pfCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void CadastrarPessoaJuridica(PJ pj, Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();

                // Insira o usuário na tabela tbUsuario
                string usuarioQuery = "INSERT INTO tbUsuario (EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2) VALUES (@EmailUsu, @SenhaUsu, @TelefoneUsu1, @TelefoneUsu2)";

                using (var cmd = new MySqlCommand(usuarioQuery, conexao))
                {
                    cmd.Parameters.AddWithValue("@EmailUsu", usuario.EmailUsu);
                    cmd.Parameters.AddWithValue("@SenhaUsu", usuario.SenhaUsu);
                    cmd.Parameters.AddWithValue("@TelefoneUsu1", usuario.TelefoneUsu1);
                    cmd.Parameters.AddWithValue("@TelefoneUsu2", usuario.TelefoneUsu2);

                    cmd.ExecuteNonQuery();
                    long lastInsertedId = cmd.LastInsertedId; // Captura o último ID inserido

                    // Insira a pessoa jurídica na tabela tbPJ
                    string query = "INSERT INTO tbPJ (Cnpj, RazaoSocial, NomeResponsavel, IdUsu) VALUES (@Cnpj, @RazaoSocial, @NomeResponsavel, @IdUsu)";
                    using (var pjCmd = new MySqlCommand(query, conexao))
                    {
                        pjCmd.Parameters.AddWithValue("@Cnpj", pj.Cnpj);
                        pjCmd.Parameters.AddWithValue("@RazaoSocial", pj.RazaoSocial);
                        pjCmd.Parameters.AddWithValue("@NomeResponsavel", pj.NomeResponsavel);
                        pjCmd.Parameters.AddWithValue("@IdUsu", lastInsertedId);
                        pjCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public bool UsuarioExiste(string EmailUsu)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string query = "SELECT COUNT(*) FROM tbUsuario WHERE EmailUsu = @EmailUsu";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@EmailUsu", EmailUsu);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
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
