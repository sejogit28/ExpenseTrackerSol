using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ExpenseTrackerModels
{
    public class ExpenseTrackerUser : IdentityUser
    {
        public DateTime DateAdded { get; set; }
        public IList<GroupUsers> GroupUsers { get; set; }
    }
}
