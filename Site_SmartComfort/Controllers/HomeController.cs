using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.CarrinhoCompra;
using Site_SmartComfort.Libraries.Login;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Diagnostics;

namespace Site_SmartComfort.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProdutoRepository _produtoRepository;
        private IItemRepository _itemRepository;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        private IPedidoRepository _pedidoRepository;
        private LoginUsuario _loginUsuario;

        // Combine os dois construtores em um único
        public HomeController(ILogger<HomeController> logger, IProdutoRepository produtoRepository, 
            IPedidoRepository pedidoRepository, CookieCarrinhoCompra cookieCarrinhoCompra, IItemRepository itemRepository, LoginUsuario loginUsuario)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _itemRepository = itemRepository;
            _loginUsuario = loginUsuario;
        }

        public IActionResult Index()
        {
            var produtos = _produtoRepository.ObterTodosProdutos();  // Obtém todos os produtos cadastrados
            return View(produtos); // Passa os produtos para a view
        }
        public IActionResult SobreNos()
        {
            return View();
        }

        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }

        public IActionResult AdicionarItem(int Id)
        {
            Produto produto = _produtoRepository.ObterProduto(Id);

            if(produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Produto()
                {
                    Id = Id,
                    QtdEstoquePro = 1,
                    ImgUrlPro = produto.ImgUrlPro,
                    NomePro = produto.NomePro,
                    CodBar = produto.CodBar,
                    Voltagem = produto.Voltagem,
                    GarantiaPro = produto.GarantiaPro,
                    PrecoPro = produto.PrecoPro,
                    RefCategoria = produto.RefCategoria
                };
                _cookieCarrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Carrinho));
            }
        }

        public IActionResult RemoverItem(int Id)
        {
            _cookieCarrinhoCompra.Remover(new Produto() { Id = Id });
            return RedirectToAction(nameof(Carrinho));
        }

        public IActionResult SalvarCarrinho(Pedido pedido)
        {
            List<Produto> carrinho = _cookieCarrinhoCompra.Consultar();


            Pedido mdE = new Pedido();
            Item mdI = new Item();

            mdE.IdUsu = Convert.ToString(_loginUsuario.GetUsuario().IdUsu);
            _pedidoRepository.Cadastrar(mdE);

            _pedidoRepository.buscaIdPed(pedido);

            for (int i = 0; i < carrinho.Count; i++)
            {

                mdI.IdPed = Convert.ToInt32(pedido.IdPed);
                mdI.Id = carrinho[i].Id;

                _itemRepository.Cadastrar(mdI);
            }

            _cookieCarrinhoCompra.RemoverTodos();
            return RedirectToAction("confEmp");
        }

        public IActionResult confEmp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
