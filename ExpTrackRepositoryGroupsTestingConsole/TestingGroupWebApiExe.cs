using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Group Names");
await getGroupsTest();
Console.ReadLine();

async Task getGroupsTest()
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var groupsList = await groupsRepository.getGroups();
    foreach (var group in groupsList)
    {
        Console.WriteLine($"Group Names: {group.GroupName}");
    }

}