using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Site_SmartComfort.Models
{
    public class Usuario
    {
        public int IdUsu { get; set; }

        public string EmailUsu { get; set; }

        public long TelefoneUsu1 { get; set; }

        public long TelefoneUsu2 { get; set; }

        public string SenhaUsu { get; set; }

        [NotMapped] // Para que essa propriedade não seja mapeada na tabela do banco de dados
        [Compare("SenhaUsu", ErrorMessage = "A confirmação da senha não coincide.")]
        public string ConfirmarSenha { get; set; }
    }  
}
