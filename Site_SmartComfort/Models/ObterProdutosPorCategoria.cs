namespace Site_SmartComfort.Models
{
    public class ObterProdutosPorCategoria
    {
        public IEnumerable<Produto> Cameras { get; set; }
        public IEnumerable<Produto> Roteadores { get; set; }

        public IEnumerable<Produto> Lampadas { get; set; }

        public IEnumerable<Produto> Porteiro { get; set; }

        public IEnumerable<Produto> Fechaduras { get; set; }
    }
}
