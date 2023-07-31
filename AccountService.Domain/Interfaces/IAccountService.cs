using AccountApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Domain.Interfaces
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAllAccountsAsync();

        public Task<IEnumerable<Account>> GetFilteredAsync(IAccountFilter filter);

        public Task<Account> GetAccountAsync(int id);

        public Task<bool> AddAccountAsync(Account account);

        public Task<bool> EditAccountAsync(Account account);

        public Task<bool> DeleteAccountAsync(int id);
    }
}
