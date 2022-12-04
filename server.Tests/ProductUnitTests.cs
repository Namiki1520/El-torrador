using NUnit.Framework;
using server.Domain.Features.ProductDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Tests
{
    public class ProductUnitTests
    {
        /*
         * Quando validar produto
         * E nome não for informado
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_NomeNaoForInformado_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(1);
            produto.Price = 35;
            produto.QuantityInStock = 10;
            produto.Active = true;

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O produto deve conter uma descrição!"));
        }

        /*
         * Quando validar produto
         * E nome for vazio
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_NomeForMenorQueTres_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(1);
            produto.Price = 35;
            produto.QuantityInStock = 10;
            produto.Active = true;

            produto.Description = "Ca";

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("A descrição do produto deve conter três ou mais letras!"));
        }

        /*
         * Quando validar produto
         * E preço for menor que zero
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_PrecoForMenorQueZero_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(1);
            produto.Price = -1;
            produto.QuantityInStock = 10;
            produto.Active = true;

            produto.Description = "Capuccino Avelã";

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O preço deve ser superior a zero!"));
        }

        /*
         * Quando validar produto
         * E preço for igual a zero
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_PrecoForIgualAZero_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(1);
            produto.Price = 0;
            produto.QuantityInStock = 10;
            produto.Active = true;

            produto.Description = "Capuccino Avelã";

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O preço deve ser superior a zero!"));
        }

        /*
         * Quando validar produto
         * E quantidade em estoque igual a zero
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_CriarProduto_E_QuantidadeEmEstoqueDiferenteDeZero_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(1);
            produto.Price = 35;
            produto.QuantityInStock = 5;
            produto.Active = true;

            produto.Description = "Capuccino Avelã";

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O produto deve iniciar com estoque zero!"));
        }

        /*
         * Quando validar produto
         * E quantidade em estoque menor zero
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_QuantidadeEmEstoqueMenorQueZero_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(1);
            produto.Price = 35;
            produto.QuantityInStock = -1;
            produto.Active = true;

            produto.Description = "Capuccino Avelã";

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O produto deve iniciar com estoque zero!")); ;
        }

        /*
         * Quando validar produto
         * E produto estiver vencido
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_ProdutoEstiverVencido_Entao_DeveResultarEmFalse()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(-1);
            produto.Price = 35;
            produto.QuantityInStock = 10;
            produto.Active = true;

            produto.Description = "Capuccino Avelã";

            // act

            ProductException ex = Assert.Throws<ProductException>(() => produto.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("A data de vencimento não pode ser inferior a hoje!"));
        }

        /*
         * Quando validar produto
         * E nome é diferente de vazio
         * E preço maior que zero
         * E não estiver vencido
         * E quantidade em estoque for maior que zero
         * O caminho é feliz
         */
        [Test]
        public void CaminhoFeliz()
        {
            // arrange
            var produto = new Product();
            produto.ExpirationDate = DateTime.Now.AddDays(3);
            produto.Price = 35;
            produto.QuantityInStock = 0;
            produto.Active = true;

            produto.Description = "Capuccino Avelã";

            // action
            var ehValido = produto.Validate();

            // assert
            Assert.IsTrue(ehValido);
        }
    }
}
