using Microsoft.AspNetCore.Mvc;
using server.Domain.Features.customer;
using server.Domain.Interfaces;
using server.Infra.Data.Repository;
using System;

namespace server.WebApi.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _clientRepository;
        public CustomerController()
        {
            _clientRepository = new CustomerRepository();
        }

        [HttpPost]
        public IActionResult PostCustomer([FromBody] Customer newClient)
        {
            try
            {
                if (newClient.Validate())
                {
                    _clientRepository.CreateNewCustomer(newClient);
                }
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            try
            {
                return Ok(_clientRepository.SearchCustomer());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult PutCustomer([FromBody] Customer newClient)
        {
            try
            {
                _clientRepository.EditCustomer(newClient);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{cpf}")]
        public IActionResult DeleteCustomer(string cpf)
        {
            try
            {
                _clientRepository.DeleteCustomer(cpf);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                return Ok(_clientRepository.SearchCustomersById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
