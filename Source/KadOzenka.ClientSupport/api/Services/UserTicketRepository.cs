using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class UserTicketRepository : IUserTicketRepository
    {
        public UserTicketRepository(ICurrentUserAccessor currentUserAccessor, ILogger<UserTicketRepository> logger)
        {
            _logger = logger;
            _currentUserAccessor = currentUserAccessor;
        }

        public void AddTicket(Ticket ticket)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IQueryable<Ticket>> GetTickets()
        {
            var user = await _currentUserAccessor.GetCurrentUser(); 
            
            _logger.LogInformation(user.Email);

            return null;
        }

        private readonly ILogger<UserTicketRepository> _logger;

        ICurrentUserAccessor _currentUserAccessor;
    }
}