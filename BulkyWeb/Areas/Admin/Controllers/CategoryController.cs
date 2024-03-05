using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repository.IRepository;
using BulkyWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category>? categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }

        public IActionResult Upsert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();

            TempData["success"] = "Category created successfuly";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);

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
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();

            TempData["success"] = "Category was updated succussfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);


            if (category is null)
                return NotFound();

            return View(category);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int id)
        {
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();

            TempData["success"] = "Category was deleted successfully";

            return RedirectToAction(nameof(Index));

        }

        
    }
}
