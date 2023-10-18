using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.Extensions;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public const string SessionKeyName = "_cart";

        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
                                    ApplicationDbContext context, 
                                    UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string? message)
        {
            return View("Index",message);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Product(int? id, int? categoryId)
        {
            List<Product> products = _context.Product.Where(x => x.Active == true).ToList();

            if (id != null)
            {
                var product = products.Where(p => p.Id == id).FirstOrDefault();
                product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == product.Id).ToList();
                product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == product.Id).ToList();
                return View("ProductDetails", product);
            }

            foreach (var product in products)
            {
                product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == product.Id).ToList();
                product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == product.Id).ToList();
            }

            if (categoryId != null)
            {
                products = products.Where(p => p.ProductCategories.Any(p => p.CategoryId == categoryId)).ToList();
            }

            ViewBag.Categories = _context.Category.ToList();
            return View(products);
        }

        [Authorize]
        public IActionResult Order(List<string> errors)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            if (cart.Count == 0)
            {
                return RedirectToAction("Index");
            }

            for (int i = 0; i < cart.Count; i++)
            {
                var product = _context.Product.Find(cart[i].Product.Id);
                if (product == null)
                {
                    cart.RemoveAt(i);
                    i--;
                    errors.Add("Product not found and was removed from cart!");
                    continue;
                }
                if (product.Quantity < cart[i].Quantity)
                {
                    cart[i].Quantity = product.Quantity;
                    errors.Add("Product quantity was reduced to available quantity!");
                }
                if(product.Quantity == 0)
                {
                    cart.RemoveAt(i);
                    i--;
                    errors.Add("Product "+ product.Title +" is out of stock and was removed from cart!");
                    continue;
                }
                if (!product.Active)
                {
                    cart.RemoveAt(i);
                    i--;
                    errors.Add("Product "+ product.Title +" is not active and was removed from cart!");
                    continue;
                }
            }
            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            foreach (CartItem item in cart)
            {
                item.Product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == item.Product.Id).ToList();
                item.Product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == item.Product.Id).ToList();
            }

            ViewBag.Errors = errors;

            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder(Order order,string shippingsameaspersonal)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
            if(cart == null)
            {
                return RedirectToAction("Index",new {Message="Cart is empty, so no order can be completed"});
            }
            if(cart.Count == 0)
            {
                return RedirectToAction("Index", new { Message = "Cart is empty, so no order can be completed" });
            }

            var errors = new List<string>();

            for (int i = 0; i < cart.Count; i++)
            {
                var product = _context.Product.Find(cart[i].Product.Id);
                if (product == null)
                {
                    cart.RemoveAt(i);
                    i--;
                    errors.Add("Product not found and was removed from cart!");
                    continue;
                }
                if (product.Quantity < cart[i].Quantity)
                {
                    cart[i].Quantity = product.Quantity;
                    errors.Add("Product quantity was reduced to available quantity!");
                }
                if (product.Quantity == 0)
                {
                    cart.RemoveAt(i);
                    i--;
                    errors.Add("Product " + product.Title + " is out of stock and was removed from cart!");
                    continue;
                }
                if (!product.Active)
                {
                    cart.RemoveAt(i);
                    i--;
                    errors.Add("Product is not active and was removed from cart!");
                    continue;
                }
            }
            HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

            if(errors.Count > 0)
            {
                return RedirectToAction("Order", new { errors = errors });
            }

            if(shippingsameaspersonal=="on")
            {
                order.ShippingFirstName = order.BillingFirstName;
                order.ShippingLastName = order.BillingLastName;
                order.ShippingEmail = order.BillingEmail;
                order.ShippingPhone = order.BillingPhone;
                order.ShippingAddress = order.BillingAddress;
                order.ShippingCity = order.BillingCity;
                order.ShippingPostalCode = order.BillingPostalCode;
                order.ShippingCountry = order.BillingCountry;
            }
            order.DateCreated = DateTime.Now;
            order.Total=cart.Sum(x=>x.Product.Price*x.Quantity);
            order.UserId = _userManager.GetUserId(User);
            
            ModelState.Remove("Id");
            ModelState.Remove("OrderItems");
            ModelState.Remove("shippingsameaspersonal");
            if (ModelState.IsValid)
            {
                _context.Order.Add(order);
                _context.SaveChanges();

                int order_id = order.Id;

                foreach (var item in cart)
                {
                    OrderItem order_item=new OrderItem
                    {
                        OrderId=order_id,
                        ProductId=item.Product.Id,
                        Quantity=item.Quantity,
                        Price=item.Product.Price
                    };
                    
                    _context.OrderItem.Add(order_item);
                    _context.Product.Find(item.Product.Id).Quantity -= item.Quantity;
                    _context.SaveChanges();
                }

                HttpContext.Session.Remove(SessionKeyName);
                //HttpContext.Session.SetObjectAsJson(SessionKeyName,"");

                return RedirectToAction("Index", new {message="Thank you for your order :)"});
            }
            else
            {
                errors.Add("Order is not valid!");
                foreach(var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        errors.Add(modelError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Order", new { errors = errors });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}