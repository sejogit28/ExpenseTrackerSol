using ExpenseTrackerModels;
using ExpenseTrackerModels.GroupModels;
using ExpenseTrackerRepository;
using ExpenseTrackerRepository.ApiRouteFetcher;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

HttpClient httpClient = new();
IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Names of All Groups");
await getGroupsTest();
Console.ReadLine();

/*Console.WriteLine("///////////////");
Console.WriteLine("Deleting A Single Group");
await deleteSingleGroupTest(10);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Names of All Groups Once Again");
await getGroupsTest();
Console.ReadLine();*/

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Groups Various Detail");
var singleGroup = await getSingleGroupTest(8);
Console.WriteLine(singleGroup.GroupsId);
Console.WriteLine(singleGroup.GroupName);
Console.WriteLine(singleGroup.DateCreated);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Groups Member Names");
await getListOfGroupMemberNamesTest(8);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Adding a New Member to a Group");
await addMemberToGroupTest();
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Groups Member Names Once Again");
await getListOfGroupMemberNamesTest(8);
Console.ReadLine();

/*Console.WriteLine("///////////////");
Console.WriteLine("Deleting a Member from a group");
await removeMemberFromGroupTest(8, "sejoTestEmail1828@mailinator.com");
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Groups Member Names Once Again");
await getListOfGroupMemberNamesTest(8);
Console.ReadLine();*/

async Task getGroupsTest()
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var groupsList = await groupsRepository.getGroups();
    foreach (var group in groupsList)
    {
        Console.WriteLine($"Group Names: {group.GroupName}");
    }

}

async Task<Groups> getSingleGroupTest(int singleGroupId) 
{
    GroupsRepository groupsRepository = new(apiExecuter);
    return await groupsRepository.getSingleGroup(singleGroupId);
}

async Task getListOfGroupMemberNamesTest(int currentGroupId) 
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var groupListNames =  await groupsRepository.listofGroupMemberNames(currentGroupId);
    foreach (var groupName in groupListNames)
    {
        Console.WriteLine($"Group Member Names: {groupName}");
    }
}

async Task deleteSingleGroupTest(int deletedGroupId) 
{
    GroupsRepository groupsRepository = new(apiExecuter);
    await groupsRepository.deleteGroup(deletedGroupId);
}

async Task removeMemberFromGroupTest(int shrinkingGroupId, string deletedUserName)
{
    GroupsRepository groupsRepository = new(apiExecuter);
    await groupsRepository.removeMemberFromGroup(shrinkingGroupId, deletedUserName);
}

async Task addMemberToGroupTest() 
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var newlyAddedMember = new AddNewMemberToGroup
    {
        GroupId = 8,
        NewMemberUserName = "sejofbo@yahoo.com"
    };

    await groupsRepository.addGroupMember(newlyAddedMember);
}