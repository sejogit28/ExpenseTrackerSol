using ExpenseTrackerModels;
using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Net.Http;
using System.Threading.Tasks;


HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);

Console.WriteLine("///////////////");
//Console.WriteLine(DateTime.Now.ToString("yyyy-MM"));
//Console.WriteLine(DateTime.Now.AddDays(-6));
Console.WriteLine("Reading All Incomes");
await getIncomesTest();
Console.ReadLine();


Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Income");
await getSingleIncomeTest(6);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Incomes By A Specified Month");
await getIncomesByMonthTest("2021-05");
Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("Creating A New Income");
//await createNewIncomeTest();
//Console.ReadLine();
//await getIncomesTest();
//Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("Deleting A New Income");
//await deleteIncomeTest(12);
//Console.ReadLine();
//await getIncomesTest();
//Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Updating A New Income");
var updatableProject = await getSingleIncomeTest(6);
await updateIncomeTest(updatableProject);
Console.ReadLine();
await getIncomesTest();
Console.ReadLine();

async Task getIncomesTest() 
{
    IncomesRepository incomesRepository = new(apiExecuter);
    var incomes = await incomesRepository.getIncomes();
    foreach(var income in incomes) 
    {
        Console.WriteLine($"Income Notes: {income.Notes}");
    }

}

async Task<Incomes> getSingleIncomeTest(int singleId)
{
    IncomesRepository incomesRepository = new(apiExecuter);
    return await incomesRepository.getSingleIncome(singleId);
    /*var singleIncome =*/
    /*Console.WriteLine(singleIncome.Notes);
    Console.WriteLine(singleIncome.Amount);
    Console.WriteLine(singleIncome.MonthYear);*/
}


async Task getIncomesByMonthTest(string TargetYearMonth)
{
    IncomesRepository incomesRepository = new(apiExecuter);
    var incomes = await incomesRepository.getIncomesByMonth(TargetYearMonth);
    foreach (var income in incomes)
    {
        Console.WriteLine($"Income Notes: {income.Notes}");
    }

}

async Task createNewIncomeTest()
{
    IncomesRepository incomesRepository = new(apiExecuter);
    var newlyMadeIncome = new Incomes
    {
        UserId = "65052034-ea64-4ee1-92cd-75d07f61297a",
        Notes = "This is an 10th console test income!!",
        Amount = (float)29.36,
        DatePaid = DateTime.Now.AddDays(-6),
        MonthYear = null,
        DateSubmitted = DateTime.Now,
    };

    newlyMadeIncome.MonthYear = newlyMadeIncome.DatePaid.ToString("yyyy-MM");

     await incomesRepository.createIncome(newlyMadeIncome);
}

async Task deleteIncomeTest(int deletedIncomeId)
{
    IncomesRepository incomesRepository = new(apiExecuter);
    await incomesRepository.deleteIncome(deletedIncomeId);
}

async Task updateIncomeTest(Incomes updatedIncome)
{
    IncomesRepository incomesRepository = new(apiExecuter);

    updatedIncome.IncomeId = updatedIncome.IncomeId;
    updatedIncome.UserId = "65052034-ea64-4ee1-92cd-75d07f61297a";
    updatedIncome.Notes = "This is a 9th updated console test income!!";
    updatedIncome.Amount = (float)29.36;
    updatedIncome.DatePaid = DateTime.Now.AddDays(-6);
    updatedIncome.MonthYear = updatedIncome.MonthYear; 
    updatedIncome.DateSubmitted = DateTime.Now;
    
    await incomesRepository.updateIncome(updatedIncome);
}
