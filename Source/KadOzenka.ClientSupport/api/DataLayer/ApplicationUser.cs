using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace api.DataLayer 
{
    public class ApplicationUser: IdentityUser  
    {
        public List<Ticket> Tickets { get; set; }
    }
}