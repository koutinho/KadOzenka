using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Messages;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.LongProcess;

namespace KadOzenka.Dal.LongProcess
{
	public abstract class LongProcess : ILongProcess
	{
		public abstract void StartProcess(OMProcessType processType, OMQueue processQueue,
			CancellationToken cancellationToken);

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}

		protected void SendNotification(OMQueue processQueue, string subject, string message)
		{
			new MessageService().SendMessages(new MessageDto
			{
				UserIds = processQueue.UserId.HasValue ? new long[] { processQueue.UserId.Value } : new long[] { },
				Subject = subject,
				Message = message,
				IsUrgent = true,
				IsEmail = true
			});
		}
	}
}
