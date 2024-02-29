using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Category>? categories = _categoryRepo.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _categoryRepo.Add(category);
            _categoryRepo.Save();

            TempData["success"] = "Category created successfuly";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Category? category = _categoryRepo.Get(c => c.Id == id);


            if (category is null)
                return NotFound();

            return View(category);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int id)
        {
            Category? category = _categoryRepo.Get(c => c.Id == id);

            _categoryRepo.Remove(category);
            _categoryRepo.Save();

            TempData["success"] = "Category was deleted successfully";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            Category? category = _categoryRepo.Get(c => c.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepo.Update(category);
            _categoryRepo.Save();

            TempData["success"] = "Category was updated succussfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
