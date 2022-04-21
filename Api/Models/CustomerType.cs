using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CustomerType
    {
        public const int AnEmployee = 1;
        public const int AnAffiate = 2;

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }



}
