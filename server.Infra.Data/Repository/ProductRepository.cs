using server.Domain.Features.product;
using server.Domain.Interfaces;
using server.Infra.Data.DAO;
using System;
using System.Collections.Generic;

namespace server.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductDAO _productDAO;

        public ProductRepository()
        {
            _productDAO = new ProductDAO();
        }

        public void AddNewProduct(Product newProduct)
        {
            _productDAO.AddNewProduct(newProduct);
        }

        public void EditProduct(Product newProduct)
        {
            _productDAO.EditProduct(newProduct);
        }
        public Product SearchProductById(int id)
        {   
            var product = _productDAO.SearchProductById(id);
            return product;
        }
        public void ActiveDeactiveProduct(int id, bool active)
        {
            _productDAO.ActiveDeactiveProduct(id, active);
        }

        public void PatchInventory(Product product)
        {
            var productInventory = SearchProductById(product.Id);
            productInventory.SumProduct(product.QuantityInStock);
            _productDAO.ManageInventory(productInventory.Id, productInventory.QuantityInStock);
        }

        public List<Product> SearchProduct()
        {
            var list = _productDAO.SearchProduct();
            return list;
        }
        public List<Product> SearchProductActive()
        {
            var listActive = _productDAO.SearchProductActive();
            return listActive;
        }
    }
}
