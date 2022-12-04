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
                var teste = new Order();
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
        [Route("{id}")]
        public IActionResult UpdateStatus(int id, Status status)
        {
            try
            {
                _orderRepository.UpdateOrderStatus(id, status);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
