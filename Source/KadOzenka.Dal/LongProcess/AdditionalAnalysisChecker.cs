using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using ObjectModel.Core.LongProcess;
using ObjectModel.Sud;

namespace KadOzenka.Dal.LongProcess
{
	public class AdditionalAnalysisChecker: ILongProcess
	{
		public static void TestQuery()
		{
			var test = new QSJoin {JoinType = QSJoinType.Inner};
			test.RegisterId = 316;
			QSQuery query = new QSQuery();
			query.Joins = new List<QSJoin>{test};

			//var data = OMObject.Where(x => x.SudLink[0]).Select(x => new
			//{
			//	x.SudLink
			//}).Execute();

		}
		
		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			OMObject.Where(x => x.SudLink.FindAll(y => y.Sud.SudDate > x.Date));
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