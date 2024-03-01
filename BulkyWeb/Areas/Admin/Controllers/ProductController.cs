using BulkyWeb.Models;
using BulkyWeb.Models.ViewModels;
using BulkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = CategoryList
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(productVM);
            }

            _unitOfWork.Product.Add(productVM.Product);
            _unitOfWork.Save();

            TempData["success"] = "Product successfuly created";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Product? product = _unitOfWork.Product.Get(p => p.Id == id);
            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAction(int id)
        {
            Product? product = _unitOfWork.Product.Get(p => p.Id == id);

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            TempData["success"] = "Product successfully deleted";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Product? product = _unitOfWork.Product.Get(p => p.Id == id);
            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();

            TempData["success"] = "Product successfully deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
