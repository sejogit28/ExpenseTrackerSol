using DataStoreEF;
using ExpenseTrackerModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseTrackerDbContext _datExpBase;

        public ExpensesController(ExpenseTrackerDbContext etDB)
        {
            _datExpBase = etDB;
        }


        [HttpGet("expenseslist")]
        public async Task<IActionResult> GetExpenses()
        {
            var expenseList = await _datExpBase.Expenses.ToListAsync();
            return Ok(expenseList);
        }

        [HttpGet("expenseslistbymonth/{targetMonth}")]
        public async Task<IActionResult> GetExpensesByMonth(string targetMonth)
        {

            var expensesByMonthList = await _datExpBase.Expenses.Where(t => t.MonthYear == targetMonth).ToListAsync();
            if (expensesByMonthList == null || expensesByMonthList.Count == 0)
            {
                expensesByMonthList = new List<Expenses>();
                return Ok(expensesByMonthList);
            }
            return Ok(expensesByMonthList);
        }

        [HttpGet("{singleExpenseId:int}")]
        public async Task<IActionResult> GetSingleExpense(int singleExpenseId)
        {
            var expense = await _datExpBase.Expenses.FindAsync(singleExpenseId);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }


        [HttpPost("createexpense")]
        public async Task<IActionResult> createExpense([FromBody] Expenses newExpense)
        {
            newExpense.MonthYear = newExpense.DateSpent.ToString("yyyy-MM");
            await _datExpBase.Expenses.AddAsync(newExpense);
            await _datExpBase.SaveChangesAsync();
            return Ok(newExpense);
        }


        [HttpPut("updateexpense/{updatedExpenseId:int}")]
        public async Task<IActionResult> updateExpense(int updatedExpenseId, [FromBody] Expenses updatedExpense)
        {
            if (updatedExpenseId != updatedExpense.ExpenseId) return BadRequest();
            _datExpBase.Entry(updatedExpense).State = EntityState.Modified;
            try
            {
                await _datExpBase.SaveChangesAsync();
                return Ok(updatedExpense);
            }
            catch
            {
                if (await _datExpBase.Expenses.FindAsync(updatedExpenseId) == null)
                {
                    return NotFound();
                    throw;
                }
            }
            return NoContent();
        }


        [HttpDelete("deleteexpense/{deletedExpenseId:int}")]
        public async Task<IActionResult> deleteExpense(int deletedExpenseId)
        {
            var deletedExpense = await _datExpBase.Expenses.FindAsync(deletedExpenseId);
            if (deletedExpense == null)
            {
                return NotFound();
            }

            _datExpBase.Expenses.Remove(deletedExpense);
            await _datExpBase.SaveChangesAsync();
            return NoContent();

        }

    }
}
