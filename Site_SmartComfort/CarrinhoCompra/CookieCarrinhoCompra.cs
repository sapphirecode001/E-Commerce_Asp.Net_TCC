using Newtonsoft.Json;
using Site_SmartComfort.Models;

namespace Site_SmartComfort.CarrinhoCompra
{
    public class CookieCarrinhoCompra
    {
        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;

        public CookieCarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        public void Salvar(List<Produto> lista)
        {
            string Valor = JsonConvert.SerializeObject(lista);
            _cookie.Cadastrar(Key, Valor);
        }

        public List<Produto> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<Produto>>(valor);
            }
            else
            {
                return new List<Produto>();
            }
        }

        public void Cadastrar(Produto item)
        {
            List<Produto> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);

                if (ItemLocalizado == null)
                {
                    Lista.Add(item);
                }
                else
                {
                    ItemLocalizado.QtdEstoquePro = ItemLocalizado.QtdEstoquePro + 1;
                }
            }
            else
            {
                Lista = new List<Produto>();
                Lista.Add(item);
            }
            Salvar(Lista);
        }

        public void Atualizar(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.QtdEstoquePro = item.QtdEstoquePro + 1;
                Salvar(Lista);
            }
        }

        public void Remover(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.Id == item.Id);

            if (ItemLocalizado != null)
            {
                Lista.Remove(ItemLocalizado);
                Salvar(Lista);
            }
        }

        public bool Existe(string Key)
        {
            if (_cookie.Existe(Key))
            {
                return false;
            }

            return true;
        }

        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
    }
}
