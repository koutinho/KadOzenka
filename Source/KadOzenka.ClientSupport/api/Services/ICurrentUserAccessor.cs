using System.Threading.Tasks;
using api.DataLayer;

namespace api.Services
{
    public interface ICurrentUserAccessor
    {
        Task<ApplicationUser> GetCurrentUser();
    }
}