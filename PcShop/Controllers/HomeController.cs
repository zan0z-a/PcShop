using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcShop.Data;

namespace PcShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;

        public HomeController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int? categoryId, string? search)
        {
            var products = db.Products.Include(p => p.Category).ToList();

            if (categoryId != null)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.Categories = db.Categories.ToList();
            ViewBag.CategoryId = categoryId;
            ViewBag.Search = search;
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}