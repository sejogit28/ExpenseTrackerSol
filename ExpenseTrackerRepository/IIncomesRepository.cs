using ExpenseTrackerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IIncomesRepository
    {
        Task createIncome(Incomes newIncome);
        Task deleteIncome(int deletedIncomeId);
        Task<IEnumerable<Incomes>> getIncomes();
        Task<IEnumerable<Incomes>> getIncomesByMonth(string MonthYear);
        Task<Incomes> getSingleIncome(int singleIncomeId);
        Task updateIncome(Incomes updatedIncome);
    }
}