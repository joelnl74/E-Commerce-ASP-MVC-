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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            TempData["success"] = "Successfully created category!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();

            TempData["success"] = "Successfully updated category!";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return BadRequest();
            }

            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return BadRequest();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return BadRequest();
            }

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Successfully deleted category!";

            return RedirectToAction("Index");
        }
    }
}
