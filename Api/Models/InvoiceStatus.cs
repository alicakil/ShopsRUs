namespace Api.Models
{
    public class InvoiceStatus
    {
        public int id { get; set; }
        public int Name { get; set; } // draft, issued, cancelled
        public int isMainStore { get; set; }
        IList<User> Users { get; set; }
    }
}
