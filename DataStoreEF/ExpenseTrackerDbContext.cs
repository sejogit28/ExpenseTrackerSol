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
    public class ExpenseTrackerDbContext : IdentityDbContext<ExpenseTrackerUser>
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {

        }

        public DbSet<ExpenseTrackerUser> ExpenseTrackerUser { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Incomes> Incomes { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupUsers> GroupUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<GroupUsers>()
            .HasKey(o => new { o.ExpenseTrackerUserId, o.GroupsGroupsId });
        }
    }
}
