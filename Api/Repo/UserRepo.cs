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

        public async Task<Customer> CreateCustomerAsync(Customer Customer)
        {
            _context.Add(Customer);
            await _context.SaveChangesAsync();
            return Customer;
        }

        public async Task<Customer> GetByIdAsnc(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x=>x.id == id);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
