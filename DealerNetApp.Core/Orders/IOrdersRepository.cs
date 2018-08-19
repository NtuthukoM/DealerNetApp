using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Orders
{
    public interface IOrdersRepository
    {
        List<MovieOrder> GetOrders(string orderNum = "");
        MovieOrder AddMovie(int orderId, int movieId);
        MovieOrder RemoveMovie(int orderId, int movieId);
        MovieOrder UpdateOrder(MovieOrder order);
        MovieOrder GetOrderDetails(int id);
        MovieOrder CreateOrder();
    }
}
