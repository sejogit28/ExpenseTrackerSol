using DataStoreEF;
using ExpenseTrackerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,User")]
    public class IncomesController : ControllerBase
    {
        private readonly ExpenseTrackerDbContext _datExpBase;

        public IncomesController(ExpenseTrackerDbContext etDB)
        {
            _datExpBase = etDB;
        }

        [HttpGet("incomeslist")]
        public async Task<IActionResult> GetIncomes()
        {
            var incomeList = await _datExpBase.Incomes.ToListAsync();

            return Ok(incomeList);
        }

        [HttpGet("incomeslistbymonth/{targetMonth}")]
        public async Task<IActionResult> GetIncomesByMonth(string targetMonth)
        {

            var incomesByMonthList = await _datExpBase.Incomes.Where(t => t.MonthYear == targetMonth).ToListAsync();

            if (incomesByMonthList == null || incomesByMonthList.Count == 0)
            {
                incomesByMonthList = new List<Incomes>();
                return Ok(incomesByMonthList);
            }

            return Ok(incomesByMonthList);
        }

        [HttpGet("{singleIncomeId:int}")]
        public async Task<IActionResult> GetSingleIncome(int singleIncomeId)
        {
            var income = await _datExpBase.Incomes.FindAsync(singleIncomeId);

            if (income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }

        [HttpPost("createincome")]
        public async Task<IActionResult> CreateIncome([FromBody] Incomes newIncome)
        {
            newIncome.MonthYear = newIncome.DatePaid.ToString("yyyy-MM");
            await _datExpBase.Incomes.AddAsync(newIncome);
            await _datExpBase.SaveChangesAsync();

            return Ok(newIncome);
        }

        [HttpPut("updateincome/{updatedIncomeId:int}")]
        public async Task<IActionResult> UpdateIncome(int updatedIncomeId, [FromBody] Incomes updatedIncome)
        {

            if (updatedIncomeId != updatedIncome.IncomeId)
            {

                return BadRequest();
            }
            _datExpBase.Entry(updatedIncome).State = EntityState.Modified;

            try
            {
                await _datExpBase.SaveChangesAsync();

                return Ok(updatedIncome);
            }
            catch
            {

                if (await _datExpBase.Expenses.FindAsync(updatedIncomeId) == null)
                {

                    return NotFound();
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("deleteincome/{deletedIncomeId:int}")]
        public async Task<IActionResult> DeleteIncome(int deletedIncomeId)
        {
            var deletedIncome = await _datExpBase.Incomes.FindAsync(deletedIncomeId);

            if (deletedIncome == null)
            {

                return NotFound();
            }

            _datExpBase.Incomes.Remove(deletedIncome);
            await _datExpBase.SaveChangesAsync();

            return Ok(deletedIncome);
        }
    }
}
