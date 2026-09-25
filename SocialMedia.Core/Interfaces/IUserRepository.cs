using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIDAsync(int id);
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
