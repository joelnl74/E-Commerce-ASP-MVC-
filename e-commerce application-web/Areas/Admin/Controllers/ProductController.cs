using e_commerce_data.Models;
using e_commerce_data.ViewModels;
using e_commerce_data_access.Repository;
using e_commerce_utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace e_commerce_application_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> products = _productRepository.GetAll(includeProperties:"Category").ToList();


            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ProductViewModel productViewModel = new()
            {
                SelectListItems = CategoryList,
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                return View(productViewModel);
            }

            productViewModel.Product = _productRepository.Get(c => c.Id == id);

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel viewModel, IFormFile? file)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"Resources\Images\Product");

                if (string.IsNullOrEmpty(viewModel.Product.ImageUrl) == false) 
                {
                    var oldImagePath = Path.Combine(wwwRootPath, viewModel.Product.ImageUrl.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                viewModel.Product.ImageUrl = @"\Resources\Images\Product\" + fileName;
            }

            if (viewModel.Product.Id == 0)
            {
                _productRepository.Add(viewModel.Product);
            }
            else
            {
                _productRepository.Update(viewModel.Product);
            }

            _productRepository.Save();

            TempData["success"] = "Successfully created category!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _productRepository.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = products });
        }

        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _productRepository.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }


            _productRepository.Delete(productToBeDeleted);
            _productRepository.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
