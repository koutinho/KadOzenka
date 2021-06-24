using System.Threading;
using System.Threading.Tasks;
using KadOzenka.Dal.LongProcess._Common;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses.CadastralPriceCalculation.Entities
{
	public class ProcessConfiguration : ParallelThreadsConfig
	{
		public int NumberOfPackages { get; set; }

		public ParallelOptions ParallelOptions { get; set; }
		public CancellationTokenSource CancellationTokenSource { get; set; }

		public ProcessConfiguration(int packageSize, int threadsCount, int generalUnitsCount)
		{
			PackageSize = packageSize;
			ThreadsCount = threadsCount;
			NumberOfPackages = generalUnitsCount / PackageSize + 1;
			
			CancellationTokenSource = new CancellationTokenSource();
			ParallelOptions = new ParallelOptions
			{
				CancellationToken = CancellationTokenSource.Token,
				MaxDegreeOfParallelism = ThreadsCount
			};
		}
	}
}
