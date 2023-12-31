﻿using e_commerce_application_web.Data;
using e_commerce_application_web.Models;
using e_commerce_data_access.Repository;
using e_commerce_utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_application_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll().ToList();

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

            _categoryRepository.Add(category);
            _categoryRepository.Save();

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

            _categoryRepository.Update(category);
            _categoryRepository.Save();

            TempData["success"] = "Successfully updated category!";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _categoryRepository.Get(c => c.Id == id);

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

            Category? category = _categoryRepository.Get(c => c.Id == id);

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

            Category? category = _categoryRepository.Get(c => c.Id == id);

            if (category == null)
            {
                return BadRequest();
            }

            _categoryRepository.Delete(category);
            _categoryRepository.Save();
            TempData["success"] = "Successfully deleted category!";

            return RedirectToAction("Index");
        }
    }
}
