using server.Domain.Features.customer;
using server.Domain.Features.order;
using server.Domain.Interfaces;
using server.Infra.Data.DAO;
using System;
using System.Collections.Generic;

namespace server.Infra.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private CustomerDAO _customerDAO = new CustomerDAO();
        private OrderDAO _orderDAO = new OrderDAO();
        private ProductDAO _productDAO = new ProductDAO();

        public void AddOrder(Order newOrder)
        {
            newOrder.GetOrderValue();
            newOrder.GetRequestDate();
            newOrder.Product.SubtractProduct(newOrder.Quantity);


            if (newOrder.Validate())
            {
                Customer customer = _customerDAO.SearchCustomersByCpf(newOrder.CustomerCpf);
                customer.Fidelity(newOrder.OrderValue);
                _customerDAO.AddFidelityPoints(newOrder.CustomerCpf, customer.FidelityPoints);
                _orderDAO.AddOrderWithCpf(newOrder);
                _productDAO.UpdateInventory(newOrder.Product.Id, newOrder.Product.QuantityInStock);
            }
            else
            {
                _orderDAO.AddOrderWithoutCpf(newOrder);
                _productDAO.UpdateInventory(newOrder.Product.Id, newOrder.Product.QuantityInStock);
            }
        }

        public void DeleteOrder(int id)
        {
            var pedido = _orderDAO.GetOrderByID(id);
            if (pedido.FinishedOrder())
            {
                _orderDAO.DeleteOrder(id);
            }

        }

        public List<Order> GetAllOrders()
        {
            var list = _orderDAO.GetAllOrder();
            return list;
        }

        public Order GetOrderById(int id)
        {
            var order = _orderDAO.GetOrderByID(id);
            return order;
        }

        public void UpdateOrderStatus(int id, Status status)
        {
            var pedido = _orderDAO.GetOrderByID(id);
            if (pedido.FinishedOrder())
            {
                _orderDAO.UpdateOrderStatus(id, status);
            }
        }
    }
}
