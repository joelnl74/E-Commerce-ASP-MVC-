using e_commerce_data.Models;
using e_commerce_data_access.Repository;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_application_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll().ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            _productRepository.Add(product);
            _productRepository.Save();

            TempData["success"] = "Successfully created category!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            _productRepository.Update(product);
            _productRepository.Save();

            TempData["success"] = "Successfully updated product!";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _productRepository.Get(c => c.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _productRepository.Get(c => c.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _productRepository.Get(c => c.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            _productRepository.Delete(product);
            _productRepository.Save();
            TempData["success"] = "Successfully deleted product!";

            return RedirectToAction("Index");
        }
    }
}
