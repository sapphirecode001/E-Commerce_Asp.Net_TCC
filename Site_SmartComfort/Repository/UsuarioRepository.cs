using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Data;

namespace Site_SmartComfort.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySQL;

        public UsuarioRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public Usuario LoginUsuario(string EmailUsu, string SenhaUsu)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL sp_LoginUsuario (@EmailUsu, @SenhaUsu)"; // Chama a procedure

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.Add("@EmailUsu", MySqlDbType.VarChar).Value = EmailUsu;
                cmd.Parameters.Add("@SenhaUsu", MySqlDbType.VarChar).Value = SenhaUsu;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.Read()) // Verifica se encontrou um usuário
                    {
                        Usuario usuario = new Usuario
                        {
                            IdUsu = Convert.ToInt32(dr["IdUsu"]),
                            EmailUsu = Convert.ToString(dr["EmailUsu"]),
                            SenhaUsu = Convert.ToString(dr["SenhaUsu"]),
                            TelefoneUsu1 = Convert.ToInt64(dr["TelefoneUsu1"]),
                            TelefoneUsu2 = dr.IsDBNull(dr.GetOrdinal("TelefoneUsu2")) ? (long?)null : Convert.ToInt64(dr["TelefoneUsu2"]),
                            DataCadUsu = Convert.ToString(dr["DataCadUsu"])
                        };

                        // Verifica o tipo de usuário (PF ou PJ)
                        string tipoUsuario = Convert.ToString(dr["TipoUsu"]);

                        if (tipoUsuario == "PF")
                        {
                            usuario.IdPF = dr.IsDBNull(dr.GetOrdinal("Cpf")) ? (int?)null : Convert.ToInt32(dr["IdPF"]);
                            usuario.Cpf = dr.IsDBNull(dr.GetOrdinal("Cpf")) ? (long?)null : Convert.ToInt64(dr["Cpf"]);
                            usuario.NomeCompleto = dr.IsDBNull(dr.GetOrdinal("NomeCompleto")) ? null : Convert.ToString(dr["NomeCompleto"]);
                        }
                        else if (tipoUsuario == "PJ")
                        {
                            usuario.IdPJ = dr.IsDBNull(dr.GetOrdinal("IdPJ")) ? (int?)null : Convert.ToInt32(dr["IdPJ"]);
                            usuario.Cnpj = dr.IsDBNull(dr.GetOrdinal("Cnpj")) ? (long?)null : Convert.ToInt64(dr["Cnpj"]);
                            usuario.RazaoSocial = dr.IsDBNull(dr.GetOrdinal("RazaoSocial")) ? null : Convert.ToString(dr["RazaoSocial"]);
                            usuario.NomeResponsavel = dr.IsDBNull(dr.GetOrdinal("NomeResponsavel")) ? null : Convert.ToString(dr["NomeResponsavel"]);
                        }

                        return usuario; // Retorna o usuário encontrado
                    }
                }
            }

            // Retorna null se nenhum usuário for encontrado
            return null;
        }

        public IEnumerable<Usuario> ObterTodosUsuarios()
        {
            throw new NotImplementedException();
        }

        public Usuario ObterUsuario(int Id)
        {
            throw new NotImplementedException();
        }

        public bool UsuarioExiste(string EmailUsu)
        {
            throw new NotImplementedException();
        }

        public void CadastrarUsuarioPF(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL sp_InserirUsuarioPF(@NomeCompleto, @Cpf, @EmailUsu, @SenhaUsu, @TelefoneUsu1, @TelefoneUsu2)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.Add("@NomeCompleto", MySqlDbType.VarChar).Value = usuario.NomeCompleto;
                cmd.Parameters.Add("@Cpf", MySqlDbType.Int64).Value = usuario.Cpf;
                cmd.Parameters.Add("@EmailUsu", MySqlDbType.VarChar).Value = usuario.EmailUsu;
                cmd.Parameters.Add("@SenhaUsu", MySqlDbType.VarChar).Value = usuario.SenhaUsu;
                cmd.Parameters.Add("@TelefoneUsu1", MySqlDbType.Int64).Value = usuario.TelefoneUsu1;
                cmd.Parameters.Add("@TelefoneUsu2", MySqlDbType.Int64).Value = usuario.TelefoneUsu2;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void CadastrarUsuarioPJ(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL sp_InserirUsuarioPJ(@RazaoSocial, @Cnpj, @NomeResponsavel, @EmailUsu, @SenhaUsu, @TelefoneUsu1, @TelefoneUsu2)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.Add("@RazaoSocial", MySqlDbType.VarChar).Value = usuario.RazaoSocial;
                cmd.Parameters.Add("@Cnpj", MySqlDbType.Int64).Value = usuario.Cnpj;
                cmd.Parameters.Add("@NomeResponsavel", MySqlDbType.VarChar).Value = usuario.NomeResponsavel;
                cmd.Parameters.Add("@EmailUsu", MySqlDbType.VarChar).Value = usuario.EmailUsu;
                cmd.Parameters.Add("@SenhaUsu", MySqlDbType.VarChar).Value = usuario.SenhaUsu;
                cmd.Parameters.Add("@TelefoneUsu1", MySqlDbType.Int64).Value = usuario.TelefoneUsu1;
                cmd.Parameters.Add("@TelefoneUsu2", MySqlDbType.Int64).Value = usuario.TelefoneUsu2;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                var query = "CALL sp_AtualizarUsuario(@NomeCompleto, @Cpf, @RazaoSocial, @Cnpj, @NomeResponsavel, @EmailUsu, @SenhaUsu, @TelefoneUsu1, @TelefoneUsu2, @IdUsu, @IdPF, @IdPJ)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.AddWithValue("@NomeCompleto", usuario.NomeCompleto ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Cpf", usuario.Cpf ?? (object)DBNull.Value);

                cmd.Parameters.AddWithValue("@RazaoSocial", usuario.RazaoSocial ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Cnpj", usuario.Cnpj ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NomeResponsavel", usuario.NomeResponsavel ?? (object)DBNull.Value);

                cmd.Parameters.AddWithValue("@EmailUsu", usuario.EmailUsu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SenhaUsu", usuario.SenhaUsu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TelefoneUsu1", usuario.TelefoneUsu1 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TelefoneUsu2", usuario.TelefoneUsu2 ?? (object)DBNull.Value);

                cmd.Parameters.AddWithValue("@IdUsu", usuario.IdUsu);
                cmd.Parameters.AddWithValue("@IdPF", usuario.IdPF ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IdPJ", usuario.IdPJ ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
