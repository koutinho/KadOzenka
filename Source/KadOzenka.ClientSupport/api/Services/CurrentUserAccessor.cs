using System.Threading.Tasks;
using api.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor,  UserManager<DataLayer.ApplicationUser> userManager,
            ILogger<CurrentUserAccessor> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
                return null;

            return await _userManager.FindByNameAsync(userName);
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<CurrentUserAccessor> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
    }
}