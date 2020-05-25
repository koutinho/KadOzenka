using KadOzenka.WebServices.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KadOzenka.WebServices.Domain.Context
{
	public class ApplicationContext: DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<ReonJournal> ReonJournal { get; set; }
	
	}
}