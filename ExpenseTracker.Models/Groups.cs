using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTrackerModels
{
    public class Groups
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }

        public IList<GroupUsers> GroupUsers {get; set;}
    }
}
