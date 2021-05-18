using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerModels.GroupModels
{
    public class AddNewMemberToGroup
    {
        public int GroupId { get; set; }
        public string NewMemberUserName { get; set; }
        public bool OperationSuccessful { get; set; }
        public string OperationMessage { get; set; }
    }
}
