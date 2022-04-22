using Api.Models;


namespace Api.BusinessLogic
{
    public static class DiscountLogic
    {
        interface IDiscount
        {
            double GetDiscount(double billAmmount);
        }


        class AnEmployeeDiscount : IDiscount
        {
            public double GetDiscount(double billAmmount)
            {
                return billAmmount * 0.7;  // 30% discount
            }
        }


        class AnAffiateDiscount : IDiscount
        {
            public double GetDiscount(double billAmmount)
            {
                return billAmmount * 0.9;  // 10% discount
            }
        }

        class OldCustomerDiscount : IDiscount
        {
            public double GetDiscount(double billAmmount)
            {
                return billAmmount * 0.95;  // 5% discount
            }
        }


        public static double GetDiscount(Customer customer, double billAmmount)
        {
            double discounted = 0;

            IDiscount Idiscount;

            if (customer.TypeId == CustomerType.AnEmployee)
            {
                Idiscount = new AnEmployeeDiscount();
                discounted = Idiscount.GetDiscount(billAmmount);
            }
            else if (customer.TypeId == CustomerType.AnAffiate)
            {
                Idiscount = new AnAffiateDiscount();
                discounted = Idiscount.GetDiscount(billAmmount);
            }
            else
            {
                if (customer.CreateTime < DateTime.Now.AddYears(-2)) // this is an old customer : 5% discount
                {
                    Idiscount = new OldCustomerDiscount();
                    discounted = Idiscount.GetDiscount(billAmmount);
                }
                else
                {
                    discounted = billAmmount; 
                    double AddditionalDiscount = Math.Round(billAmmount / 100) * 5; // 5$ discount for each 100$ on the bill.
                    discounted -= AddditionalDiscount;
                }
            }


            return discounted;
        }
    }
}
