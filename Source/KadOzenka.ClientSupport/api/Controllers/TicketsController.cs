using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        public TicketsController(IUserTicketRepository userTicketRepository, ILogger<TicketsController> logger)
        {
            _userTicketRepository = userTicketRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> Get()
        {
            List<Ticket> tickets;

            try
            {
                var ticketsQuery = await _userTicketRepository.GetTickets();
                
                tickets = ticketsQuery.ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> AddTicket(Ticket ticket)
        {
            try
            {
                await _userTicketRepository.AddTicket(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(ticket);
        }

        private IUserTicketRepository _userTicketRepository;

        private ILogger<TicketsController> _logger;
    }
}