using ExpenseTrackerModels;
using ExpenseTrackerRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpTrackerAppApplicationLogic
{
    public class IncomesScreenUseCases : IIncomesScreenUseCases
    {
        private readonly IIncomesRepository incomeRepository;

        public IncomesScreenUseCases(IIncomesRepository incomesRepository)
        {
            this.incomeRepository = incomesRepository;
        }

        public async Task<IList<Incomes>> ViewAllIncomes()
        {

            var incomesEnumerable =  await incomeRepository.getIncomes();
            IList<Incomes> incomeList = (IList<Incomes>)incomesEnumerable;
            return incomeList;
        }

        public async Task<Incomes> GetSingleIncome(int incomeId)
        {
            return await incomeRepository.getSingleIncome(incomeId);
        }

        public async Task<IEnumerable<Incomes>> ViewIncomesByMonth(string targetMonth)
        {
            return await incomeRepository.getIncomesByMonth(targetMonth);
        }

        public async Task CreateNewIncome(Incomes newIncome)
        {
            await incomeRepository.createIncome(newIncome);
        }

        public async Task EditIncome(Incomes editedIncome )
        {
            await incomeRepository.updateIncome(editedIncome);
        }

        public async Task DeleteIncome(int deletedIncomeId)
        {
            await incomeRepository.deleteIncome(deletedIncomeId);
        }
    }
}
