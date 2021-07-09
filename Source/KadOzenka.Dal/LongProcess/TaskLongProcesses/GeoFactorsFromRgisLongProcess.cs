using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CadAppraisalDataApi.Models;
using Core.Register;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using KadOzenka.WebClients.RgisClient.Api;
using KadOzenka.WebClients.RgisClient.Client;
using KadOzenka.WebClients.RgisClient.Model;
using Microsoft.Practices.ObjectBuilder2;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;
using Serilog;

namespace KadOzenka.Dal.LongProcess.TaskLongProcesses
{
	public class GeoFactorsFromRgisLongProcess
	{
		private readonly ILogger _log = Log.ForContext<GeoFactorsFromRgisLongProcess>();
		object lockMy = new ();

		private readonly RgisDataApi _rgisDataApi;
		public GeoFactorsFromRgisLongProcess()
		{
			_rgisDataApi = new RgisDataApi();
		}

		public void StartProcess(long taskId, List<long> idsFactors)
		{
			var messageSubject = "Получение географических факторов из РГИС";

			var task = GetTask(taskId);
			var document = GetDocument(task.DocumentId);
			var units = GetUnits(task.Id);


			using var gbuReportService = new GbuReportService("Получение географических факторов из РГИС");

			var zuUnits = units.Where(x => x.PropertyType_Code == PropertyTypes.Stead).ToList();
			var zuLayers = GetLayersZuOnlySelected(idsFactors);
			var oksUnits = units.Where(x => x.PropertyType_Code != PropertyTypes.Stead).ToList();
			var oksLayers = GetLayersOksOnlySelected(idsFactors);

			var options = new ParallelOptions
			{
				MaxDegreeOfParallelism = 10,
				CancellationToken = CancellationToken.None //todo replase LP
			};

			
			
			List<Task> tasks = new();
			if (zuLayers.Count > 0)
			{
				int countParamsToRequest = 100;
				var groupingUnits = GetGroupingUnits(zuUnits, zuLayers, countParamsToRequest);

				 var zuTask = Task.Run(() =>
				{
					Parallel.ForEach(groupingUnits, options, omUnits =>
					{
						try
						{
							int countParams = omUnits.Count * zuLayers.Count;
							if (countParams > 1)
							{
								ApiResponse<ResponseData> response = _rgisDataApi.GetDistanceZuFactorsValue(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = zuLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, zuUnits, zuLayers, document);

							}
							else
							{
								ApiResponse<ResponseDataSingle> response = _rgisDataApi.GetDistanceZuFactorsValueSingle(new RequestData
								{
									KadNumbers = omUnits.Select(x => x.CadastralNumber).ToList(),
									Layers = zuLayers.Select(x => x.LayerName).ToList()
								});

								PrepareResponseData(response.Data, zuUnits, zuLayers, document);
							}

						}
						catch (Exception e)
						{
							_log.Error(e, e.Message);
							//report 
						}

					});

				});

				 tasks.Add(zuTask);
			}

			if (oksLayers.Count > 0)
			{
				int countParamsToRequest = 100;
				var groupingUnits = GetGroupingUnits(oksUnits, oksLayers, countParamsToRequest);
			}

			Task.WaitAll(tasks.ToArray());
			//ObjectType

		}


		#region support methods

		private OMTask GetTask(long taskId)
		{
			var task = OMTask.Where(x => x.Id == taskId)
				.Select(x => x.EstimationDate)
				.Select(x => x.DocumentId)
				.ExecuteFirstOrDefault();

			if (task == null)
				throw new Exception($"Не найдено задание на оценку с Id ='{taskId}'");

			return task;
		}

		private OMInstance GetDocument(long? documentId)
		{
			return OMInstance.Where(x => x.Id == documentId)
				.Select(x => x.Id)
				.Select(x => x.CreateDate)
				.ExecuteFirstOrDefault();
		}

		private List<OMUnit> GetUnits(long taskId)
		{
			return OMUnit.Where(x =>
					x.TaskId == taskId)
				.Select(x => x.CadastralNumber)
				.Select(x => x.ObjectId)
				.Select(x => x.PropertyType_Code)
				.Execute();
		}


		private List<OMRgisLayers> GetLayers(List<long> idsFactors)
		{
			return OMRgisLayers.Where(x => idsFactors.Contains(x.Id)).SelectAll().Execute();
		}

