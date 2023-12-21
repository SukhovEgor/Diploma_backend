using Domain.CalculationProbability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> Login(string login, string password);
    }
}
