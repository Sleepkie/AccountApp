using AccountApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using AccountApp.Domain.Models.DTO;

namespace AccountApp.Services
{
    public class AccountValidator : IAccountValidator
    {
        public bool Validate(AccountDTO accountDTO)
        {
            if (accountDTO.Area <= 0)
            {
                return false;
            }
                

            DateTime openingDate;
            DateTime expirationDate;

            string openingDateOnly = accountDTO.OpeningDate.Substring(0, 10);
            string expirationDateOnly = accountDTO.ExpirationDate.Substring(0, 10);
            var dateIsValid = DateTime.TryParseExact(openingDateOnly, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out openingDate) & 
                              DateTime.TryParseExact(expirationDateOnly, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out expirationDate);

            if (!dateIsValid)
            {
                return false;
            }

            


            if (DateTime.Compare(openingDate, expirationDate) > 0)
            {
                return false;
            }

            return true;
        }
    }
}
