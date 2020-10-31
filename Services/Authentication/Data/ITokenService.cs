using Database.Entities;
using System.Threading.Tasks;

namespace Authentication.Data
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
