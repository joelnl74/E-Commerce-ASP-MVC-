using e_commerce_application_web.Data;
using e_commerce_application_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_application_web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Categories.ToList();

            return View(categories);
        }
    }
}
