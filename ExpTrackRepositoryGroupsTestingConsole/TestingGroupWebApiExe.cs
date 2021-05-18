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

//Console.WriteLine("///////////////");
//Console.WriteLine("Reading All Names of All Groups");
//await getGroupsTest();
//Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("Reading All Names of the groups that a specific is in");
//await getGroupsByUserTest("sejogoo@gmail.com");
//Console.ReadLine();

//Console.WriteLine("///////////////");
//Console.WriteLine("Creating A New Group");
//await createGroupTest();
//Console.ReadLine();


//Console.WriteLine("///////////////");
//Console.WriteLine("Reading All Names of All Groups Once Again");
//await getGroupsTest();
//Console.ReadLine();


/*Console.WriteLine("///////////////");
Console.WriteLine("Reading A Groups Various Detail");
var singleGroup = await getSingleGroupTest(9);
Console.WriteLine(singleGroup.GroupsId);
Console.WriteLine(singleGroup.GroupName);
Console.WriteLine(singleGroup.DateCreated);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Updating A Single Groups Name");
await updateGroupTest(singleGroup);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Groups Various Detail Once Again");
Console.WriteLine(singleGroup.GroupsId);
Console.WriteLine(singleGroup.GroupName);
Console.WriteLine(singleGroup.DateCreated);
Console.ReadLine();*/


/*Console.WriteLine("///////////////");
Console.WriteLine("Deleting A Single Group");
await deleteSingleGroupTest(10);
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading All Names of All Groups Once Again");
await getGroupsTest();
Console.ReadLine();*/

/*Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Groups Member Names");
await getListOfGroupMemberNamesTest(8);
Console.ReadLine();*/

/*Console.WriteLine("///////////////");
Console.WriteLine("Adding a New Member to a Group");
await addMemberToGroupTest();
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Groups Member Names Once Again");
await getListOfGroupMemberNamesTest(8);
Console.ReadLine();*/

/*Console.WriteLine("///////////////");
Console.WriteLine("Deleting a Member from a group");
await removeMemberFromGroupTest(8, "sejoTestEmail1828@mailinator.com");
Console.ReadLine();

Console.WriteLine("///////////////");
Console.WriteLine("Reading A Single Groups Member Names Once Again");
await getListOfGroupMemberNamesTest(8);
Console.ReadLine();*/

/*Console.WriteLine("///////////////");
Console.WriteLine("Sending an email to a potential Group member");
await sendGroupInviteTest();
Console.ReadLine();*/

Console.WriteLine("///////////////");
Console.WriteLine("Confirming group invite as the invitee");
await inviteeConfirmTest();
Console.ReadLine();

/*Console.WriteLine("///////////////");
Console.WriteLine("Confirming the invitation as the inviter and then adding the invitee to the group");
await inviterConfirmTest();
Console.ReadLine();*/

async Task getGroupsTest()
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var groupsList = await groupsRepository.listOfGroups();
    foreach (var group in groupsList)
    {
        Console.WriteLine($"Group Names: {group.GroupName}");
    }

}

async Task getGroupsByUserTest(string userName)
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var groupsList = await groupsRepository.listOfGroupsByUser(userName);
    foreach (var group in groupsList)
    {
        Console.WriteLine($"Group Names: {group.Groups.GroupName}");
    }

}

async Task createGroupTest() 
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var newlyCreatedGroup = new Groups
    {
        GroupName = "Test Group 3",
        DateCreated = DateTime.Now,
        ExpenseTrackerUserId = "65052034-ea64-4ee1-92cd-75d07f61297a"
    };
    await groupsRepository.createGroup(newlyCreatedGroup, "sejogoo@gmail.com");
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

async Task updateGroupTest(Groups group) 
{
    GroupsRepository groupsRepository = new(apiExecuter);

    group.GroupsId = group.GroupsId;
    group.GroupName = "Updated Group Test 2";
    group.DateCreated = group.DateCreated;
    group.ExpenseTrackerUserId = group.ExpenseTrackerUserId;

    await groupsRepository.updateGroup(group);

}

async Task sendGroupInviteTest() 
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var initialInvite = new PossibleMemberInvite
    {
        InviteeEmail = "sejoTestEmail1828@mailinator.com",
        InviterEmail = "sejogoo@gmail.com",
        GroupId = 8
    };
    await groupsRepository.sendGroupInvite(initialInvite);

}

async Task inviteeConfirmTest()
{
    GroupsRepository groupsRepository = new(apiExecuter);
    var inviteeConfirmation = new PossibleMemberConfirm
    {
        InviteeEmail = "sejoTestEmail1828@mailinator.com",
        InviterEmail = "sejogoo@gmail.com",
        Password = "D8f8ndM",
        ConfirmPassword = "D8f8ndM@il",
        GroupId = 8
    };
     await groupsRepository.inviteeConfirm(inviteeConfirmation);

}

async Task inviterConfirmTest()
{
    GroupsRepository groupsRepository = new(apiExecuter);

    var inviterConfirmation = new PossibleMemberConfirm
    {
        InviteeEmail = "sejoTestEmail1828@mailinator.com",
        InviterEmail = "sejogoo@gmail.com",
        Password = "D8f8nd@Exp",
        ConfirmPassword = "D8f8nd@Exp",
        GroupId = 8
    };
    await groupsRepository.inviterConfirm(inviterConfirmation);

}