using ExpenseTrackerModels;
using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Net.Http;
using System.Threading.Tasks;


HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Expense");
await getExpensesTest();
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Expense");
await getSingleExpenseTest(2);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Expenses By A Specified Month");
await getExpensesByMonthTest("2021-04");
Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("Creating A New Expense");
//await createNewExpenseTest();
//Console.ReadLine();
//await getExpensesTest();
//Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("Updating A New Income");
//var updatableProject = await getSingleExpenseTest(5);
//await updateIncomeTest(updatableProject);
//Console.ReadLine();
//await getExpensesTest();
//Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Deleting An Expense");
await deleteExpenseTest(2);
Console.ReadLine();
await getExpensesTest();
Console.ReadLine();

async Task getExpensesTest()
{
    ExpensesRepository expensesRepository = new(apiExecuter);
    var expenses = await expensesRepository.getExpenses();
    foreach (var expense in expenses)
    {
        Console.WriteLine($"Expense Notes: {expense.Notes}");
    }

}

async Task<Expenses> getSingleExpenseTest(int singleId)
{
    ExpensesRepository expensesRepository = new(apiExecuter);
    var singleExpense = await expensesRepository.getSingleExpense(singleId);
    Console.WriteLine(singleExpense.Notes);
    Console.WriteLine(singleExpense.Amount);
    Console.WriteLine(singleExpense.MonthYear);
    return singleExpense;
}

async Task getExpensesByMonthTest(string TargetYearMonth)
{
    ExpensesRepository expensesRepository = new(apiExecuter);
    var expenses = await expensesRepository.getExpensesByMonth(TargetYearMonth);
    foreach (var expense in expenses)
    {
        Console.WriteLine($"Expense Notes: {expense.Notes}");
    }

}

async Task createNewExpenseTest()
{
    ExpensesRepository expensesRepository = new(apiExecuter);
    var newlyMadeExpenses = new Expenses
    {
        UserId = "you webApiBro",
        Notes = "This is a 6th console test income!!",
        Amount = (float)759.28,
        DateSpent = DateTime.Now.AddDays(-15),
        MonthYear = null,
        DateSubmitted = DateTime.Now,
    };

    newlyMadeExpenses.MonthYear = newlyMadeExpenses.DateSpent.ToString("yyyy-MM");

    await expensesRepository.createExpense(newlyMadeExpenses);
}


async Task updateIncomeTest(Expenses updatedExpense)
{
    ExpensesRepository expensesRepository = new(apiExecuter);

    updatedExpense.ExpenseId = updatedExpense.ExpenseId;
    updatedExpense.UserId = "You updated webApiBroBro";
    updatedExpense.Notes = "This is an updated 5th console test expense(!)";
    updatedExpense.Amount = (float)29.36;
    updatedExpense.DateSpent = updatedExpense.DateSpent;
    updatedExpense.MonthYear = updatedExpense.MonthYear;
    updatedExpense.DateSubmitted = updatedExpense.DateSubmitted;

    await expensesRepository.updateExpense(updatedExpense);
}

async Task deleteExpenseTest(int deletedExpenseId)
{
    ExpensesRepository expensesRepository = new(apiExecuter);
    await expensesRepository.deleteExpense(deletedExpenseId);
}


