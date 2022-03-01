using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ExpenseTrackerModels
{
    public class ExpenseTrackerUser : IdentityUser
    {
        public DateTimeOffset DateAdded { get; set; }
        public IList<GroupUsers> GroupUsers { get; set; }
    }
}
