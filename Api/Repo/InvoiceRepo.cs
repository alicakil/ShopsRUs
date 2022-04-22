using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repo
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly Context _context;

        public InvoiceRepo(Context context)
        {
            this._context = context;
        }

        public Invoice Create(Invoice Invoice)
        {
            _context.Add(Invoice);
            _context.SaveChanges();
            return Invoice;
        }

        public Invoice Update(Invoice Invoice)
        {
            _context.Update(Invoice);
            _context.SaveChanges();
            return Invoice;
        }

        public Invoice GetById(int id)
        {
            return _context.Invoices.AsNoTracking().FirstOrDefault(x=>x.Id == id);
        }

        public List<Invoice> GetAll()
        {
            return _context.Invoices.AsNoTracking().ToList();
        }
    }
}
