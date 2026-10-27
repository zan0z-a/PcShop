using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PcShop.Data;
using PcShop.Models;

namespace PcShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext db;

        public AdminController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Reviews = db.Reviews.Include(r => r.User).OrderByDescending(r => r.CreatedAt).ToList();
            var products = db.Products.Include(p => p.Category).OrderByDescending(p => p.Id).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                return View(product);
            }

            db.Products.Add(product);
            db.SaveChanges();
            TempData["Message"] = "Товар добавлен";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}