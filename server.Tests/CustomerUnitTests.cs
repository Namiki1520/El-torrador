using NUnit.Framework;
using server.Domain.Features.customer;
using server.Domain.Features.product;
using System;

namespace server.Tests
{
    public class CustomerUnitTests
    {
        /*
         * Quando validar cliente
         * E nome menor que três caracteres
         * Então deve resultar em cliente inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_NomeMenorQueTres_Entao_DeveEstourarExcecao()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "To";
            cliente.Cpf = "12345678912";
            cliente.BirthDate= DateTime.Now.AddDays(-3);

            // act

            CustomerException ex = Assert.Throws<CustomerException>(() => cliente.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O nome deve conter no mínomo 3 caracteres!"));
        }

        /*
         * Quando validar cliente
         * E nome for vazio 
         * Então deve resultar em cliente inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_NomeNaoForInformado_Entao_DeveResultarEmFalse()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = " ";
            cliente.Cpf = "12345678912";
            cliente.BirthDate = DateTime.Now.AddDays(-3);

            // act

            CustomerException ex = Assert.Throws<CustomerException>(() => cliente.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O nome deve conter no mínomo 3 caracteres!"));
        }

        /*
         * Quando validar cliente
         * E cpf não for informado 
         * Então deve resultar em cliente inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_CpfNaoForInformado_Entao_DeveResultarEmFalse()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Tobias";
            cliente.Cpf = " ";
            cliente.BirthDate = DateTime.Now.AddDays(-3);

            // act

            CustomerException ex = Assert.Throws<CustomerException>(() => cliente.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O cpf não pode ser vazio."));
        }

        /*
         * Quando validar cliente
         * E cpf for diferente de 11 digitos númericos
         * Então deve resultar em cliente inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_CpfForDiferenteDeOnze_Entao_DeveResultarEmFalse()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Tobias";
            cliente.Cpf = "123123";
            cliente.BirthDate = DateTime.Now.AddDays(-3);

            // act

            CustomerException ex = Assert.Throws<CustomerException>(() => cliente.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("O CPF deve conter 11 dígitos númericos"));
        }

        /*
         * Quando validar produto
         * E data de nascimento for maior que hoje
         * Então deve resultar em produto inválido
         */
        [Test]
        public void Quando_ValidarProduto_E_DataDeNascimentoForMaiorQueHoje_Entao_DeveResultarEmFalse()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Tobias";
            cliente.Cpf = "12312312312";
            cliente.BirthDate = DateTime.Now.AddDays(1);

            // act

            CustomerException ex = Assert.Throws<CustomerException>(() => cliente.Validate());


            // assert

            Assert.That(ex.Message, Is.EqualTo("A data de nascimento deve ser anterior a hoje!"));
        }

        /*
         * Quando cliente comprar uma unidade de produto 
         * o valor do produto for 10 reais
         * Então deve ganhar 20 pontos de fidelidade
         */
        [Test]
        public void Quando_ClienteComprarUmaUnidadeDeProduto_E_OValorDoProdutoForDezReais_Entao_DeveGanharVintePontosDeFidelidade()
        {
            // arrange
            var produto = new Product();
            produto.Description = "Description";
            produto.Price = 10;
            produto.ExpirationDate = DateTime.Now.AddDays(11);
            produto.QuantityInStock = 1;
            produto.Active = true;

            var cliente = new Customer();
            cliente.Name = "Tobias";
            cliente.Cpf = "12312312312";
            cliente.BirthDate = DateTime.Now.AddDays(1);
            cliente.Fidelity(produto.Price);

            // act
            var PontosFidelidade = 20;


            // assert

            Assert.AreEqual(PontosFidelidade,cliente.FidelityPoints);
        }

        /*
         * Quando validar produto
         * E nome é diferente de vazio
         * E cpf é válido
         * E data de nascimento é válida
         * O caminho é feliz
         */
        [Test]
        public void CaminhoFeliz()
        {
            // arrange
            var cliente = new Customer();
            cliente.Name = "Saibot";
            cliente.Cpf = "12312312312";
            cliente.BirthDate = DateTime.Now.AddMonths(-1);

            // action
            var ehValido = cliente.Validate();

            // assert
            Assert.IsTrue(ehValido);
        }
    }

}
