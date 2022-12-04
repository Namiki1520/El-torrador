using Microsoft.AspNetCore.Mvc;
using server.Domain.Features.order;
using server.Domain.Interfaces;
using server.Infra.Data.Repository;
using System;

namespace server.WebApi.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private IOrderRepository _orderRepository;

        public OrderController()
        {
            _orderRepository = new OrderRepository();
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] Order newOrder)
        {
            try
            {
                _orderRepository.AddOrder(newOrder);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            try
            {
                return Ok(_orderRepository.GetAllOrders());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                return Ok(_orderRepository.GetOrderById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderRepository.DeleteOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public IActionResult UpdateStatus([FromBody] Order order)
        {
            try
            {
                _orderRepository.UpdateOrderStatus(order.Id, order.CurrentStatus);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
