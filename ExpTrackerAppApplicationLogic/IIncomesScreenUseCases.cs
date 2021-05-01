using ExpenseTrackerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public interface IIncomesScreenUseCases
    {
        Task CreateNewIncome(Incomes newIncome);
        Task DeleteIncome(int deletedIncomeId);
        Task EditIncome(Incomes editedIncome);
        Task<Incomes> GetSingleIncome(int incomeId);
        Task<IList<Incomes>> ViewAllIncomes();
        Task<IEnumerable<Incomes>> ViewIncomesByMonth(string targetMonth);
    }
}