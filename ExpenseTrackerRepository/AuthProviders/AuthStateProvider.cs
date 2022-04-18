using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ExpenseTrackerRepository.ApiRouteFetcher;

namespace ExpenseTrackerRepository.AuthProviders
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymous;

        public AuthStateProvider(ILocalStorageService localStorage, IWebApiExecuter webApiExecuter) 
        {
            _localStorageService = localStorage;
            _webApiExecuter = webApiExecuter;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() 
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token)) 
            {

                return _anonymous;
            }

            var claims = JWTParser.ParseClaimsFromJWT(token);
            var expiry = claims.Where(claim => claim.Type.Equals("exp")).FirstOrDefault();

            if (expiry == null)
            {

                return _anonymous;
            }

            var dateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiry.Value));

            if(dateTime.UtcDateTime <= DateTime.UtcNow)
            {

                return _anonymous;
            }

            _webApiExecuter.AddAuthHeader(token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JWTParser.ParseClaimsFromJWT(token), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string token) 
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JWTParser.ParseClaimsFromJWT(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout() 
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
