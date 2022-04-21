using Api.Models;

namespace Api.Repo
{
    public interface ICustomerRepo
    {
        public Task<List<Customer>> GetCustomersAsync();
        public Task<Customer> GetByIdAsnc(int id);
        public Task<Customer> CreateCustomerAsync(Customer Customer);
    }
}
