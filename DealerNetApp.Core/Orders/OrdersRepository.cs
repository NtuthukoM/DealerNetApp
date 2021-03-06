﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private DealerNetDbEntities db = new DealerNetDbEntities();

        private string GenerateOrderNum()
        {
            //Generate Order Number:
            string orderNum = "DN00001";
            var maxOrder = db.MovieOrders.OrderByDescending(d =>
            d.Id).FirstOrDefault();
            if (maxOrder != null)
            {
                string strNum = maxOrder.OrderNum.Substring(2);
                long num = 0;
                if (long.TryParse(strNum, out num))
                {
                    num++;
                    orderNum = "DN" + num.ToString("D5");
                }
            }
            return orderNum;
        }

        public MovieOrder CreateOrder()
        {
            string orderNum = GenerateOrderNum();

            MovieOrder order = new MovieOrder() {
                  OrderDate = DateTime.Now,
                  StatusId = 1, //New by default.
                  OrderNum = orderNum
            };

            db.MovieOrders.Add(order);
            db.SaveChanges();
            return order;
        }

        public MovieOrder AddMovie(int orderId, int movieId)
        {
            var order = db.MovieOrders.Find(orderId);
            var lineItem = db.OrderDetails.FirstOrDefault(d =>
            d.MovieOrderId == orderId && d.MovieId == movieId);
            if(lineItem == null)
            {
                lineItem = new OrderDetail() {
                    Qty = 0,
                    MovieOrderId = orderId,
                    MovieId = movieId
                };
                db.OrderDetails.Add(lineItem);
            }
            lineItem.Qty += 1;
            db.SaveChanges();

            decimal total = db.OrderDetails.Where(d =>
                d.MovieOrderId == orderId).Sum(m =>
                m.Qty * m.Movie.Price);
            order.TotalCost = total;
            db.SaveChanges();

            return new MovieOrder() {
                Id = order.Id,
            OrderDate = order.OrderDate,
            OrderNum = order.OrderNum,
            StatusId = order.StatusId,
            TotalCost = order.TotalCost
            };
        }

        public List<MovieOrder> GetOrders(string orderNum = "")
        {
            return db.MovieOrders.Where(m =>
            orderNum == "" ? true : m.OrderNum == orderNum)
            .ToList();
        }

        public MovieOrder RemoveMovie(int orderId, int movieId)
        {
            var order = db.MovieOrders.Find(orderId);
            var lineItem = db.OrderDetails.FirstOrDefault(d =>
            d.MovieOrderId == orderId && d.MovieId == movieId);
            if (lineItem != null)
            {
                lineItem.Qty -= 1;
                if (lineItem.Qty < 1)
                    db.OrderDetails.Remove(lineItem);
            }
            db.SaveChanges();

            decimal? total = null;
            try
            {
                total = db.OrderDetails.Where(d =>
                d.MovieOrderId == orderId).Sum(m =>
                m.Qty * m.Movie.Price);
            }
            catch (Exception) { }
            order.TotalCost = total.HasValue?total.Value:0;
            db.SaveChanges();
            return new MovieOrder()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderNum = order.OrderNum,
                StatusId = order.StatusId,
                TotalCost = order.TotalCost
            };
        }

        public MovieOrder UpdateOrder(MovieOrder order)
        {
            var currentItems = db.OrderDetails.Where(d =>
                    d.MovieOrderId == order.Id).ToList();
            db.OrderDetails.RemoveRange(currentItems);
            db.SaveChanges();
            MovieOrder model = db.MovieOrders.Find(order.Id);
            model.OrderDesc = order.OrderDesc;
            model.StatusId = order.StatusId;
            model.TotalCost = order.OrderDetails.Where(d =>
                d.MovieOrderId == order.Id).Sum(m =>
                m.Qty * m.Movie.Price);
            model.OrderDetails = order.OrderDetails;
            db.SaveChanges();
            order.TotalCost = model.TotalCost;          
            return model;
        }

        public MovieOrder GetOrderDetails(int id)
        {
            MovieOrder order = db.MovieOrders.Find(id);
            order.OrderDetails = db.OrderDetails.Where(o =>
                o.MovieOrderId == id).ToList();
            return order;
        }
    }
}
