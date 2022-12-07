using server.Domain.Features.customer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace server.Infra.Data.DAO
{
    public class CustomerDAO
    {
        private const string _connectionString = @"server=.\SQLexpress;initial catalog=CAFETERIADB;integrated security=true;";

        public void InsertCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"INSERT CLIENTES VALUES (@NOME, @CPF, @DATANASCIMENTO, @PONTOSFIDELIDADE)";

                    command.Parameters.AddWithValue("@NOME", customer.Name);
                    command.Parameters.AddWithValue("@CPF", customer.Cpf);
                    command.Parameters.AddWithValue("@DATANASCIMENTO", customer.BirthDate);
                    command.Parameters.AddWithValue("@PONTOSFIDELIDADE", customer.FidelityPoints);

                    
                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(string cpf)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"DELETE FROM CLIENTES WHERE CPF = @CPF;
                                   DELETE FROM PEDIDOS WHERE CPF_CLIENTE = @CPF;";

                    command.Parameters.AddWithValue("@CPF", cpf);


                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }

        public Customer SearchCustomersByCpf(string cpf)
        {
            var customer = new Customer();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"SELECT *
                                    FROM CLIENTES WHERE CPF = @CPF";

                    command.CommandText = sql;

                    command.Parameters.AddWithValue("@CPF", cpf);

                    SqlDataReader leitor = command.ExecuteReader();
                    
                    while (leitor.Read())
                    {
                        Customer foundClient = new Customer();

                        foundClient.Id = int.Parse(leitor["ID"].ToString());
                        foundClient.Name = leitor["NOME"].ToString();
                        foundClient.Cpf = leitor["CPF"].ToString();
                        foundClient.BirthDate = Convert.ToDateTime(leitor["DATANASCIMENTO"].ToString());
                        foundClient.FidelityPoints = double.Parse(leitor["PONTOSFIDELIDADE"].ToString());
                        customer = foundClient;

                    }
                }
            }
            if(customer != null)
            {
                return customer;
            }
            return null;
        }

        public Customer SearchCustomersById(int id)
        {
            var customer = new Customer();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"SELECT *
                                    FROM CLIENTES WHERE Id = @ID";

                    command.CommandText = sql;

                    command.Parameters.AddWithValue("@ID", id);

                    SqlDataReader leitor = command.ExecuteReader();

                    while (leitor.Read())
                    {
                        Customer foundClient = new Customer();

                        foundClient.Id = int.Parse(leitor["ID"].ToString());
                        foundClient.Name = leitor["NOME"].ToString();
                        foundClient.Cpf = leitor["CPF"].ToString();
                        foundClient.BirthDate = Convert.ToDateTime(leitor["DATANASCIMENTO"].ToString());
                        foundClient.FidelityPoints = double.Parse(leitor["PONTOSFIDELIDADE"].ToString());
                        customer = foundClient;

                    }
                }
            }
            if (customer != null)
            {
                return customer;
            }
            return null;
        }

        public void EditCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE CLIENTES SET NOME = @NOME, CPF = @CPF, DATANASCIMENTO = @DATANASCIMENTO WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", customer.Id);
                    command.Parameters.AddWithValue("@NOME", customer.Name);
                    command.Parameters.AddWithValue("@CPF", customer.Cpf);
                    command.Parameters.AddWithValue("@DATANASCIMENTO", customer.BirthDate);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddFidelityPoints(string cpf, double fidelityPoints)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string sql = @"UPDATE CLIENTES SET PONTOSFIDELIDADE = @PONTOSFIDELIDADE WHERE CPF = @CPF";

                    command.Parameters.AddWithValue("@CPF", cpf);
                    command.Parameters.AddWithValue("@PONTOSFIDELIDADE", fidelityPoints);

                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Customer> SearchCustomers()
        {
            var customerList = new List<Customer>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT *
                                    FROM CLIENTES";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        Customer foundClient = new Customer();

                        foundClient.Id = int.Parse(leitor["ID"].ToString());
                        foundClient.Name = leitor["NOME"].ToString();
                        foundClient.Cpf = leitor["CPF"].ToString();
                        foundClient.BirthDate = Convert.ToDateTime(leitor["DATANASCIMENTO"].ToString());
                        foundClient.FidelityPoints = double.Parse(leitor["PONTOSFIDELIDADE"].ToString());
                     
                        customerList.Add(foundClient);
                    }
                }
            }

            return customerList;
        }
    }
}
