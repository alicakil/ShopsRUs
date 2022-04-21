using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Api.Models
{
    public class Invoice : BaseTable
    {
        public int Id { get; set; }

        [Range(0, 1000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Ammount { get; set; }
        public double Discounted { get; set; }
        public int Customerid { get; set; }
        public Customer Customer { get; set; }
        public int Statusid { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
