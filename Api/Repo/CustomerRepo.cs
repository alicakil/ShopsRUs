using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repo
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly Context _context;

        public CustomerRepo(Context context)
        {
            this._context = context;
        }

        public Customer CreateCustomer(Customer Customer)
        {
            _context.Add(Customer);
            _context.SaveChanges();
            return Customer;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(x=>x.id == id);
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }
    }
}
