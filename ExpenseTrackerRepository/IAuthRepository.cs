using ExpenseTrackerModels;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository
{
    public interface IAuthRepository
    {
        Task<RegistrationResponseDto> RegisterExpUser(UserRegistrationDto userRegistrationDto);

        Task<LoginResponseDto> Login(LoginAuthenticationDto loginAuthenticationDto);

        Task Logout();
    }
}