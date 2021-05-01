using ExpenseTrackerApi.Models;
using ExpenseTrackerBlazorWASMClient.RestApiConnectors;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Registration
{
    private UserRegistrationDto _userRegistrationDto = new UserRegistrationDto();

    [Inject]
    public IAuthService AuthService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public bool ShowRegErrors { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public async Task Register()
    {
        ShowRegErrors = false;

        var result = await AuthService.RegisterUser(_userRegistrationDto);
        if (!result.IsSuccessfulRegistration)
        {
            Errors = result.Errors;
            ShowRegErrors = true;
        }
        else
        {
            NavigationManager.NavigateTo("/");
        }
    }
}