using BankingSystemAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BankingSystemAPI.Persistence.Models
{
    public class BankingApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        //public DbSet<Account> Accounts { get; set; }

        public string DbPath { get; }

        public BankingApiContext(/*DbContextOptions<BankingApiContext> options)  : base(options*/)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "customer.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}
