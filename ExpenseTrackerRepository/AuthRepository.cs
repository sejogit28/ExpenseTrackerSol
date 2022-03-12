using ExpenseTrackerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using ExpenseTrackerRepository.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ExpenseTrackerRepository.ApiRouteFetcher;
using ExpenseTrackerModels.AuthModels;

namespace ExpenseTrackerRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorageService;
        private readonly string localApiDomain = "https://localhost:5001";
        private readonly string remoteApiDomain = "https://expense-tracker-api28.herokuapp.com";
        public AuthRepository(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorageService, IWebApiExecuter webApiExecuter)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<RegistrationResponseDto> RegisterExpUser(UserRegistrationDto userRegistrationDto)
        {
            var expContent = JsonSerializer.Serialize(userRegistrationDto);
            var bodyContent = new StringContent(expContent, Encoding.UTF8, "application/json");

            var expRegisterResult = await _client.PostAsync(localApiDomain + "/api/accounts/Registration", bodyContent);
            var expRegisterContent = await expRegisterResult.Content.ReadAsStringAsync();

            if (!expRegisterResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(expRegisterContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<LoginResponseDto> Login(LoginAuthenticationDto loginAuthenticationDto)
        {
            var content = JsonSerializer.Serialize(loginAuthenticationDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var loginResult = await _client.PostAsync(localApiDomain + "/api/accounts/login", bodyContent);
            var loginContent = await loginResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginResponseDto>(loginContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!loginResult.IsSuccessStatusCode)
            {

                return result;
            }

            await _localStorageService.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return new LoginResponseDto { IsLoginSuccessful = true };
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

    }
}
