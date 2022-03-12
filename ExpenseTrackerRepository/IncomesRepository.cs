using ExpenseTrackerModels;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public class IncomesRepository : IIncomesRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public IncomesRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Incomes>> getIncomes()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Incomes>>("api/Incomes/incomeslist");
        }

        public async Task<IEnumerable<Incomes>> getIncomesByMonth(string MonthYear)
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Incomes>>($"api/Incomes/incomeslistbymonth/{MonthYear}");
        }

        public async Task<Incomes> getSingleIncome(int singleIncomeId)
        {
            return await webApiExecuter.InvokeGet<Incomes>($"api/Incomes/{singleIncomeId}");
        }

        public async Task createIncome(Incomes newIncome)
        {
            await webApiExecuter.InvokePost("api/Incomes/createincome", newIncome);            
        }

        public async Task deleteIncome(int deletedIncomeId)
        {
            await webApiExecuter.InvokeDelete<Incomes>($"api/Incomes/deleteincome/{deletedIncomeId}");
        }

        public async Task updateIncome(Incomes updatedIncome)
        {
            await webApiExecuter.InvokePut<Incomes>($"api/Incomes/updateincome/{updatedIncome.IncomeId}", updatedIncome);
        }
    }
}
