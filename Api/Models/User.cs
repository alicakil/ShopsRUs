using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class User
    {
        public int id { get; set; }

        [StringLength(100)]
        [Required]
        [DisplayName("User Name & Surname")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", 
            ErrorMessage = "Please enter a valid password!")]
        public string Password { get; set; }

        public int? Storeid { get; set; }
        public Store? Store { get; set; }

        IList<Invoice> Invoices { get; set; }
    }
}
