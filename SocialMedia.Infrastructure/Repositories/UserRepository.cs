using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly SocialMediaContext _socialMediaContext;

        public UserRepository(SocialMediaContext socialMediaContext)
        {
            _socialMediaContext = socialMediaContext;
        }
        public Task DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertUser(User user)
        {
            throw new Exception("No existe el user");
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
