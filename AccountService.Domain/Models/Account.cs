using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public DateTime OpeningDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Area { get; set; }

        public List<Resident> Residents { get; set; } = new List<Resident>();


    }
}
