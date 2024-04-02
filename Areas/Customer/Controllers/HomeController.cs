using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_BookStore.DataAccess.Data;
using Project_BookStore.DataAccess.Repository;
using Project_BookStore.Models;
using Project_BookStore.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace Project_BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepo;

        private readonly ICategoryRepository _categoryRepo;

        private readonly IShoppingCartRepository _shoppingCartRepo;
        //private readonly AppDbContext _dbContext;

        public HomeController(IProductRepository productRepo, ICategoryRepository categoryRepo, IShoppingCartRepository shoppingCart)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _shoppingCartRepo = shoppingCart;
            //_dbContext = dbContext;


        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _productRepo.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int productid)
        {
            ShoppingCart cart = new()
            {
                //Product = _productRepo.Get(u => u.Id == productid, includeProperties: "Category")
                Product = _productRepo.Get(u => u.Id == productid, includeProperties: "Category"),
                Count = 1,
                ProductId = productid
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUsderId = userId;


            ShoppingCart cartFromDb = _shoppingCartRepo.Get(u => u.ApplicationUsderId == userId &&
                u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {

                cartFromDb.Count += shoppingCart.Count;
                _shoppingCartRepo.Update(cartFromDb);

            }
            else
            {
                _shoppingCartRepo.Add(shoppingCart);


                //HttpContext.Session.SetInt32(StaticDetail.SessionCart,
                //_shoppingCartRepo.GetAll(u => u.ApplicationUsderId == userId).Count());
            }



            _shoppingCartRepo.Save();
            return RedirectToAction(nameof(Index));
        }




        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
