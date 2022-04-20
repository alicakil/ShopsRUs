using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Api.Models
{
    public class Store
    {
        public int id { get; set; }
        public string Name { get; set; }
        public bool isMainStore { get; set; }
        IList<User> Users { get; set; }
    }
}
