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

		public DbSet<ExportTemplate> ExportTemplate { get; set; }


		/// <summary>
		/// map db Instances
		/// </summary>
		public DbSet<TdInstance> TdInstances { get; set; }

		/// <summary>
		/// map db tours
		/// </summary>
		public DbSet<Tour> Tours { get; set; }

		/// <summary>
		/// map db tasks
		/// </summary>
		public DbSet<Task> Tasks { get; set; }
	
	}
}