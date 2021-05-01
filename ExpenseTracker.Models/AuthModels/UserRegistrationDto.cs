using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerModels
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(75)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [MaxLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
