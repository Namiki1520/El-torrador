using server.Domain.Features.customer;
using server.Domain.Interfaces;
using server.Infra.Data.DAO;
using System.Collections.Generic;

namespace server.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerDAO _customerDAO;

        public CustomerRepository()
        {
            _customerDAO = new CustomerDAO();
        }
        public void CreateNewCustomer(Customer newCustomer)
        {
            var verificationList = _customerDAO.SearchCustomers();

            if (verificationList.Exists(i => i.Cpf == newCustomer.Cpf))
            {
                throw new ExistingCustomerException();
            }

            _customerDAO.InsertCustomer(newCustomer);
        }

        public void DeleteCustomer(string cpf)
        {
            _customerDAO.DeleteCustomer(cpf);
        }

        public void EditCustomer(Customer customer)
        {
            _customerDAO.EditCustomer(customer);
        }
        public void AddFidelityPoints(string cpf, double fidelityPoints)
        {
            _customerDAO.AddFidelityPoints(cpf, fidelityPoints);
        }

        public List<Customer> SearchCustomer()
        {
            var List = _customerDAO.SearchCustomers();
            return List;
        }

        public Customer SearchCustomersById(int id)
        {
            var customer = _customerDAO.SearchCustomersById(id);
            return customer;
        }
    }
}
