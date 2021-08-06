using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {
        public TicketsController(IUserTicketRepository userTicketRepository, ILogger<TicketsController> logger)
        {
            _userTicketRepository = userTicketRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Ticket>> Get()
        {
            var tickets = await _userTicketRepository.GetTickets();

            return tickets.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> AddTicket(Ticket ticket)
        {
            await _userTicketRepository.AddTicket(ticket);

            return Ok(ticket);
        }

        private IUserTicketRepository _userTicketRepository;

        private ILogger<TicketsController> _logger;
    }
}