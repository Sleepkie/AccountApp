using AccountApp.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Domain.Interfaces
{
    public interface IAccountFilter
    {
        public bool? HasResidents { get; set; }

        public string? FromDate { get; set; } 

        public string? ToDate { get; set; }

        public string? OpeningDate { get; set; }

        public string? ResidentName { get; set; }

        public AddressDTO? Address { get; set; } 
    }
}
