using AccountApp.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Domain.Interfaces
{
    public interface IAccountValidator
    {
        public bool Validate(AccountDTO accountDTO);
    }
}
