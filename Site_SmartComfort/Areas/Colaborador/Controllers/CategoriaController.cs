using Microsoft.AspNetCore.Mvc;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaRepository categoriaRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index()
        {
            return View(_categoriaRepository.ObterTodosCategorias());
        }
        public IActionResult CadCategoria()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadCategoria(Categoria categoria)
        {
            _categoriaRepository.CadastrarCategoria(categoria);
            return RedirectToAction(nameof(Index)); // Redireciona para a ação Index
        }
        public IActionResult editarCategoria(int id)
        {
            return View(_categoriaRepository.ObterCategoria(id));
        }

        [HttpPost]
        public IActionResult editarCategoria(Categoria categoria)
        {
            _categoriaRepository.AtualizarCategoria(categoria);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            _categoriaRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

