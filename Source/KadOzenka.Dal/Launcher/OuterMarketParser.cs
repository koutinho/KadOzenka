﻿using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using OuterMarketParser.LinksGenerator;
using OuterMarketParser.DatabaseReader;
using ObjectModel.Core.LongProcess;
using OuterMarketParser.Parser.Cian;
using Core.Register.LongProcessManagment;
using OuterMarketParser.Model.DatabaseOperations;

namespace OuterMarketParser.Launcher
{

    public class OuterMarketParser : ILongProcess
    {

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            try
            {
                OuterMarketSettings settings = new OuterMarketSettings();
                List<string> links = new LinkGenerator().GenerateCianLinks(settings);
                Parser.Cian.Client client = new Parser.Cian.Client(new CianDataParser(links));
                Model.DatabaseOperations.Client dbCient = new Model.DatabaseOperations.Client(new DataToPostgreSQL(client.Parser.GetProperty()));
                settings.UpdateLastSuccesfulUpdateDate();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void StartProcess()
        {
            try
            {
                OuterMarketSettings settings = new OuterMarketSettings();
                List<string> links = new LinkGenerator().GenerateCianLinks(settings);
                Console.WriteLine("=====> " + links.Count);
                Parser.Cian.Client client = new Parser.Cian.Client(new CianDataParser(links));
                Model.DatabaseOperations.Client dbCient = new Model.DatabaseOperations.Client(new DataToPostgreSQL(client.Parser.GetProperty()));
                settings.UpdateLastSuccesfulUpdateDate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("=====> Writen To Postgres");
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            throw new NotImplementedException();
        }

        public bool Test() => true;

    }

}
