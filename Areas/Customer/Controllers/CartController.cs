using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_BookStore.DataAccess.Repository;
using Project_BookStore.Models;
using Project_BookStore.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace Project_BookStore.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize(Roles = StaticDetail.Roll_Customer)]
	public class CartController : Controller
	{
		private readonly IProductRepository _productRepo;

		private readonly ICategoryRepository _categoryRepo;

		private readonly IShoppingCartRepository _shoppingCartRepo;

		private readonly IApplicationUserRepository _applicationUserRepo;

		private readonly IOrderHeaderRepository _orderheaderRepo;

		private readonly IOrderDetailRepository _orderdetailRepo;


		//private readonly ShoppingCart shoppingcart;


		[BindProperty]
		public ShoppingCartVM ShoppingCartVM { get; set; }

		public CartController(IProductRepository productRepo,
								ICategoryRepository categoryRepo,
									IShoppingCartRepository shoppingCart,
										IApplicationUserRepository applicationUserRepo,
										IOrderHeaderRepository orderHeaderRepository,
										IOrderDetailRepository orderDetailRepository)
		{
			_productRepo = productRepo;
			_categoryRepo = categoryRepo;
			_shoppingCartRepo = shoppingCart;
			_applicationUserRepo = applicationUserRepo;
			_orderheaderRepo = orderHeaderRepository;
			_orderdetailRepo = orderDetailRepository;
		}


		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;

			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _shoppingCartRepo.GetAll(u => u.ApplicationUsderId == userId,
				includeProperties: "Product"),
				OrderHeader = new()

			};

			ShoppingCartVM.OrderHeader.OrderTotal = 0;
			ShoppingCartVM.TotalItemCount = 0;

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{

				double? price = cart.Product.Price;


				ShoppingCartVM.OrderHeader.OrderTotal += (price * cart.Count);
				ShoppingCartVM.TotalItemCount += cart.Count;


			}

			return View(ShoppingCartVM);
		}


		public IActionResult Summary()
		{

			var claimsIdentity = (ClaimsIdentity)User.Identity;

			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM = new()
			{
				ShoppingCartList = _shoppingCartRepo.GetAll(u => u.ApplicationUsderId == userId,
				includeProperties: "Product"),
				OrderHeader = new()

			};

			ShoppingCartVM.OrderHeader.ApplicationUser = _applicationUserRepo.Get(u => u.Id == userId);
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;

			ShoppingCartVM.OrderHeader.OrderTotal = 0;

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{

				double? price = cart.Product.Price;


				ShoppingCartVM.OrderHeader.OrderTotal += (price * cart.Count);


			}
			return View(ShoppingCartVM);
		}


		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPOST()
		{

			var claimsIdentity = (ClaimsIdentity)User.Identity;

			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingCartVM.ShoppingCartList = _shoppingCartRepo.GetAll(u => u.ApplicationUsderId == userId,
				includeProperties: "Product");

			ShoppingCartVM.OrderHeader.OderDate = DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

			ApplicationUser applicationUser = _applicationUserRepo.Get(u => u.Id == userId);


			ShoppingCartVM.OrderHeader.OrderTotal = 0;

			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{

				double? price = cart.Product.Price;


				ShoppingCartVM.OrderHeader.OrderTotal += (price * cart.Count);


			}

			_orderheaderRepo.Add(ShoppingCartVM.OrderHeader);
			_orderheaderRepo.Save();

			foreach(var cart in ShoppingCartVM.ShoppingCartList)
			{
				OrderDetail orderDetail = new()
				{
					ProductId = cart.ProductId,
					OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
					Price = cart.Product.Price,
					Count = cart.Count,
				};
				_orderdetailRepo.Add(orderDetail);
				_orderdetailRepo.Save();
			}

			return RedirectToAction(nameof(OrderConfirmation), new { id =ShoppingCartVM.OrderHeader.Id});
		}


		public IActionResult OrderConfirmation(int id)
		{
			return View(id);
		}




		public IActionResult Plus(int cartId)
		{
			var cartFromDb = _shoppingCartRepo.Get(u => u.Id == cartId);
			cartFromDb.Count += 1;
			_shoppingCartRepo.Update(cartFromDb);
			_shoppingCartRepo.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Minus(int cartId)
		{
			var cartFromDb = _shoppingCartRepo.Get(u => u.Id == cartId);
			if (cartFromDb.Count <= 1)
			{
				_shoppingCartRepo.Remove(cartFromDb);
			}
			else
			{
				cartFromDb.Count -= 1;
				_shoppingCartRepo.Update(cartFromDb);
			}


			_shoppingCartRepo.Save();
			return RedirectToAction("Index");
		}

		public IActionResult Remove(int cartId)
		{
			var cartFromDb = _shoppingCartRepo.Get(u => u.Id == cartId);

			_shoppingCartRepo.Remove(cartFromDb);

			_shoppingCartRepo.Save();
			return RedirectToAction("Index");
		}


	}
}
