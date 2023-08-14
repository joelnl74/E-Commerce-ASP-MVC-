using e_commerce_application_web.Models;
using e_commerce_data.Models;
using e_commerce_data_access.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace e_commerce_application_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll(includeProperties: "Category").ToList();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.Get(u => u.Id == id, includeProperties: "Category");

            return View(product);
        }

        public IActionResult Privacy()
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