using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class InvoiceStatus
    {
        public const int draft = 1;
        public const int issued = 2;
        public const int cancelled = 3;

        [Key]
        public int id { get; set; }
        public string Name { get; set; } // draft, issued, cancelled
        IList<Invoice> Invoices { get; set; }
    }
}
