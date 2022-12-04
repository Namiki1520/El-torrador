using server.Domain.Features.customer;
using server.Domain.Features.order;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace server.Infra.Data.DAO
{
    public class OrderDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=CAFETERIADB;integrated security=true;";

        public void AddOrderWithCpf(Order newOrder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"INSERT INTO PEDIDOS VALUES (@CPFCLIENTE, @PRODUTOID, @DATAPEDIDO, @QTDPRODUTO, @STATUSPEDIDO, @VALORTOTAL)";


                    command.Parameters.AddWithValue("@CPFCLIENTE", newOrder.CustomerCpf);
                    command.Parameters.AddWithValue("@PRODUTOID", newOrder.Product.Id);
                    command.Parameters.AddWithValue("@DATAPEDIDO", newOrder.RequestDate);
                    command.Parameters.AddWithValue("@QTDPRODUTO", newOrder.Quantity);
                    command.Parameters.AddWithValue("@STATUSPEDIDO", newOrder.CurrentStatus);
                    command.Parameters.AddWithValue("@VALORTOTAL", newOrder.OrderValue);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddOrderWithoutCpf(Order newOrder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"INSERT INTO PEDIDOS 
                                    (PRODUTO_ID, DATAPEDIDO, QTDPRODUTO, STATUSPEDIDO, VALORTOTAL)
                                    VALUES
                                (@PRODUTOID, @DATAPEDIDO, @QTDPRODUTO, @STATUSPEDIDO, @VALORTOTAL)";

                    command.Parameters.AddWithValue("@PRODUTOID", newOrder.Product.Id);
                    command.Parameters.AddWithValue("@DATAPEDIDO", newOrder.RequestDate);
                    command.Parameters.AddWithValue("@QTDPRODUTO", newOrder.Quantity);
                    command.Parameters.AddWithValue("@STATUSPEDIDO", newOrder.CurrentStatus);
                    command.Parameters.AddWithValue("@VALORTOTAL", newOrder.OrderValue);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOrder(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"DELETE FROM PEDIDOS WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", id);


                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Order> GetAllOrder()
        {
            var orderList = new List<Order>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT P.ID as IDPEDIDO,
                                    C.NOME,
                                    P.DATAPEDIDO,
                                    P.VALORTOTAL,
                                    P.STATUSPEDIDO
                                    FROM PEDIDOS P 
                                    LEFT JOIN CLIENTES C
                                    ON C.CPF = P.CPF_CLIENTE;";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Order foundOrder = new Order();

                        foundOrder.Id = int.Parse(leitor["IDPEDIDO"].ToString());
                        if (leitor["NOME"].ToString() != null)
                        {
                            foundOrder.CustomerName = leitor["NOME"].ToString();
                        }
                        foundOrder.RequestDate = Convert.ToDateTime(leitor["DATAPEDIDO"].ToString());
                        foundOrder.OrderValue = Convert.ToDouble(leitor["VALORTOTAL"].ToString());
                        foundOrder.CurrentStatus = (Status)int.Parse(leitor["STATUSPEDIDO"].ToString());
                        
                        
                        orderList.Add(foundOrder);
                    }
                }
            }

            return orderList;
        }

        public void UpdateOrderStatus(int id, Status status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE PEDIDOS SET STATUSPEDIDO = @STATUSPEDIDO WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@STATUSPEDIDO", status);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
