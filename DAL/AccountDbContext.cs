using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountApp.Domain.Models;

namespace AccountApp.DAL
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Resident> Residents { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(i => i.AccountNumber).IsUnique();
            });

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Residents)
                .WithOne(r => r.Account);

            modelBuilder.Entity<Address>()
                .HasKey(a => new { a.City, a.Street, a.Building });
        }
    }
}
