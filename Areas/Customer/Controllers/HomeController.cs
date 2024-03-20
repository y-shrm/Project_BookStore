using Microsoft.AspNetCore.Mvc;
using Project_BookStore.DataAccess.Data;
using Project_BookStore.DataAccess.Repository;
using Project_BookStore.Models;
using System.Diagnostics;

namespace Project_BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepo;
        
        private readonly ICategoryRepository _categoryRepo;

        public HomeController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _productRepo.GetAll(includeProperties:"Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            Product product = _productRepo.Get(u => u.Id==id, includeProperties: "Category");
            return View(product);
        }

        public IActionResult Privacy()
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
