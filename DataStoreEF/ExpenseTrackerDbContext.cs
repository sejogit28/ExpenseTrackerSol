using ExpenseTrackerModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStoreEF
{
    public class ExpenseTrackerDbContext : IdentityDbContext<IdentityUser>
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Incomes> Incomes { get; set; }
        public DbSet<Groups> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
