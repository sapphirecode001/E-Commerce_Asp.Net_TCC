namespace Site_SmartComfort.Models
{
    public class Item
    {
        public Guid ItemPedidoID { get; set; }

        public int Id { get; set; }
        public int IdPed { get; set; }

        public float PrecoIte { get; set; }
        public string nomeProduto { get; set; }
        public string imagem { get; set; }
        public int QtdIte { get; set; }
    }
}
