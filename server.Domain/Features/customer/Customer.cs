using System;

namespace server.Domain.Features.customer
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public double FidelityPoints { get; set; }
        public Customer()
        {

        }

        public void Fidelity(double value)
        {
            FidelityPoints += (value * 2);
        }

        public bool Validate()
        {
            if(Name.Length < 3)
            {
                throw new CustomerException("O nome deve conter no mínomo 3 caracteres!");
            }
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new CustomerException("O nome do cliente não pode ser vazio!");
            }
            if (string.IsNullOrWhiteSpace(this.Cpf))
            {
                throw new CustomerException("O cpf não pode ser vazio.");
            }
            if (this.Cpf.Length != 11)
            {
                throw new CustomerException("O CPF deve conter 11 dígitos númericos");
            }
            if (this.BirthDate > DateTime.Now)
            {
                throw new CustomerException("A data de nascimento deve ser anterior a hoje!");
            }
            return true;
        }
    }
}
