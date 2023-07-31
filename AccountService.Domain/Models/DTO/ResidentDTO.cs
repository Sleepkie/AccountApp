using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Domain.Models.DTO
{
    public class ResidentDTO
    {
        [Required]
        public string FullName { get; set; }

    }
}
