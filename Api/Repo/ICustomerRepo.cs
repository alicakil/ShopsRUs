using Api.Models;

namespace Api.Repo
{
    public interface ICustomerRepo
    {
        public List<Customer> GetCustomers();
        public Customer GetById(int id);
        public Customer CreateCustomer(Customer Customer);
    }
}
