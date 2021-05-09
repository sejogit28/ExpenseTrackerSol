using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenseTrackerModels
{
    public class Incomes
    {
        [Key]
        public int IncomeId { get; set; }
        [Required]
        [MaxLength(75)]
        public string UserId { get; set; }
        //public string GroupId { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        //[MaxLength(25)]
        public DateTime DatePaid { get; set; }
        [Required]
        [MaxLength(25)]
        public string MonthYear { get; set; }

        [Required]
        public DateTime DateSubmitted { get; set; }
        public IncomeCategoriesList IncomeTypes { get; set; }
        public enum IncomeCategoriesList
        {

            
            Taxes,           
            Job,
            Borrowed,          
            Repaid,
            Other
        }

        public int? GroupsGroupsId { get; set; }
        public Groups Groups { get; set; }
    }
}
