using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_BookStore.DataAccess.Repository;
using Project_BookStore.Models;
using Project_BookStore.Utility;

namespace Project_BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Roll_Admin)]
    public class OrderController : Controller
    { 

    private readonly IOrderHeaderRepository _orderHeaderRepo;
    private readonly IOrderDetailRepository _orderDetailRepo;
    public OrderController(IOrderHeaderRepository db, IOrderDetailRepository orderDetailRepository)
    {
            _orderHeaderRepo = db;
            _orderDetailRepo = orderDetailRepository;
    }
    
        public IActionResult Index()
        {
            List<OrderDetail> orderheader = _orderDetailRepo.GetAll(includeProperties: "OrderHeader,Product").ToList();
            return View(orderheader);
        }
    }
}
