using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mvc.NET.Models;

namespace mvc.NET.Services.Users
{

    public interface IUsersService
    {
        Task<User> GetUserAsync(Guid id);

        Task<IEnumerable<User>> GetUsersAsync();

        Task CreateUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(Guid id);
    }
}