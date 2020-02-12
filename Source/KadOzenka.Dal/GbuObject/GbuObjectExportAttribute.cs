using System;
using System.Text;
using System.Data;
using System.Globalization;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

using Core.Register;
using Core.Numerator;
using Core.Shared.Misc;
using Core.Shared.Extensions;
using Core.Register.RegisterEntities;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Gbu.ExportAttribute;

namespace KadOzenka.Dal.GbuObject
{
    /// <summary>
    /// Перенос атрибутов из ГБУ в КО
    /// </summary>
    public class ExportAttributeToKO
    {
        /// <summary>
        /// Выполнение операции переноса атрибутов
        /// </summary>
        public static void Run(GbuExportAttributeSettings setting)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 20
            };


            if (setting.TaskFilter.Count > 0)
            {
                foreach(long taskId in setting.TaskFilter)
                {
                    List<ObjectModel.KO.OMUnit> Objs = ObjectModel.KO.OMUnit.Where(x => x.TaskId == taskId).SelectAll().Execute();
                    //Parallel.ForEach(Objs, options, item => { RunOneUnit(item, setting); });
                }
            }
        }
    }

}
