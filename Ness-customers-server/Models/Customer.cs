using System.ComponentModel.DataAnnotations;

namespace Ness_customers_server.Models
{
    public class Customer
    {
        [Key]
        public int customerNumber { get; set; }
        public string customerName { get; set; }

        public string customerType { get; set; }
        public string? address { get; set; }
        public string contentPerson { get; set; }
        public string phoneNumber { get; set; }

    }
}
