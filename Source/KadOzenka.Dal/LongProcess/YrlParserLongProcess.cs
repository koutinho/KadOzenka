using System;
using System.IO;
using System.Threading;
using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using JetBrains.Annotations;
using KadOzenka.Dal.ChunkUpload;
using ObjectModel.Core.LongProcess;
using Serilog;

namespace KadOzenka.Dal.LongProcess
{
    [UsedImplicitly]
    public class YrlParserLongProcess : LongProcess
    {
        private const string LongProcessName = nameof(YrlParserLongProcess);
        private ILogger _log = Log.ForContext<YrlParserLongProcess>();

        public static void AddProcessToQueue(Guid uuid)
        {
            LongProcessManager.AddTaskToQueue(LongProcessName, parameters: uuid.SerializeToXml());
        }

        public override void StartProcess(OMProcessType processType, OMQueue processQueue,
            CancellationToken cancellationToken)
        {
            var fileUploadGuid = processQueue.Parameters.DeserializeFromXml<Guid>();
            var files = ChunkUploadManager.Instance().GetFilesByUuid(fileUploadGuid);
            foreach (var file in files)
            {
                file.FileStream.Seek(0, SeekOrigin.Begin);
                var parser = new YRLParser.YrlFeedParser(file.FileStream);
                parser.ParseAndCreate();
            }
        }
    }
}