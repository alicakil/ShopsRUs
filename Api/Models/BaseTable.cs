namespace Api.Models
{
    public class BaseTable
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime ModifyTime { get; set; }
    }
}
