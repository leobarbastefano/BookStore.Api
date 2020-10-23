using BookStore.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Api.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
        Task<User> Delete(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Update(User userParam, string password = null);
    }

}