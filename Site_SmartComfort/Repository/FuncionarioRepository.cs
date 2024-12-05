using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using System.Data;

namespace Site_SmartComfort.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        // Propriedade Privada para injetar a conexão com o banco de dados ;
        private readonly string _conexaoMySQL;

        //Metodo construtor da classe ColaboradorRepository    
        public FuncionarioRepository(IConfiguration conf)
        {
            // Injeção de dependencia do banco de dados
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public Funcionario Login(string Email, string Senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbFuncionario where EmailFunc = @EmailFunc and SenhaFunc = @SenhaFunc", conexao);

                cmd.Parameters.Add("@EmailFunc", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@SenhaFunc", MySqlDbType.VarChar).Value = Senha;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Funcionario colaborador = new Funcionario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    colaborador.IdFunc = Convert.ToInt32(dr["IdFunc"]);
                    colaborador.NomeFunc = Convert.ToString(dr["NomeFunc"]);
                    colaborador.EmailFunc = Convert.ToString(dr["EmailFunc"]);
                    colaborador.SenhaFunc = Convert.ToString(dr["SenhaFunc"]);
                }
                return colaborador;
            }
        }
    }
}
