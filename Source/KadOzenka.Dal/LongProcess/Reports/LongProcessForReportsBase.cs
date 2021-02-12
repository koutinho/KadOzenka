using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KadOzenka.Dal.LongProcess.Reports
{
	public abstract class LongProcessForReportsBase : LongProcess
	{
		protected CustomReportsService CustomReportsService { get; }

		protected LongProcessForReportsBase()
		{
			CustomReportsService = new CustomReportsService();
		}

		public virtual void AddToQueue(object input)
		{

		}

		protected void CheckCancellationToken(CancellationToken processCancellationToken,
	        CancellationTokenSource localCancellationToken, ParallelOptions options)
        {
	        if (!processCancellationToken.IsCancellationRequested)
		        return;

	        localCancellationToken.Cancel();
	        options.CancellationToken.ThrowIfCancellationRequested();
        }

		protected void WriteToStream(List<string> str, Encoding encoding, MemoryStream stream)
		{
			str.Add("\n");
			var headers = string.Join(',', str);
			byte[] firstString = encoding.GetBytes(headers);
			stream.Write(firstString, 0, firstString.Length);
		}
	}
}
