using System;
using System.Threading.Tasks;
using api.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor,  UserManager<DataLayer.ApplicationUser> userManager,
            ILogger<CurrentUserAccessor> logger, IMemoryCache memoryCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userManager = userManager;
            _cache = memoryCache;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
                return null;

            ApplicationUser user = null;

            if (!_cache.TryGetValue(userName, out user))
            {
                user = await _userManager.FindByNameAsync(userName);
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(userName, user, cacheEntryOptions);
            }
            
            return user;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<CurrentUserAccessor> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        private IMemoryCache _cache;
    }
}