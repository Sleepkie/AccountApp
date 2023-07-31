using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace AccountApp.Domain.Models.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string OpeningDate { get; set; }

        [Required]
        public string ExpirationDate { get; set; }

        [Required]
        public AddressDTO Address { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Area { get; set; }

        public List<ResidentDTO>? Residents { get; set; } = new List<ResidentDTO>();
    }
}
