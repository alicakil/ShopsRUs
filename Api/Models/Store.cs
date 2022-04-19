namespace Api.Models
{
    public class Store
    {
        public int id { get; set; }
        public int Name { get; set; }
        public int isMainStore { get; set; }
        IList<User> Users { get; set; }
    }
}
