using System;

namespace server.Domain.Features.product
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int QuantityInStock { get; set; }
        public bool Active { get; set; }
        public Product()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void SumProduct(int qtd) {
            
            this.QuantityInStock += qtd;
        }
        public void SubtractProduct(int qtd)
        {

            this.QuantityInStock -= qtd;
        }


        public bool Validate()
        {
            if (string.IsNullOrEmpty(Description))
            {
                throw new ProductException("O produto deve conter uma descrição!");
            }
            if (Description.Length < 3)
            {
                throw new ProductException("A descrição do produto deve conter três ou mais letras!");
            }
            if (Price < 1)
            {
                throw new ProductException("O preço deve ser superior a zero!");
            }
            if (ExpirationDate < DateTime.Now)
            {
                throw new ProductException("A data de vencimento não pode ser inferior a hoje!");
            }
            if (QuantityInStock != 0)
            {
                throw new ProductException("O produto deve iniciar com estoque zero!");
            }
            if (!Active)
            {
                throw new ProductException("O produto deve iniciar Ativo!");
            }
            return true;
        }

    }
}
