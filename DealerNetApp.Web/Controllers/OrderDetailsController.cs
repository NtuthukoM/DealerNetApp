using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealerNetApp.Data;
using DealerNetApp.Core.Orders;

namespace DealerNetApp.Web.Controllers
{
    public class OrderDetailsController : Controller
    {
        private IOrdersRepository _ordersRepo;

        public OrderDetailsController(IOrdersRepository ordersRepo)
        {
            _ordersRepo = ordersRepo;
        }

        // GET: OrderDetails
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult RemoveMovie(int orderId, int movieId)
        {
            var movieOrder = _ordersRepo.RemoveMovie(orderId,movieId);
            return Json(movieOrder, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddMovie(int movieId,int? orderId = null)
        {
            int oId = 0;
            if (orderId.HasValue)
            {
                oId = orderId.Value;
            }
            else
            {
                var order = _ordersRepo.CreateOrder();
                oId = order.Id;
            }
            var movieOrder = _ordersRepo.AddMovie(oId, movieId);
            return Json(movieOrder, JsonRequestBehavior.AllowGet);
        }
    }
}