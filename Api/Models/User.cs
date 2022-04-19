namespace Api.Models
{
    public class User
    {
        public int id { get; set; }
        public int Name { get; set; }

        public int Storeid { get; set; }
        public Store Store { get; set; }

        IList<Invoice> Invoices { get; set; }
    }
}
