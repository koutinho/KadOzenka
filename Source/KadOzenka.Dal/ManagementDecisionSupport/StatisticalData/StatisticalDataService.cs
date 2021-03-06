using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CommonSdks;
using Core.ConfigParam;
using Core.Register.QuerySubsystem;
using KadOzenka.Dal.ManagementDecisionSupport.Dto.StatisticalData;
using KadOzenka.Dal.ManagementDecisionSupport.Enums;
using ObjectModel.KO;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using KadOzenka.Dal.CancellationQueryManager;
using KadOzenka.Dal.LongProcess.Reports;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfCalculations;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms.AnalysisOfResuluts;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports;
using KadOzenka.Dal.LongProcess.Reports.PricingFactorsComposition.Reports.ResultComposition;
using KadOzenka.Dal.LongProcess.Reports.QualityPricingFactorsEncodingResults;
using KadOzenka.Dal.LongProcess.Reports.ResultsForApproval;
using KadOzenka.Dal.Tours;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{
    public class StatisticalDataService
    {
	    public string SqlQueriesFolder => "SqlQueries";

	    public readonly QueryManager QueryManager;
        private TourFactorService TourFactorService { get; set; }

        public static readonly Dictionary<long?, Type> ReportsViaLongLongProcess =
	        new Dictionary<long?, Type>
	        {
		        {(long) StatisticalDataType.PricingFactorsCompositionFinalUniform, typeof(UniformReportLongProcess)},
		        {(long) StatisticalDataType.PricingFactorsCompositionFinalNonuniform, typeof(NonUniformReportLongProcess)},
		        {(long) StatisticalDataType.PricingFactorsCompositionForOks, typeof(OksReportLongProcess)},
		        {(long) StatisticalDataType.PricingFactorsCompositionForZu, typeof(ZuReportLongProcess)},
		        {(long) StatisticalDataType.QualityPricingFactorsEncodingResultsGrouping, typeof(GroupingReportLongProcess)},
		        {(long) StatisticalDataType.AdditionalFormsCalculationsAnalysis, typeof(AnalysisOfCalculationsLongProcess)},
		        {(long) StatisticalDataType.InfoAboutCadastralCostDeterminingMethod, typeof(InfoAboutCadastralCostDeterminingMethodReportLongProcess)},
		        {(long) StatisticalDataType.AdditionalFormsResultsAnalysis, typeof(AnalysisOfResultsLongProcess)},
		        {(long) StatisticalDataType.ResultsForApproval, typeof(ResultsForApprovalLongProcess)},
		        {(long) StatisticalDataType.AdditionalFormsChangesUploading, typeof(ChangesUploadingReportLongProcess)}
	        };

        public StatisticalDataService()
        {
            TourFactorService = new TourFactorService();
            QueryManager = new QueryManager();
        }



        #region Sql Files Content

        public FileStream GetSqlQueryFileStream(string fileName)
        {
	        return Configuration.GetFileStream(fileName, "sql", SqlQueriesFolder);
        }

        public string GetSqlFileContent(string folder, string fileName)
        {
	        var pathToFile = FileSystemHelper.CombinePath("StatisticalData", folder, fileName);

            string contents;
	        using (var sr = new StreamReader(GetSqlQueryFileStream(pathToFile)))
	        {
		        contents = sr.ReadToEnd();
	        }

	        return contents;
        }

        #endregion

        public QSQuery GetQueryForUnitsByTasks(long[] taskIdList, List<QSCondition> additionalConditions = null, List<QSJoin> additionalJoins = null)
        {
            var conditions = new List<QSCondition>
            {
                new QSConditionSimple(OMUnit.GetColumn(x => x.TaskId), QSConditionType.In, taskIdList.Select(x => (double) x).ToList())
            };
            additionalConditions?.ForEach(x => conditions.Add(x));

            var query = new QSQuery
            {
                MainRegisterID = OMUnit.GetRegisterId(),
                Condition = new QSConditionGroup
                {
                    Type = QSConditionGroupType.And,
                    Conditions = conditions
                },
                Joins = additionalJoins
            };

            return query;
        }

        public string GetRegionNumberByCadastralQuarter(string cadastralQuartal)
        {
	        var delimeterIndex = cadastralQuartal.IndexOf(':', cadastralQuartal.IndexOf(':') + 1);
	        return cadastralQuartal.Substring(0, delimeterIndex);
        }

        public decimal? GetCalcValue<T>(UpksCalcType upksCalcType, List<T> dtoList) where T : CalcDto
        {
	        decimal? result = null;
	        switch (upksCalcType)
	        {
		        case UpksCalcType.Min:
			        result = dtoList.Min(x => x.ObjectValue);
			        break;
		        case UpksCalcType.Max:
			        result = dtoList.Max(x => x.ObjectValue);
			        break;
		        case UpksCalcType.Average:
			        result = dtoList.Average(x => x.ObjectValue);
			        break;
		        case UpksCalcType.AverageWeight:
                    var totalCost = dtoList.Sum(x => x.ObjectCost);
                    var totalSquare = dtoList.Sum(x => x.ObjectSquare);
                    result = totalSquare != 0 ? totalCost / totalSquare : null;
			        break;
	        }

	        return result;
        }


        #region Tour Settings

        public RegisterAttribute GetGroupAttributeFromTourSettings(long tourId)
        {
            return GetTourAttribute(tourId, KoAttributeUsingType.CodeGroupAttribute);
        }

        public RegisterAttribute GetCadastralQuartalAttributeFromTourSettings(long tourId)
        {
            return GetTourAttribute(tourId, KoAttributeUsingType.CodeQuarterAttribute);
        }

        private RegisterAttribute GetTourAttribute(long tourId, KoAttributeUsingType attributeUsingType)
        {
            var attribute = TourFactorService.GetTourAttributeFromSettings(tourId, attributeUsingType);
            if (attribute == null)
                throw new Exception($"Для тура с Id='{tourId}' не задан {attributeUsingType.GetEnumDescription()}");

            return attribute;
        }

        #endregion


        #region Long Processes

        public void AddProcessToQueue(long? modelReportType, object parameters)
        {
	        if (modelReportType == null)
		        throw new Exception("Не передан тип отчета");
	        if (!ReportsViaLongLongProcess.TryGetValue(modelReportType.Value, out var longProcessType))
		        throw new Exception("Формирование отчета через фоновый процесс недоступно");

            var longProcess = (LongProcessForReportsBase)Activator.CreateInstance(longProcessType);

			////TODO код для отладки
			//longProcess.StartProcess(new OMProcessType(), new OMQueue
			//{
			//	Status_Code = Status.Added,
			//	UserId = SRDSession.GetCurrentUserId(),
			//	Parameters = parameters.SerializeToXml()
			//}, new CancellationToken());

			longProcess.AddToQueue(parameters);
		}

        #endregion
    }
}
