﻿using server.Domain.Features.product;
using server.Domain.Features.customer;
using System;

namespace server.Domain.Features.order
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerCpf { get; set; }

        public string? CustomerName { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double OrderValue { get; set; }

        public DateTime RequestDate { get; set; }

        public Status CurrentStatus { get; set; }

        public Order()
        {

        }
        
        public void GetOrderValue()
        {
            OrderValue = (Product.Price * Quantity);
        }

        public void GetRequestDate()
        {
            RequestDate = DateTime.Now;
        }

        public bool Validate()
        {
            if (Quantity < 1)
            {
                throw new OrderException("A quantidade de produtos não pode ser zero ou inferior!");
            }
            if (OrderValue < 0)
            {
                throw new OrderException("O valor do pedido não pode ser zero ou inferior!");
            }
            if(CurrentStatus == Status.finished)
            {
                throw new OrderException("O pedido foi finalizado, não pode ser alterado!");
            }
            if(Quantity > Product.QuantityInStock)
            {
                throw new OrderException("A quantidade do pedido não pode ser maior que a quantidade do produto em estoque");
            }
            if (string.IsNullOrWhiteSpace(CustomerCpf) || string.IsNullOrEmpty(CustomerCpf) || CustomerCpf == "string")
            {
                return false;
            }
            return true;
        }
    }
    public enum Status
    {
        InProgress,
        InTransit,
        finished
    }
}
