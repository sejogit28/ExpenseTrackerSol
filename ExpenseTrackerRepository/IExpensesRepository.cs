using ExpenseTrackerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IExpensesRepository
    {
        Task createExpense(Expenses newExpense);

        Task deleteExpense(int deletedExpenseId);

        Task<IEnumerable<Expenses>> getExpenses();

        Task<IEnumerable<Expenses>> getExpensesByMonth(string MonthYear);

        Task<Expenses> getSingleExpense(int singleExpenseId);

        Task updateExpense(Expenses updatedExpense);
    }
}