using System.Data;

namespace SocialMedia.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
