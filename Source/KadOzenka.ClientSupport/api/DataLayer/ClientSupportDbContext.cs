using api.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;  
  
namespace api.DataLayer  
{  
    public class ClientSupportDbContext : IdentityDbContext<ApplicationUser>  
    {  
        public ClientSupportDbContext(DbContextOptions<ClientSupportDbContext> options)
            : base(options)  { }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(user => user.Tickets)
                .WithOne(ticket => ticket.User);
        }
    }  
}  