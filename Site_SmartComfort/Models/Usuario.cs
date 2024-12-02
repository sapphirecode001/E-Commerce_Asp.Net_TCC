using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Site_SmartComfort.Models
{
    public class Usuario
    {
        public int IdUsu { get; set; }
        public string EmailUsu { get; set; }
        public long? TelefoneUsu1 { get; set; }
        public long? TelefoneUsu2 { get; set; }
        public string SenhaUsu { get; set; }
        public string DataCadUsu { get; set; }

        [NotMapped] // Para que essa propriedade não seja mapeada na tabela do banco de dados
        [Compare("SenhaUsu", ErrorMessage = "A confirmação da senha não coincide.")]
        public string ConfirmarSenha { get; set; }


        // Campos caso seja PF
        public int? IdPJ { get; set; }
        public long? Cpf { get; set; }         
        public string? NomeCompleto { get; set; }


        // Campos Caso seja PJ
        public int? IdPF { get; set; }
        public long? Cnpj { get; set; }        
        public string? RazaoSocial { get; set; } 
        public string? NomeResponsavel { get; set; } 
    }  
}
