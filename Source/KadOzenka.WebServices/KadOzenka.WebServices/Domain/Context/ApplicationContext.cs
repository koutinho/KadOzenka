using KadOzenka.WebServices.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace KadOzenka.WebServices.Domain.Context
{
	public class ApplicationContext: DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<ReonJournal> ReonJournal { get; set; }

		public DbSet<ExportTemplate> ExportTemplate { get; set; }

		public DbSet<TdInstance> TdInstances { get; set; }

		public DbSet<Tour> Tours { get; set; }

		public DbSet<Task> Tasks { get; set; }

		public DbSet<Unit> Units { get; set; }

		public DbSet<Group> Groups { get; set; }
	}
}