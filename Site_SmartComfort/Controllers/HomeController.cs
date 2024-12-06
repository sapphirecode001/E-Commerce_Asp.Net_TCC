using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.CarrinhoCompra;
using Site_SmartComfort.Libraries.Filtro;
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
        private IFavoritoRepository _favoritoRepository;

        // Combine os dois construtores em um único
        public HomeController(ILogger<HomeController> logger, IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository, CookieCarrinhoCompra cookieCarrinhoCompra, IItemRepository itemRepository, LoginUsuario loginUsuario, IFavoritoRepository favoritoRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _itemRepository = itemRepository;
            _loginUsuario = loginUsuario;
            _favoritoRepository = favoritoRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new ObterProdutosPorCategoria
            {
                Cameras = _produtoRepository.ObterProdutosPorCategoria(1), // Produtos de câmeras
                Roteadores = _produtoRepository.ObterProdutosPorCategoria(2), // Produtos de roteadores
                Lampadas = _produtoRepository.ObterProdutosPorCategoria(3), // Produtos de câmeras
                Porteiro = _produtoRepository.ObterProdutosPorCategoria(4), // Produtos de roteadores
                Fechaduras = _produtoRepository.ObterProdutosPorCategoria(4) // Produtos de roteadores
            };

            return View(viewModel); // Retorna a View com o modelo completo
        }

        public IActionResult SobreNos()
        {
            return View();
        }

        [UsuarioAutorizacao]
        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }

        [UsuarioAutorizacao]
        public IActionResult AdicionarItem(int Id)
        {
            Produto produto = _produtoRepository.ObterProduto(Id);

            if (produto == null)
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

        [UsuarioAutorizacao]
        public IActionResult RemoverItem(int Id)
        {
            _cookieCarrinhoCompra.Remover(new Produto() { Id = Id });
            return RedirectToAction(nameof(Carrinho));
        }

        [UsuarioAutorizacao]
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

        [UsuarioAutorizacao]
        public IActionResult confEmp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [UsuarioAutorizacao]
        public IActionResult AdicionarFavorito(int Id)
        {
            var usuario = _loginUsuario.GetUsuario();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            Produto produto = _produtoRepository.ObterProduto(Id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }

            _favoritoRepository.AdicionarFavorito(usuario.IdUsu, Id);

            TempData["MensagemSucesso"] = "Produto adicionado aos favoritos com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [UsuarioAutorizacao]
        public IActionResult RemoverFavorito(int Id)
        {
            var usuario = _loginUsuario.GetUsuario();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            _favoritoRepository.RemoverFavorito(usuario.IdUsu, Id);

            TempData["MensagemSucesso"] = "Produto removido dos favoritos com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [UsuarioAutorizacao]
        public IActionResult Favoritos()
        {
            var usuario = _loginUsuario.GetUsuario();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var favoritos = _favoritoRepository.ObterFavoritosDoUsuario(usuario.IdUsu);

            // Carregar detalhes dos produtos associados
            var produtosFavoritos = favoritos.Select(f => _produtoRepository.ObterProduto(f.Id)).ToList();

            return View(produtosFavoritos);
        }
    }
}
