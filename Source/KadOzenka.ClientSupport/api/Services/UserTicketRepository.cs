using System;
using System.Linq;
using System.Threading.Tasks;
using api.DataLayer;
using api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class UserTicketRepository : IUserTicketRepository
    {
        public UserTicketRepository(ICurrentUserAccessor currentUserAccessor, ILogger<UserTicketRepository> logger,
            ClientSupportDbContext context)
        {
            _logger = logger;
            _currentUserAccessor = currentUserAccessor;
            _context = context;
        }

        public async Task AddTicket(Model.Ticket ticket)
        {
            var user = await _currentUserAccessor.GetCurrentUser();

            if (user == null)
                throw new Exception("Пользователь не найден");

            var dbTicket = new DataLayer.Ticket
            {
                KadNumber = ticket.KadNumber,
                Content = ticket.Content,
                UserId = user.Id
            };

            _context.Tickets.Add(dbTicket);

            await _context.SaveChangesAsync();

            ticket.TicketId = dbTicket.TicketId;
        }

        public async Task<IQueryable<Model.Ticket>> GetTickets()
        {
            var user = await _currentUserAccessor.GetCurrentUser(); 

            if (user == null)
                throw new Exception("Пользователь не найден");

            return _context.Tickets.AsNoTracking()                
                .Where(ticket => ticket.User == user)
                .Select(ticket => new Model.Ticket
                {
                    TicketId = ticket.TicketId,
                    KadNumber = ticket.KadNumber,
                    Content = ticket.Content
                });
        }

        private readonly ILogger<UserTicketRepository> _logger;
        ICurrentUserAccessor _currentUserAccessor;
        ClientSupportDbContext _context;
    }
}