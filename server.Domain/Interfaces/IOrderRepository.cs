using server.Domain.Features.order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order newOrder);
        void DeleteOrder(int id);
        void UpdateOrderStatus(int id, Status status);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}
