namespace Site_SmartComfort.Models
{
    public class Favoritos
    {
        public int IdFav { get; set; }
        public int IdUsu { get; set; } // Se você quiser associar aos usuários
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Preco { get; set; } // Caso seja um link
    }
}
