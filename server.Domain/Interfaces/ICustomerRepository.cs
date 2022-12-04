using server.Domain.Features.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        void CreateNewCustomer(Customer newCustomer);
        void EditCustomer(Customer newCustomer);
        void AddFidelityPoints(string cpf, double fidelityPoints);
        void DeleteCustomer(string cpf);
        List<Customer> SearchCustomer();
        Customer SearchCustomersById(int id);
    }
}
