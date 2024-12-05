namespace Site_SmartComfort.Models
{
    public class Favorito
    {
        public int IdFav { get; set; }
        public int IdUsu { get; set; }  // ID do usuário que favoritou o produto
        public int Id { get; set; }  // ID do produto
    }
}
