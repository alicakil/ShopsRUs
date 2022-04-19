namespace Api.Models
{
    public class Invoice : BaseTable
    {
        public int Id { get; set; }
        public double Ammount { get; set; }     
        public User User { get; set; }
        public int Status { get; set; }
    }
}
