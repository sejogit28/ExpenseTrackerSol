using ExpenseTrackerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public interface IExpensesScreenUseCases
    {
        Task CreateNewExpense(Expenses newExpense);

        Task DeleteExpense(int deletedExpenseId);

        Task EditExpense(Expenses editedExpense);

        Task<Expenses> GetSingleExpense(int ExpenseId);

        Task<IList<Expenses>> ViewAllExpenses();

        Task<IEnumerable<Expenses>> ViewExpensesByMonth(string targetMonth);
    }
}