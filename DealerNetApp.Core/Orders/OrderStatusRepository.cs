using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Orders
{
    public class OrderStatusRepository: IOrderStatusRepository
    {
        private DealerNetDbEntities db = new DealerNetDbEntities();

        public List<OrderStatu> GetOrderStatuses()
        {
            return db.OrderStatus.ToList();
        }
    }
}
