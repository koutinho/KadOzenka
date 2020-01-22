using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Sud;

namespace KadOzenka.Dal.LongProcess
{
	public class AdditionalAnalysisChecker: ILongProcess
	{

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			DbCommand command = DBMngr.Main.GetStoredProcCommand("additional_analysis_checker", 0);
			DBMngr.Main.ExecuteNonQuery(command);
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			throw new NotImplementedException();
		}

		public bool Test()
		{
			return true;
		}
	}
}