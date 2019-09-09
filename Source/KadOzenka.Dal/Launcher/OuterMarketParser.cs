using ObjectModel.Core.LongProcess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Core.Register.LongProcessManagment;

using OuterMarketParser.LinksGenerator;
using OuterMarketParser.Model.DatabaseOperations;
using OuterMarketParser.Parser.Cian;

namespace OuterMarketParser.Launcher
{
    class OuterMarketParser : ILongProcess
    {

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            List<string> links = new LinkGenerator().GenerateCianLinks();
            Parser.Cian.Client client = new Parser.Cian.Client(new CianDataParser(links));
            Model.DatabaseOperations.Client dbCient = new Model.DatabaseOperations.Client(new DataToPostgreSQL(client.Parser.GetProperty()));
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            throw new NotImplementedException();
        }

        public bool Test() => true;
    }
}
