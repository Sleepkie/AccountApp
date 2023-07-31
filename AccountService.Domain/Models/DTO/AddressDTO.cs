using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Domain.Models.DTO
{
    public class AddressDTO
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }
    }
}
