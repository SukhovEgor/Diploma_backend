using Application.Interfaces;
using Domain.CalculationProbability;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetUsers()
        {
            return _userRepository.GetAllUsers().Result;
        }

        public async Task<User> Login(string login, string password)
        {
            var user = _userRepository.Login(login, password).Result;
            if (user == null) throw new Exception($"Неверные имя пользовователя или пароль");
            return user;
        }

        public async Task CreateUser(User user)
        {
            await _userRepository.CreateUser(user);
        }

        public async Task DeleteUserById(string? id)
        {
            await _userRepository.DeleteUserById(id);
        }
    }
}
