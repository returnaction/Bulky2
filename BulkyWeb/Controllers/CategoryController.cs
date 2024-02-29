using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Category>? categories = await _context.Categories.ToListAsync();
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
            _context.Categories.Add(category);
            _context.SaveChanges();

            TempData["success"] = "Category created successfuly";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category is null)
                return NotFound();

            return View(category);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

            _context.Categories.Remove(category!);
            _context.SaveChanges();

            TempData["success"] = "Category was deleted successfully";

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

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
            _context.Categories.Update(category);
            _context.SaveChanges();

            TempData["success"] = "Category was updated succussfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
