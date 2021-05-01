using ExpenseTrackerModels.AuthModels;
using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);


Console.WriteLine("///////////////");
Console.WriteLine("///////////////");
Console.WriteLine("Reading All Users");
await getAllUsersTest();
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single User");
await getSingleUserTest("65052034-ea64-4ee1-92cd-75d07f61297a");
Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("///////////////");
//Console.WriteLine("Deleting A Single User");
//Console.WriteLine("///////////////");
//await deleteSingleUserTest("b5474337-5bad-4717-ae9f-ece6db1a241a");
//await getAllUsersTest();
//Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("///////////////");
Console.WriteLine("Updateing A Single Users Password");
await updateSingleUserPasswordTest();
Console.ReadLine();

async Task getAllUsersTest() 
{
    UserProfileRepository userProfileRepository = new(apiExecuter);
    var users = await userProfileRepository.GetAllUsers();
    

    foreach(var user in users.ToList()) 
    {
        Console.WriteLine($"User Name: {user.Email}");
    }

}

async Task getSingleUserTest(string singleUserId)
{
    UserProfileRepository userProfileRepository = new(apiExecuter);
    var user = await userProfileRepository.GetSingleUser(singleUserId);
    Console.WriteLine(user.Email);
    Console.WriteLine(user.Id);
}

async Task deleteSingleUserTest(string deletedSingleUserId)
{
    UserProfileRepository userProfileRepository = new(apiExecuter);
    await userProfileRepository.DeleteSingleUser(deletedSingleUserId);
  
}

async Task updateSingleUserPasswordTest()
{
    UserProfileRepository userProfileRepository = new(apiExecuter);
    var newPass = new ChangePassword 
    {
        CurrentPassword = "D8f8nd@Exp29",
        NewPassword = "D8f8nd@Exp2",
        ConfirmNewPassword = "D8f8nd@Exp2"
    };

    await userProfileRepository.UpdateUserPassword(newPass, "1117910b-fe69-47fb-85f0-d469e38732c5");

}
