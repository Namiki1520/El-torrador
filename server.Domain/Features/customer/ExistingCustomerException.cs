using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Domain.Features.customer
{
    public class ExistingCustomerException : Exception
    {
        public ExistingCustomerException() : base("Cliente já cadastrado")
        {

        }
    }
}
