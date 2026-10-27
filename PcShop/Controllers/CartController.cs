using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcShop.Data;
using PcShop.Models;

namespace PcShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext db;

        public CartController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var item = db.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (item == null)
            {
                db.CartItems.Add(new CartItem { UserId = userId, ProductId = productId, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }

            db.SaveChanges();
            TempData["Message"] = "Товар добавлен в корзину";

            var back = Request.Headers.Referer.ToString();
            return Redirect(string.IsNullOrEmpty(back) ? "/" : back);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var item = db.CartItems.FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (item != null)
            {
                db.CartItems.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction("Profile", "Account");
        }
    }
}