    namespace Site_SmartComfort.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public long CodBar { get; set; }
        public string NomePro { get; set; }
        public decimal PrecoPro { get; set; }
        public int QtdEstoquePro { get; set; }
        public DateTime GarantiaPro { get; set; }
        public string Voltagem { get; set; }
        public string ImgUrlPro { get; set; }   
        public Categoria RefCategoria { get; set; }

    }
}
