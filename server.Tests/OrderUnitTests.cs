using NUnit.Framework;
using server.Domain.Features.customer;
using server.Domain.Features.order;
using server.Domain.Features.product;
using System;

namespace server.Tests
{
    public class OrderUnitTests
    {
        /*
        * Quando validar pedido 
        * E a quantidade de produtos for zero
        * Então deve lançar erro
        */
        [Test]
        public void Quando_ValidarPedido_E_AQuantidadeDeProdutosForZero_Entao_DeveLançarErro()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Eu nasci ontem?";
            cliente.Cpf = "10020030011";
            cliente.BirthDate = DateTime.Now.AddDays(-1);

            var produto = new Product();
            produto.Description= "Description";
            produto.Price = 10.50;
            produto.ExpirationDate = DateTime.Now.AddDays(11);
            produto.QuantityInStock = 1;
            produto.Active = true;

            var pedido = new Order();
            pedido.CustomerCpf = cliente.Cpf;
            pedido.CustomerName = cliente.Name;
            pedido.Product = produto;

            pedido.Quantity = 0;

            pedido.GetOrderValue();
            pedido.GetRequestDate();
            pedido.CurrentStatus = Status.InProgress;

            // act

            OrderException ex = Assert.Throws<OrderException>(() => pedido.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("A quantidade de produtos não pode ser zero ou inferior!"));
        }
        /*
        * Quando validar pedido 
        * E a quantidade de produtos for menor que zero
        * Então deve lançar erro
        */
        [Test]
        public void Quando_ValidarProduto_E_AQuantidadeDeProdutosForMenorQueZero_Entao_DeveResultarEmErro()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Eu nasci ontem?";
            cliente.Cpf = "10020030011";
            cliente.BirthDate = DateTime.Now.AddDays(-1);

            var produto = new Product();
            produto.Description = "Description";
            produto.Price = 10.50;
            produto.ExpirationDate = DateTime.Now.AddDays(11);
            produto.QuantityInStock = 1;
            produto.Active = true;

            var pedido = new Order();
            pedido.CustomerCpf = cliente.Cpf;
            pedido.CustomerName = cliente.Name;
            pedido.Product = produto;

            pedido.Quantity = -1;

            pedido.GetOrderValue();
            pedido.GetRequestDate();
            pedido.CurrentStatus = Status.InProgress;

            // act

            OrderException ex = Assert.Throws<OrderException>(() => pedido.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("A quantidade de produtos não pode ser zero ou inferior!"));
        }

        /*
        * Quando validar produto
        * E o status atual for finalizado
        * Então deve lançar erro
        */
        [Test]
        public void Quando_ValidarProduto_E_OEstatusAtualDoProdutoForFinalizado_Entao_DeveResultarEmErro()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Eu nasci ontem?";
            cliente.Cpf = "10020030011";
            cliente.BirthDate = DateTime.Now.AddDays(-1);

            var produto = new Product();
            produto.Description = "Description";
            produto.Price = 10.50;
            produto.ExpirationDate = DateTime.Now.AddDays(11);
            produto.QuantityInStock = 1;
            produto.Active = true;

            var pedido = new Order();
            pedido.CustomerCpf = cliente.Cpf;
            pedido.CustomerName = cliente.Name;
            pedido.Product = produto;
            pedido.Quantity = 1;
            pedido.GetOrderValue();
            pedido.GetRequestDate();
            pedido.CurrentStatus = Status.finished;

            // act

            OrderException ex = Assert.Throws<OrderException>(() => pedido.FinishedOrder());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O pedido foi finalizado, não pode ser alterado!"));
        }

        /*
        * Quando criar um pedido
        * E quantidade de produtos for maior que quantidade de produtos em estoque
        * Então deve lançar erro
        */
        [Test]
        public void Quando_CriarPedido_E_QuantidadeDeProdutosForMaiorQueQuantidadeDeProdutosEmEstoque_Entao_DeveResultarEmErro()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Eu nasci ontem?";
            cliente.Cpf = "10020030011";
            cliente.BirthDate = DateTime.Now.AddDays(-1);

            var produto = new Product();
            produto.Description = "Description";
            produto.Price = 10.50;
            produto.ExpirationDate = DateTime.Now.AddDays(11);
            produto.QuantityInStock = 1;
            produto.Active = true;

            var pedido = new Order();
            pedido.CustomerCpf = cliente.Cpf;
            pedido.CustomerName = cliente.Name;
            pedido.Product = produto;
            pedido.Quantity = 2;
            pedido.GetOrderValue();
            pedido.GetRequestDate();
            pedido.CurrentStatus = Status.InTransit;

            // act

            OrderException ex = Assert.Throws<OrderException>(() => pedido.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("A quantidade do pedido não pode ser maior que a quantidade do produto em estoque"));
        }

        /*
        * Quando validar produto
        * E a quantidade for maior que zero
        * E o valor do pedido for maior que zero
        * E estatus atual for diferente de finalizado
        * O caminho é feliz
        */
        [Test]
        public void CaminhoFeliz()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Eu nasci ontem?";
            cliente.Cpf = "10020030011";
            cliente.BirthDate = DateTime.Now.AddDays(-1);

            var produto = new Product();
            produto.Description = "Description";
            produto.Price = 10.50;
            produto.ExpirationDate = DateTime.Now.AddDays(11);
            produto.QuantityInStock = 1;
            produto.Active = true;

            var pedido = new Order();
            pedido.CustomerCpf = cliente.Cpf;
            pedido.CustomerName = cliente.Name;
            pedido.Product = produto;

            pedido.Quantity = 1;

            pedido.GetOrderValue();
            pedido.GetRequestDate();
            pedido.CurrentStatus = Status.InProgress;

            // action
            var ehValido = pedido.Validate();

            // assert
            Assert.IsTrue(ehValido);
        }
    }
}
