using ExpenseTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpenseTrackerBlazorWASMClient.RestApiConnectors
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userRegistrationDto)
        {
            var content = JsonSerializer.Serialize(userRegistrationDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync("https://localhost:44344/api/accounts/registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result;
            }
            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }
    }
}
