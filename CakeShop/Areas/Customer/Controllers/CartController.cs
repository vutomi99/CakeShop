using CakeShop.DataAccess.Repository.IRepository;
using CakeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CakeShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Add to Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            // Get logged-in user's email
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Json(new { success = false, message = "User email not found!" });
            }

            // Check if product exists
            var product = _unitOfWork.Product.Get(p => p.Id == productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found!" });
            }

            // Check if item is already in the cart
            var cartItem = _unitOfWork.Cart.Get(u => u.ProductId == productId && u.UserEmail == userEmail);

            if (cartItem == null)
            {
                // Create new cart item
                cartItem = new Cart
                {
                    ProductId = productId,
                    UserEmail = userEmail,
                    Quantity = quantity
                };
                _unitOfWork.Cart.Add(cartItem);
            }
            else
            {
                // Update quantity if item exists
                cartItem.Quantity += quantity;
                _unitOfWork.Cart.Update(cartItem);
            }

            _unitOfWork.Save();
            TempData["Success"] = "Product Added to Cart  Successfuly";

            return RedirectToAction("Index"); ;
        }

        // View Cart
        public IActionResult Index()
        {
            // Get logged-in user's email
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                TempData["Error"] = "User email not found!";
                return RedirectToAction("Index", "Product");
            }

            // Get cart items for the user
            var cartItems = _unitOfWork.Cart.GetAll(u => u.UserEmail == userEmail, includeProperties: "Product");
            return View(cartItems);
        }

        // Remove from Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            // Find cart item by ID
            var cartItem = _unitOfWork.Cart.Get(u => u.Id == id);
            if (cartItem == null)
            {
                return Json(new { success = false, message = "Cart item not found!" });
            }

            _unitOfWork.Cart.Remove(cartItem);
            _unitOfWork.Save();

            TempData["Success"] = "Product Removed from Cart  Successfuly";

            return RedirectToAction("Index");
        }

        // Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout()
        {
            // Get logged-in user's email
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                TempData["Error"] = "User email not found!";
                return RedirectToAction("Index", "Product");
            }

            // Get cart items for the user
            var cartItems = _unitOfWork.Cart.GetAll(u => u.UserEmail == userEmail, includeProperties: "Product");

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }

          
            foreach (var item in cartItems)
            {
                _unitOfWork.Cart.Remove(item);
            }
            _unitOfWork.Save();

            TempData["Success"] = "Checkout successful!";
            return RedirectToAction("index1", "Cart");
        }
        public IActionResult Index1()
        {
       
            return View();
        }

    }
}
