using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context) { }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Login == login.User
            && x.Password == login.Password);
        }
    }

}
