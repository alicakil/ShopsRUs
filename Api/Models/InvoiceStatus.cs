namespace Api.Models
{
    public class InvoiceStatus
    {
        public int id { get; set; }
        public string Name { get; set; } // draft, issued, cancelled
        IList<Invoice> Invoices { get; set; }
    }
}
