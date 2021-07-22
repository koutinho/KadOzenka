using api.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;  
  
namespace api.DataLayer  
{  
    public class ClientSupportDbContext : IdentityDbContext<ApplicationUser>  
    {  
        public ClientSupportDbContext(DbContextOptions<ClientSupportDbContext> options)
            : base(options)  { }
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
    }  
}  