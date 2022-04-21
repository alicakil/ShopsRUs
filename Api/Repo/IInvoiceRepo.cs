using Api.Models;

namespace Api.Repo
{
    public interface IInvoiceRepo
    {
        public List<Invoice> GetAll();
        public Invoice GetById(int id);
        public Invoice Create(Invoice Invoice);
        public Invoice Update(Invoice Invoice);
    }
}
