using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerModels
{
    public class Expenses
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }

        [Required]
        public float Amount { get; set; }

        public DateTimeOffset DateSpent { get; set; }

        [Required]
        [MaxLength(25)]
        public string MonthYear { get; set; }

        [Required]
        public DateTimeOffset DateSubmitted { get; set; }

        public ExpenseCategoriesList ExpenseTypes { get; set; }
        public enum ExpenseCategoriesList 
        {
            Housing,
            Insurance,
            Electric,
            Phone,
            Internet,
            Gas,
            Parking,
            Pet,
            Subscription,
            Groceries,
            Fun
        }

        public int? GroupsGroupsId { get; set; }

        public Groups Groups { get; set; }

    }
}
