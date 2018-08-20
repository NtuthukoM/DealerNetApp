using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealerNetApp.Core.Orders;

namespace DealerNetApp.Web.Controllers.Api
{
    [Authorize]
    public class StatusesController : ApiController
    {
        private IOrderStatusRepository _orderStatus;

        public StatusesController(IOrderStatusRepository orderStatus)
        {
            _orderStatus = orderStatus;
        }
        // GET: api/Statuses
        public IHttpActionResult Get()
        {
            var statuses = _orderStatus.GetOrderStatuses();
            return Ok(statuses);
        }
    }
}
