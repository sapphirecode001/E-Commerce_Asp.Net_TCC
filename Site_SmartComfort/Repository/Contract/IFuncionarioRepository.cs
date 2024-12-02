using Site_SmartComfort.Models;

    public interface IFuncionarioRepository
    {
        Funcionario Login(string Email, string Senha);
    }

