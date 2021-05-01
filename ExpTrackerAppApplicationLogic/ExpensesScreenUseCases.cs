using ExpenseTrackerModels;
using ExpenseTrackerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public class ExpensesScreenUseCases : IExpensesScreenUseCases
    {
        private readonly IExpensesRepository expenseRepository;

        public ExpensesScreenUseCases(IExpensesRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public async Task<IList<Expenses>> ViewAllExpenses()
        {
            var expenseEnumerable = await expenseRepository.getExpenses();
            var expenseList = (IList<Expenses>)expenseEnumerable;
            return expenseList;
        }

        public async Task<Expenses> GetSingleExpense(int ExpenseId)
        {
            return await expenseRepository.getSingleExpense(ExpenseId);
        }

        public async Task<IEnumerable<Expenses>> ViewExpensesByMonth(string targetMonth)
        {
            return await expenseRepository.getExpensesByMonth(targetMonth);
        }

        public async Task CreateNewExpense(Expenses newExpense)
        {
            await expenseRepository.createExpense(newExpense);
        }

        public async Task EditExpense(Expenses editedExpense)
        {
            await expenseRepository.updateExpense(editedExpense);
        }

        public async Task DeleteExpense(int deletedExpenseId)
        {
            await expenseRepository.deleteExpense(deletedExpenseId);
        }
    }
}
