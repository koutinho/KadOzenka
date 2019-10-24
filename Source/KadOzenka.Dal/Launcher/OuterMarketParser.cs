using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using ObjectModel.Core.LongProcess;
using Core.Register.LongProcessManagment;

namespace OuterMarketParser.Launcher
{

    public class OuterMarketParser : ILongProcess
    {

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {

        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            throw new NotImplementedException();
        }

        public bool Test() => true;

    }

}
