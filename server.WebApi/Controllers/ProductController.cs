using Microsoft.AspNetCore.Mvc;
using server.Domain.Features.product;
using server.Domain.Interfaces;
using server.Infra.Data.Repository;
using System;

namespace server.WebApi.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : Controller
    {

        private IProductRepository _productRepository;
        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] Product newProduct)
        {
            try
            {
                if (newProduct.Validate())
                {
                    _productRepository.AddNewProduct(newProduct);
                }
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            try
            {
                return Ok(_productRepository.SearchProduct());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                return Ok(_productRepository.SearchProductById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("active")]
        public IActionResult GetProductActive()
        {
            try
            {
                return Ok(_productRepository.SearchProductActive());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public IActionResult PatchInventory([FromBody] Product product)
        {
            try
            {

                _productRepository.PatchInventory(product);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult PutProduct([FromBody] Product product)
        {
            try
            {
                _productRepository.EditProduct(product);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        [Route("active")]
        public IActionResult PatchActive([FromBody] Product product)
        {
            try
            {

                _productRepository.ActiveDeactiveProduct(product.Id, product.Active);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
