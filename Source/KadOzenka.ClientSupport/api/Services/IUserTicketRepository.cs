using System.Linq;
using System.Threading.Tasks;
using api.Model;

namespace api.Services
{
    public interface IUserTicketRepository
    {
        void AddTicket(Ticket ticket);

        Task<IQueryable<Ticket>> GetTickets();
    }
}