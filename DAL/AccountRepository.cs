using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using AccountApp.Domain.Interfaces;
using AccountApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.DAL
{
    public class AccountRepository : IAccountRepository
    {

        private readonly AccountDbContext _context;

        public AccountRepository(AccountDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(a => a.Address).Include(a => a.Residents).ToListAsync();
        }

        public async Task<Account> GetAsync(int id)
        {
            return await _context.Accounts.Include(a => a.Address).Include(a => a.Residents).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetFilteredAsync(IAccountFilter filter)
        {
            var query = _context.Accounts.Include(a => a.Address).Include(a => a.Residents).AsQueryable();

            if (filter.HasResidents.HasValue)
            {
                if (filter.HasResidents == true)
                {
                    query = query.Where(a => a.Residents.Any());
                }
                else
                {
                    query = query.Where(a => a.Residents.Count == 0);
                }
            }

            //Фильтр по адресу

            bool hasAddress = filter.Address != null;

            if (hasAddress)
            {
                if (!String.IsNullOrEmpty(filter.Address.City))
                {
                    query = query.Where(a => a.Address.City == filter.Address.City);
                }

                if (!String.IsNullOrEmpty(filter.Address.Street))
                {
                    query = query.Where(a => a.Address.Street == filter.Address.Street);
                }

                if (!String.IsNullOrEmpty(filter.Address.Building))
                {
                    query = query.Where(a => a.Address.Building == filter.Address.Building);
                }
            }

            //Фильтр по ФИО

            if (!String.IsNullOrEmpty(filter.ResidentName))
            {
                query = query.Where(a => a.Residents.Any(r => r.FullName == filter.ResidentName));
            }

            //Фильтры по дате

            if (!String.IsNullOrEmpty(filter.FromDate))
            {
                DateTime.TryParseExact(filter.FromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);

                query = query.Where(a => a.OpeningDate >= fromDate);
            }

            if (!String.IsNullOrEmpty(filter.ToDate))
            {
                DateTime.TryParseExact(filter.ToDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);

                query = query.Where(a => a.ExpirationDate <= toDate);
            }

            if (!String.IsNullOrEmpty(filter.OpeningDate))
            {
                DateTime.TryParseExact(filter.OpeningDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openingDate);

                query = query.Where(a => a.OpeningDate == openingDate);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AddAsync(Account account)
        {
            bool exists = await _context.Accounts.AnyAsync(a => a.AccountNumber == account.AccountNumber);

            if (exists)
            {
                return  false;
            }

            try
            {
                await _context.AddAsync(account);

                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EditAsync(Account account)
        {
            var accountToEdit = await _context.Accounts.Include(a => a.Address).Include(a => a.Residents).FirstAsync(a => a.AccountNumber == account.AccountNumber);

            if (accountToEdit == null)
            {
                return false;
            }

            try
            {
                accountToEdit.Residents.Clear();
                
                foreach (var resident in  account.Residents)
                {
                    accountToEdit.Residents.Add(resident);
                }

                _context.Addresses.Remove(accountToEdit.Address);
                accountToEdit.Address = account.Address;

                accountToEdit.Area = account.Area;

                accountToEdit.OpeningDate = account.OpeningDate;
                accountToEdit.ExpirationDate = account.ExpirationDate;

                await _context.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account is null)
            {
                return false;
            }

            try
            {
                _context.Remove(account);

                await _context.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }

            return true;
        }
    }
}
