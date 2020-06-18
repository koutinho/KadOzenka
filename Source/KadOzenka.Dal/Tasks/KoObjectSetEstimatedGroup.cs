using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared.Extensions;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.Register;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.KoObject
{
	#region TypeStructure

	public enum ReportColumns: int
	{
		KnColumn = 0,
		InputFieldColumn = 1,
		ValueColumn = 2,
		OutputFieldColumn = 3,
		ErrorColumn = 4
	}
	public struct ValueItem
	{
		public string Value { get; set; }
		public long? IdDocument { get; set; }
	}

	public struct ComplianceGuid
	{
		public string Group { get; set; }
		public string Code { get; set; }
		public string TypeRoom { get; set; }
		public long SubGroup { get; set; }
	}

	public class EstimatedGroupModel
	{
		public long IdTask { get; set; }
		public long IdCodeGroup { get; set; }
		public long IdCodeQuarter { get; set; }
		public long IdTypeRoom { get; set; }

		/// <summary>
		/// Result parameter.
		/// </summary>
		public long IdEstimatedSubGroup { get; set; }
	}

	#endregion


	public class KoObjectSetEstimatedGroup
	{
		/// <summary>
		/// Объект для блокировки счетчика в многопоточке
		/// </summary>
		public static object locked;

		public static int CountAllUnits;

		public static int SuccessCount;


		public static long Run(EstimatedGroupModel param)
		{
			var reportService = new GbuReportService();
			reportService.AddHeaders(0, new List<string>{"КН", "Поле в которое производилась запись", "Внесенное значение", "Источник внесенного значения", "Ошибка" });
			locked = new object();
			var units = OMUnit.Where(x => x.TaskId != null && x.TaskId == param.IdTask).SelectAll().Execute().ToList();
			CountAllUnits = units.Count;

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 1
			};

			var estimatedSubGroupAttribute = OMAttribute.Where(x => x.Id == param.IdEstimatedSubGroup).SelectAll()
				.ExecuteFirstOrDefault();

			var codeGroupAttribute = OMAttribute.Where(x => x.Id == param.IdCodeGroup).SelectAll()
				.ExecuteFirstOrDefault();

			Parallel.ForEach(units, options, item =>
			{
				int rowReport;
				lock (locked)
				{
					rowReport = reportService.GetCurrentRow();
				}

				reportService.AddValue(item.CadastralNumber, (int)ReportColumns.KnColumn, rowReport);
				var gbuObject = OMMainObject.Where(x => x.Id == item.ObjectId).SelectAll().ExecuteFirstOrDefault();

		
				// берем код группы (значение из справочника цод)
				var codeGroup = GetValueFactor(gbuObject, codeGroupAttribute.RegisterId, codeGroupAttribute.Id);
				if (string.IsNullOrEmpty(codeGroup.Value))
				{
					lock (locked)
					{
						AddErrorRow($"Не найдено значение из справочника ЦОД для объекта {gbuObject.CadastralNumber}", rowReport, reportService);
					}
					return;
				}

				var complianceGuides = GetComplianceGuides(OMComplianceGuide.Where(x => x.Code == codeGroup.Value && x.TypeProperty == item.PropertyType).SelectAll().Execute());

				if (complianceGuides.Count == 1)
				{
                    var value = complianceGuides[0].Group;
                    AddValueFactor(gbuObject, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value);
					AddRowToReport(rowReport, item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value, reportService);
					return;
				}

				if (complianceGuides.Count <= 1)
				{
					AddErrorRow($"Не найдено значение в таблице сопоставления {gbuObject.CadastralNumber}", rowReport, reportService);
					return;
				}
				{
					if (complianceGuides.Any(x => x.TypeRoom != null && x.TypeRoom != KoTypeOfRoom.None.GetEnumDescription()) )
					{
						var attributeRoom = OMAttribute.Where(x => x.Id == param.IdTypeRoom).SelectAll()
							.ExecuteFirstOrDefault();
						// берем тип помещения
						ValueItem typeRoom = GetValueFactor(gbuObject, attributeRoom.RegisterId, attributeRoom.Id);
						if (string.IsNullOrEmpty(typeRoom.Value))
						{
							AddErrorRow($"Не найден тип помещения для объекта {gbuObject.CadastralNumber}", rowReport, reportService);
							return;
						}
						var group = complianceGuides.FirstOrDefault(x => x.TypeRoom == typeRoom.Value);
						if (group.TypeRoom == null)
						{
							AddValueFactor(gbuObject, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, group.Group);
							AddRowToReport(rowReport, item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, group.Group, reportService);
							return;
						}
						
					}

					var attributeQuarter = OMAttribute.Where(x => x.Id == param.IdCodeQuarter).SelectAll()
						.ExecuteFirstOrDefault();
					// берем кадастровый квартал
					ValueItem codeQuarter = GetValueFactor(gbuObject, attributeQuarter.RegisterId, attributeQuarter.Id);

					if (string.IsNullOrEmpty(codeQuarter.Value))
					{
						AddErrorRow($"Не найден кадастровый квартал для объекта {gbuObject.CadastralNumber}.", rowReport, reportService);
						return;
					}

					var kv = OMKadastrKvartal.Where(x => x.KadastrKvartal == codeQuarter.Value).SelectAll().ExecuteFirstOrDefault();
					if (kv == null)
					{
						AddErrorRow($"Не найден кадастровый квартал {codeQuarter.Value}. Необходимо обновить справочник", rowReport, reportService);
						return;
					}
					var task = OMTask.Where(x => x.Id == param.IdTask).SelectAll().ExecuteFirstOrDefault();
					var tourYear = OMTour.Where(x => x.Id == task.TourId).SelectAll().ExecuteFirstOrDefault().Year;

					switch (tourYear)
					{
						case 2017:
                            var value2017 = complianceGuides.FirstOrDefault(x => x.SubGroup == kv.TypeTerritory2017).Group;
                            AddValueFactor(gbuObject, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value2017);
							AddRowToReport(rowReport, item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value2017, reportService);
							break;
						case 2020:
						case 2018:
                            var value2020 = complianceGuides.FirstOrDefault(x => x.SubGroup == kv.TypeTerritory2020).Group;
                            AddValueFactor(gbuObject, estimatedSubGroupAttribute.Id, codeGroup.IdDocument, DateTime.Now, value2020);
							AddRowToReport(rowReport, item.CadastralNumber, estimatedSubGroupAttribute.Id, codeGroupAttribute.Id, value2020, reportService);
							break;
						default:
						{
							AddErrorRow("Для выбраного тура не предусмотренны параметры проставления оценки", rowReport, reportService);
							return;
						}
					}
				}
			});
			reportService.SetStyle();
			reportService.SetIndividualWidth((int)ReportColumns.KnColumn, 4);
			reportService.SetIndividualWidth((int)ReportColumns.InputFieldColumn, 6);
			reportService.SetIndividualWidth((int)ReportColumns.ValueColumn, 3);
			reportService.SetIndividualWidth((int)ReportColumns.OutputFieldColumn, 6);
			reportService.SetIndividualWidth((int)ReportColumns.ErrorColumn, 5);
			long reportId = reportService.SaveReport("Отчет проставления оценочной группы");

			return reportId;
		}

		#region Help Methods

		private static void AddValueFactor(OMMainObject mObject, long? idFactor, long? idDoc, DateTime date, string value)
		{
			var attributeValue = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = idFactor.Value,
				ObjectId = mObject.Id,
				ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
				S = date.Date,
				ChangeUserId = SRDSession.Current.UserID,
				ChangeDate = DateTime.Now,
				Ot = date.Date.Date,
				StringValue = value,
			};
			var id = attributeValue.Save();
			if (id != 0)
			{
				lock(locked)
				{
					SuccessCount++;
				}
            }
        }

		private static ValueItem GetValueFactor(OMMainObject obj, long idRegister, long idFactor)
		{
			ValueItem res = new ValueItem
			{
				Value = string.Empty,
				IdDocument = null,
			};

			List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(obj.Id, new List<long> { idRegister }, new List<long> { idFactor }, DateTime.Now.Date);
			if (attribs.Count > 0)
			{
				if (attribs[0].GetValueInString() != string.Empty && attribs[0].GetValueInString() != null)
				{
					res.Value = attribs[0].StringValue;
					res.IdDocument = attribs[0].ChangeDocId;
				}
			}

			return res;
		}

		private static List<ComplianceGuid> GetComplianceGuides(List<OMComplianceGuide> complianceGuides)
		{
			var res = new List<ComplianceGuid>();

			foreach (var complianceGuide in complianceGuides)
			{
				long.TryParse(complianceGuide.SubGroup?.Split('.')[1], out var sGroup);
				if(complianceGuide.SubGroup != null)
					res.Add(new ComplianceGuid { Group = complianceGuide.SubGroup, Code = complianceGuide.Code, TypeRoom = complianceGuide.TypeRoom, SubGroup = sGroup });
			}

			return res;
		}

		public static void AddErrorRow(string value, int rowNumber, GbuReportService reportService)
		{
			reportService.AddValue(value, (int)ReportColumns.ErrorColumn, rowNumber);
		}

		public static void AddRowToReport(int rowNumber, string kn, long inputAttributeId, long sourceAttributeId, string value, GbuReportService reportService)
		{
			var inputAttributeName = GbuObjectService.GetAttributeNameById(inputAttributeId);
			var sourceAttributeName = GbuObjectService.GetAttributeNameById(sourceAttributeId);
			reportService.AddValue(kn, (int)ReportColumns.KnColumn, rowNumber);
			reportService.AddValue(inputAttributeName, (int)ReportColumns.InputFieldColumn, rowNumber);
			reportService.AddValue(value, (int)ReportColumns.ValueColumn, rowNumber);
			reportService.AddValue(sourceAttributeName, (int)ReportColumns.OutputFieldColumn, rowNumber);
			reportService.AddValue(string.Empty, (int)ReportColumns.ErrorColumn, rowNumber);
		}

		#endregion
    }
}