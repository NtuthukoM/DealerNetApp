using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Orders
{
    public interface IOrderStatusRepository
    {
        List<OrderStatu> GetOrderStatuses();
    }
}
