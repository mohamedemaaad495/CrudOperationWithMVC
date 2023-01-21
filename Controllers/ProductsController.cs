using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDBContext _repo;
        private readonly IToastNotification _toastNotification;

        public ProductsController(ApplicationDBContext repo,IToastNotification toastNotification)
        {
            _repo = repo;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _repo.Products.ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {         
                var viewModel = new ProductFormViewModel
                {
                    Suppliers = await _repo.Suppliers.ToListAsync(),
                };
                return View("ProductForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Suppliers = await _repo.Suppliers.ToListAsync();
                return View("ProductForm", model);
            }
            var product = new Product
            { 
                ProductName = model.ProductName,
                QuantityPerUnit = model.QuantityPerUnit,
                ReorderLevel=model.ReorderLevel,
                UnitsInStock = model.UnitsInStock,
                UnitsOnOrder = model.UnitsOnOrder,
                UnitPrice = model.UnitPrice,
                SupplierID = model.SupplierID
            };
            _repo.Products.Add(product);
            _repo.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Product Added Successfully");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var product = await _repo.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            var viewModel = new ProductFormViewModel
            {
                ID=product.ProductID,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                ReorderLevel = product.ReorderLevel,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                UnitPrice = product.UnitPrice,
                SupplierID = product.SupplierID,
                Suppliers = await _repo.Suppliers.ToListAsync(),
            };
            return View("ProductForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Suppliers = await _repo.Suppliers.ToListAsync();
                return View("ProductForm", model);
            }
            var product = await _repo.Products.FindAsync(model.ID);
            if (product == null)
                return NotFound();
            product.ProductName = model.ProductName;
            product.QuantityPerUnit = product.QuantityPerUnit;
            product.ReorderLevel = product.ReorderLevel;
            product.UnitsInStock = product.UnitsInStock;
            product.UnitsOnOrder = product.UnitsOnOrder;
            product.UnitPrice = product.UnitPrice;
            product.SupplierID = product.SupplierID;

            _repo.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Product Updated Successfully");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var product = await _repo.Products.Include(m=>m.Supplier).SingleOrDefaultAsync(m=>m.ProductID==id);
            if (product ==null)
                return NotFound();

            return View(product);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var product = await _repo.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _repo.Products.Remove(product);
            _repo.SaveChanges();
            return Ok() ;

        }
    }
}
