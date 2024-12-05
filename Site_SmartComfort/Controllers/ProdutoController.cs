using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site_SmartComfort.GerenciaArquivos;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _logger = logger;
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;

        }

        public IActionResult Index()
        {
            return View(_produtoRepository.ObterTodosProdutos());
        }

        public IActionResult BuscarProduto(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return View("Index", new List<Produto>()); // Mostra uma lista vazia se não houver termo.
            }

            var produtos = _produtoRepository.BuscarProdutos(termo);

            return View("BuscarProduto", produtos);
        }

        public IActionResult ProdutoClick(int id)
        {
            var produto = _produtoRepository.ObterProduto(id); // Busca o produto pelo ID

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return View(produto); // Retorna o produto para a view
        }

        [HttpGet]
        public IActionResult CadProduto()
        {

            var listaCategoria = _categoriaRepository.ObterTodosCategorias();
            ViewBag.ListaCategorias = new SelectList(listaCategoria, "IdCategoria", "NomeCategoria");
            return View();
        }
        [HttpPost]
        public IActionResult CadProduto(Produto produto, IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);
            produto.ImgUrlPro = Caminho;
            produto.GarantiaPro = DateTime.Now.AddDays(90);

            var listaCategoria = _categoriaRepository.ObterTodosCategorias();
            ViewBag.ListaCategorias = new SelectList(listaCategoria, "IdCategoria", "NomeCategoria");

            _produtoRepository.CadastrarProduto(produto);
            return RedirectToAction(nameof(Index)); // Redireciona para a listagem de produtos
        }
        public IActionResult EditarProduto(int id)
        {
            // Obtém o produto pelo Id
            var produto = _produtoRepository.ObterProduto(id);

            if (produto == null)
            {
                // Se o produto não for encontrado, redireciona ou exibe uma mensagem de erro
                return NotFound();
            }

            return View(produto); // Passa o produto para a view-
        }

        public IActionResult ProdutosPorCategoria(int idCategoria)
        {
            var produtos = _produtoRepository.ObterProdutosPorCategoria(idCategoria);
            return PartialView("_CarrosselProdutos", produtos); // Usando uma Partial View
        }

        [HttpPost]
        public IActionResult editarProduto(Produto produto)
        {
            _produtoRepository.AtualizarProduto(produto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _produtoRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
