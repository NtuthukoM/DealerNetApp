using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealerNetApp.Data;
using DealerNetApp.Core.Orders;

namespace DealerNetApp.Web.Controllers.Api
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private IOrdersRepository _ordersRepo;

        public OrdersController(IOrdersRepository ordersRepo)
        {
            _ordersRepo = ordersRepo;
        }

        // GET: api/Orders
        public IHttpActionResult Get(string orderNum = "")
        {
            return Ok(_ordersRepo.GetOrders(orderNum));
        }

        // GET: api/Orders/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_ordersRepo.GetOrderDetails(id));
        }


        // PUT: api/Orders/5
        public void Put(int id, [FromBody]MovieOrder order)
        {
            _ordersRepo.UpdateOrder(order);
        }

    }
}
