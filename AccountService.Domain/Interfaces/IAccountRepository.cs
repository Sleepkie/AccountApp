using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountApp.Domain.Models;

namespace AccountApp.Domain.Interfaces
{
    public interface IAccountRepository
    {
        public  Task<IEnumerable<Account>> GetAllAsync();

        public Task<IEnumerable<Account>> GetFilteredAsync(IAccountFilter filter);

        public Task<Account> GetAsync(int id);

        public Task<bool> AddAsync(Account account);

        public Task<bool> DeleteAsync(int id);

        public Task<bool> EditAsync(Account account);

    }
}
