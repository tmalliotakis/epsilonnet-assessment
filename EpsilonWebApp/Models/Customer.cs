using System.ComponentModel.DataAnnotations;

namespace EpsilonWebApp.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? CompanyName { get; set; }

        [StringLength(200)]
        public string? ContactName { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }
    }
}
