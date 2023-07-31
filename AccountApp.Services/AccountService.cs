using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Domain.Interfaces;
using AccountApp.Domain.Models;
namespace AccountApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Account>> GetFilteredAsync(IAccountFilter filter)
        {
            return await _repository.GetFilteredAsync(filter);
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<bool> EditAccountAsync(Account account)
        {
            var edited = await _repository.EditAsync(account);

            return edited;
        }

        public async Task<bool> AddAccountAsync(Account account)
        {
            var added = await _repository.AddAsync(account);

            return added;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            return deleted;
        }
    }
}
