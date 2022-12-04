using server.Domain.Features.product;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace server.Infra.Data.DAO
{
    public class ProductDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=CAFETERIADB;integrated security=true;";

        public void AddNewProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"INSERT PRODUTOS VALUES (@DESCRICAO, @PRECO, @DATAVALIDADE, @QTDESTOQUE, @ATIVO)";

                    command.Parameters.AddWithValue("@DESCRICAO", product.Description);
                    command.Parameters.AddWithValue("@PRECO", product.Price);
                    command.Parameters.AddWithValue("@DATAVALIDADE", product.ExpirationDate);
                    command.Parameters.AddWithValue("@QTDESTOQUE", product.QuantityInStock);
                    command.Parameters.AddWithValue("@ATIVO", product.Active);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Product> SearchProduct()
        {
            var productList = new List<Product>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT *
                                    FROM PRODUTOS";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Product foundProduct = new Product();

                        foundProduct.Id = int.Parse(leitor["ID"].ToString());
                        foundProduct.Description = leitor["DESCRICAO"].ToString();
                        foundProduct.Price = double.Parse(leitor["PRECO"].ToString());
                        foundProduct.QuantityInStock = int.Parse(leitor["QTDESTOQUE"].ToString());
                        foundProduct.ExpirationDate = Convert.ToDateTime(leitor["DATAVALIDADE"].ToString());
                        foundProduct.Active = bool.Parse(leitor["ATIVO"].ToString());

                        productList.Add(foundProduct);
                    }
                }
            }

            return productList;
        }

        public List<Product> SearchProductActive()
        {
            {
                var productList = new List<Product>();

                using (var conexao = new SqlConnection(_connectionString))
                {
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;

                        string sql = @"SELECT *
                                    FROM PRODUTOS 
                                    WHERE ATIVO = 1 
                                    AND QTDESTOQUE > 0";

                        comando.CommandText = sql;

                        SqlDataReader leitor = comando.ExecuteReader();

                        while (leitor.Read())
                        {
                            Product foundProduct = new Product();

                            foundProduct.Id = int.Parse(leitor["ID"].ToString());
                            foundProduct.Description = leitor["DESCRICAO"].ToString();
                            foundProduct.Price = double.Parse(leitor["PRECO"].ToString());
                            foundProduct.QuantityInStock = int.Parse(leitor["QTDESTOQUE"].ToString());
                            foundProduct.ExpirationDate = Convert.ToDateTime(leitor["DATAVALIDADE"].ToString());
                            foundProduct.Active = bool.Parse(leitor["ATIVO"].ToString());

                            productList.Add(foundProduct);
                        }
                    }
                }

                return productList;
            }
        }

        public Product SearchProductById(int id)
        {
            var product = new Product();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"SELECT *
                                    FROM PRODUTOS 
                                    WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", id);
                    command.CommandText = sql;

                    SqlDataReader leitor = command.ExecuteReader();

                    while (leitor.Read())
                    {
                        Product foundProduct = new Product();

                        foundProduct.Id = int.Parse(leitor["ID"].ToString());
                        foundProduct.Description = leitor["DESCRICAO"].ToString();
                        foundProduct.Price = double.Parse(leitor["PRECO"].ToString());
                        foundProduct.QuantityInStock = int.Parse(leitor["QTDESTOQUE"].ToString());
                        foundProduct.ExpirationDate = Convert.ToDateTime(leitor["DATAVALIDADE"].ToString());
                        foundProduct.Active = bool.Parse(leitor["ATIVO"].ToString());

                        product = foundProduct;
                    }
                }
            }
            return product;
        }
        public void EditProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE PRODUTOS SET DESCRICAO = @DESCRICAO, PRECO = @PRECO, DATAVALIDADE = @DATAVALIDADE, QTDESTOQUE = @QTDESTOQUE, ATIVO = @ATIVO WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", product.Id);
                    command.Parameters.AddWithValue("@DESCRICAO", product.Description);
                    command.Parameters.AddWithValue("@PRECO", product.Price);
                    command.Parameters.AddWithValue("@DATAVALIDADE", product.ExpirationDate);
                    command.Parameters.AddWithValue("@QTDESTOQUE", product.QuantityInStock);
                    command.Parameters.AddWithValue("@ATIVO", product.Active);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void ActiveDeactiveProduct(int id, bool active)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE PRODUTOS SET ATIVO = @ATIVO WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ATIVO", active);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void ManageInventory(int id, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE PRODUTOS SET QTDESTOQUE = @QTDESTOQUE WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@QTDESTOQUE", quantity);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateInventory(int id, int quantity)
        {
                        
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE PRODUTOS SET QTDESTOQUE = @QTDESTOQUE WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@QTDESTOQUE", quantity);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
