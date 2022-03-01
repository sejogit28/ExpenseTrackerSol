using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTrackerModels
{
    public class Groups
    {
        [Key]
        public int GroupsId { get; set; }

        [Required]
        [MaxLength(80)]
        public string GroupName { get; set; }

        [Required]
        public DateTimeOffset DateCreated { get; set; }

        public string ExpenseTrackerUserId { get; set; }
        public ExpenseTrackerUser ExpenseTrackerUser { get; set; }

        public IList<Incomes> Incomes { get; set; }
        public IList<Expenses> Expenses { get; set; }
        public IList<GroupUsers> GroupUsers {get; set;}

    }
}
