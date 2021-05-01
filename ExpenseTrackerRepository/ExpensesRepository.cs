using ExpenseTrackerModels;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ExpensesRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Expenses>> getExpenses()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Expenses>>("api/Expenses/expenseslist");
        }

        public async Task<IEnumerable<Expenses>> getExpensesByMonth(string MonthYear)
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Expenses>>($"api/Expenses/expenseslistbymonth/{MonthYear}");
        }

        public async Task<Expenses> getSingleExpense(int singleExpenseId)
        {
            return await webApiExecuter.InvokeGet<Expenses>($"api/Expenses/{singleExpenseId}");
        }

        public async Task createExpense(Expenses newExpense)
        {
            await webApiExecuter.InvokePost("api/Expenses/createexpense", newExpense);
        }

        public async Task deleteExpense(int deletedExpenseId)
        {
            await webApiExecuter.InvokeDelete<Expenses>($"api/Expenses/deleteexpense/{deletedExpenseId}");
        }

        public async Task updateExpense(Expenses updatedExpense)
        {
            await webApiExecuter.InvokePut<Expenses>($"api/Expenses/updateexpense/{updatedExpense.ExpenseId}", updatedExpense);
        }
    }
}
