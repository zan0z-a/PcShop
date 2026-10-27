using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcShop.Data;
using PcShop.Models;

namespace PcShop.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext db;

        public ReviewsController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var reviews = db.Reviews
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            return View(reviews);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(string text, int rating)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["Message"] = "Отзыв не может быть пустым";
                return RedirectToAction("Index");
            }

            if (rating < 1 || rating > 5)
            {
                rating = 5;
            }

            db.Reviews.Add(new Review
            {
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                Text = text,
                Rating = rating,
                CreatedAt = DateTime.Now
            });
            db.SaveChanges();

            TempData["Message"] = "Спасибо за отзыв";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
            }

            var back = Request.Headers.Referer.ToString();
            return Redirect(string.IsNullOrEmpty(back) ? "/Reviews" : back);
        }
    }
}