using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerModels
{
    public class GroupUsers
    {
        public int GroupsGroupsId { get; set; }
        public Groups Groups { get; set; }
        public string ExpenseTrackerUserId { get; set; }
        public ExpenseTrackerUser ExpenseTrackerUser { get; set; }
    }
}
