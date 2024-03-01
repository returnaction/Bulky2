using BulkyWeb.Models;
using BulkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _unitOfWork.Product.Add(product);
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
