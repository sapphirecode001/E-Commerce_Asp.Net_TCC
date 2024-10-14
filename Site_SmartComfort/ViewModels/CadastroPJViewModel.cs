using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Site_SmartComfort.ViewModels
{
    public class CadastroPJViewModel
    {
        [Required(ErrorMessage = "RZ é obrigatório.")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string NomeResponsavel { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public int Cnpj { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string EmailUsu { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        public int TelefoneUsu1 { get; set; }

        public int TelefoneUsu2 { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string SenhaUsu { get; set; }

        [NotMapped]
        [Compare("SenhaUsu", ErrorMessage = "A confirmação da senha não coincide.")]
        [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        public string ConfirmarSenha { get; set; }
    }
}
