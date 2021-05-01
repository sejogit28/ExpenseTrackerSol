using ExpenseTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerBlazorWASMClient.RestApiConnectors
{
    public interface IAuthService
    {
        Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userRegistrationDto);
    }
}
