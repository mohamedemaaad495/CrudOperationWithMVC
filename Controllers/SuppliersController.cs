using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ApplicationDBContext _repo;
        private readonly IToastNotification _toastNotification;
        public SuppliersController(ApplicationDBContext repo, IToastNotification toastNotification)
        {
            _repo = repo;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _repo.Suppliers.ToListAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new SuppliersViewModel
            {
                Products = await _repo.Products.ToListAsync(),
            };
            return View("Create", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(SuppliersViewModel model)
        {
            var supplier = new Supplier
            {
               SupplierName = model.SupplierName
            };
            _repo.Suppliers.Add(supplier);
            _repo.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Supplier Added Successfully");
            return  RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
            var supplier = await _repo.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();
            var viewModel = new SuppliersViewModel
            {
             SupplierName = supplier.SupplierName
            };
            return View("Create", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SuppliersViewModel model)
        {
            
            var sub = await _repo.Suppliers.FindAsync(model.SupplierId);
            if (sub == null)
                return NotFound();
            sub.SupplierName = model.SupplierName;
            _repo.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Supplier Updated Successfully");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var sub = await _repo.Suppliers.SingleOrDefaultAsync(m => m.SupplierID == id);
            if (sub == null)
                return NotFound();

            return View(sub);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var sub = await _repo.Suppliers.FindAsync(id);
            if (sub == null)
                return NotFound();
            _repo.Suppliers.Remove(sub);
            _repo.SaveChanges();
            return Ok();

        }
    }
}
