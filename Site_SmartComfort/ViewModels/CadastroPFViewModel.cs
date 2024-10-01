using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Site_SmartComfort.ViewModels
{
    public class CadastroPFViewModel
    {
        [Required(ErrorMessage = "O Nome Completo é obrigatório.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public long Cpf { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string EmailUsu { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        public long TelefoneUsu1 { get; set; }

        public long TelefoneUsu2 { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string SenhaUsu { get; set; }

        [NotMapped]
        [Compare("SenhaUsu", ErrorMessage = "A confirmação da senha não coincide.")]
        [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        public string ConfirmarSenha { get; set; }
    }
}