		private List<OMRgisLayers> GetLayersZuOnlySelected(List<long> idsFactors)
		{
			var idsZuFactors = RegisterCache.RegisterAttributes.Values
				.Where(x => x.RegisterId == OMSource26.GetRegisterId()).Select(x => x.Id);
			var selectedIdsZuFactors = idsFactors.Where(x => idsZuFactors.Contains(x)).ToList();

			return selectedIdsZuFactors.Any() 
				? OMRgisLayers.Where(x => selectedIdsZuFactors.Contains(x.Id)).SelectAll().Execute().ToList() 
				: new ();
		}

		private List<OMRgisLayers> GetLayersOksOnlySelected(List<long> idsFactors)
		{
			var idsOksFactors = RegisterCache.RegisterAttributes.Values
				.Where(x => x.RegisterId == OMSource25.GetRegisterId()).Select(x => x.Id);
			var selectedIdsOksFactors = idsOksFactors.Where(idsFactors.Contains).ToList();

			return selectedIdsOksFactors.Any() 
				?  OMRgisLayers.Where(x => selectedIdsOksFactors.Contains(x.Id)).SelectAll().Execute().ToList() 
				: new ();
		}

		private List<List<OMUnit>> GetGroupingUnits(List<OMUnit> units, List<OMRgisLayers> layers, int count)
		{
			int availableCount = count / layers.Count;

			if (availableCount > 1)
			{
				var res = new List<List<OMUnit>>();
				int unitsGroup = units.Count % availableCount == 0
					? units.Count / availableCount
					: (units.Count / availableCount) + 1;


				int unitsInOneGroup = units.Count % unitsGroup == 0 
					? units.Count / unitsGroup 
					: (units.Count / unitsGroup) + 1;

				int counter = 0;
				for (int i = 0; i < unitsGroup; i++)
				{
					var tempList = new List<OMUnit>();
					for (int j = 0; j < unitsInOneGroup; j++)
					{
						if (counter == units.Count) break;
						tempList.Add(units[counter]);
						counter++;
					}
					res.Add(tempList);
				}

				return res;
			}

			return units.Select(x => new List<OMUnit> {x}).ToList();
		}


		private void SaveFactor(long objectId, long attributeId, RegisterAttributeType attributeType,
			double distance, OMInstance taskDocument)
		{
			_log.ForContext("distance", distance)
				.ForContext("attributeId", attributeId)
				.Debug("Сохранение Ргис фактора");

			var gbuObjectAttribute = new GbuObjectAttribute
			{
				ObjectId = objectId,
				AttributeId = attributeId,
				NumValue = (decimal)distance,
				S = taskDocument.CreateDate,
				Ot = taskDocument.CreateDate,
				ChangeDocId = taskDocument.Id,
				ChangeUserId = SRDSession.Current.UserID,
				ChangeDate = DateTime.Now
			};

			gbuObjectAttribute.Save();
		}


		private void PrepareResponseData(ResponseData responseData, List<OMUnit> units, List<OMRgisLayers> layers, OMInstance doc)
		{
			foreach (var res in responseData.Result)
			{
				var objId = units.FirstOrDefault(x => x.CadastralNumber == res[0].Params.KadNumber)?.ObjectId;
				var attrId = layers.FirstOrDefault(x => x.LayerName == res[0].Params.LayerName)?.Id;
				var attrType = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == attrId)?.Type ?? RegisterAttributeType.DECIMAL;
				if (objId != null && res[0].Distance != null && attrId != null)
				{
					SaveFactor(objId.Value, attrId.Value, attrType, res[0].Distance.Value, doc);
				}
				else
				{
					_log.ForContext("attrId", attrId)
						.ForContext("objId", objId)
						.ForContext("Distance", res[0].Distance).Warning("Добавление фактора не выполненео т.к attrId, objId или Distance равны null");
				}
			}
		}

		private void PrepareResponseData(ResponseDataSingle responseData, List<OMUnit> units, List<OMRgisLayers> layers, OMInstance doc)
		{
			foreach (var res in responseData.Result)
			{
				var objId = units.FirstOrDefault(x => x.CadastralNumber == res.Params.KadNumber)?.ObjectId;
				var attrId = layers.FirstOrDefault(x => x.LayerName == res.Params.LayerName)?.Id;
				var attrType = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.Id == attrId)?.Type ?? RegisterAttributeType.DECIMAL;
				if (objId != null && res.Distance != null && attrId != null)
				{
					SaveFactor(objId.Value, attrId.Value, attrType, res.Distance.Value, doc);
				}
				else
				{
					_log.ForContext("attrId", attrId)
						.ForContext("objId", objId)
						.ForContext("Distance", res.Distance).Warning("Добавление фактора не выполненео т.к attrId, objId или Distance равны null");
				}
			}
		}



		#endregion


	}
}