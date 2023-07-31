using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AccountApp.Domain.Models
{
    public class Address
    {
        //public int Id { get; set; }

        [Required]
        [Key]
        public string City { get; set; }

        [Required]
        [Key]
        public string Street { get; set; }

        [Required]
        [Key]
        public string Building { get; set; }

        [JsonIgnore]
        public int AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
    }
}
