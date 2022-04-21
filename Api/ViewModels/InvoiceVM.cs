using Api.Models;

namespace Api.ViewModels
{
    public class InvoiceVM
    {
        public int Id { get; set; }
        public double Ammount { get; set; }
        public double Discounted { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string CustomMessage { get; set; }

        public static InvoiceVM GetViewModel(Invoice invoice, string CustomMessage)
        {
            InvoiceVM invoicevm = new InvoiceVM(); // Return back only required fields, not all. So I use ViewModel.
            invoicevm.Id = invoice.Id;
            invoicevm.Ammount = invoice.Ammount;
            invoicevm.Discounted = invoice.Discounted;
            invoicevm.CustomMessage = CustomMessage;

            return invoicevm;
        }

    }


}
