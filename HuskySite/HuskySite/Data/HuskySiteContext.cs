using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HuskySite.Models.BookkeepingModels;

namespace HuskySite.Models
{
    public class HuskySiteContext : DbContext
    {
        public HuskySiteContext (DbContextOptions<HuskySiteContext> options)
            : base(options)
        {
        }        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Accounting> Accountings { get; set; }
        public DbSet<PatternSMS> PatternsSMS { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<OperationType> OperationType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>().ToTable("Account");
            builder.Entity<Accounting>().ToTable("Accounting");
            builder.Entity<PatternSMS>().ToTable("PatternSMS");
            builder.Entity<Wallet>().ToTable("Wallet");
            builder.Entity<OperationType>().ToTable("OperationType");
        }

        
    }
}
