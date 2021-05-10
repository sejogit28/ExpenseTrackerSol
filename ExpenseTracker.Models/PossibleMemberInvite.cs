using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerModels
{
    public class PossibleMemberInvite
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9_]+\.[a-zA-Z0-9-.]+$")]
        public string InviteeEmail { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9_]+\.[a-zA-Z0-9-.]+$")]
        public string InviterEmail { get; set; }

        [Required]
        public int GroupId { get; set; }
    }
}

