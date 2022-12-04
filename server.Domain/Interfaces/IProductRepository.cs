using server.Domain.Features.product;
using System.Collections.Generic;

namespace server.Domain.Interfaces
{
    public interface IProductRepository
    {
        void AddNewProduct(Product newProduct);
        void EditProduct(Product newProduct);
        void PatchInventory(Product product);
        void ActiveDeactiveProduct(int id, bool active);
        void DeleteProduct(Product newProduct);
        List<Product> SearchProduct();

        Product SearchProductById(int id);
        List<Product> SearchProductActive();
    }
}
