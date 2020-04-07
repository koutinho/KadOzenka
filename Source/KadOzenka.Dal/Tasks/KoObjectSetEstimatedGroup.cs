using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using ObjectModel.Core.Register;
using ObjectModel.Gbu;
using ObjectModel.KO;

namespace KadOzenka.Dal.KoObject
{
	#region TypeStructure
	public struct ValueItem
	{
		public string Value { get; set; }
		public long? IdDocument { get; set; }
	}

	public struct ComplianceGuid
	{
		public string Group { get; set; }
		public string Code { get; set; }
		public string IsResidential { get; set; }
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

		public static List<string> ErrorMessages;

		public static void Run(EstimatedGroupModel param)
		{
			locked = new object();
			var units = OMUnit.Where(x => x.TaskId != null && x.TaskId == param.IdTask).SelectAll().Execute().ToList();
			CountAllUnits = units.Count;
			ErrorMessages = new List<string>();

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 1
			};

			var attributeResult = OMAttribute.Where(x => x.Id == param.IdEstimatedSubGroup).SelectAll()
				.ExecuteFirstOrDefault();

			Parallel.ForEach(units, options, item =>
			{
				var gbuObject = OMMainObject.Where(x => x.Id == item.ObjectId).SelectAll().ExecuteFirstOrDefault();

				var attributeCodeGroup = OMAttribute.Where(x => x.Id == param.IdCodeGroup).SelectAll()
					.ExecuteFirstOrDefault();
				// берем код группы (значение из справочника цод)
				ValueItem codeGroup = GetValueFactor(gbuObject, attributeCodeGroup.RegisterId, attributeCodeGroup.Id);
				if (string.IsNullOrEmpty(codeGroup.Value))
				{
					lock (locked)
					{
						ErrorMessages.Add($"Не найдено значение из справочника ЦОД для объекта {gbuObject.CadastralNumber}");
					}
					return;
				}

				var complianceGuides = GetComplianceGuides(OMComplianceGuide.Where(x => x.Code == codeGroup.Value && x.TypeProperty == item.PropertyType).SelectAll().Execute());

				if (complianceGuides.Count == 1)
				{
					AddValueFactor(gbuObject, attributeResult.Id, codeGroup.IdDocument, DateTime.Now, complianceGuides[0].Group);
					return;
				}

				if (complianceGuides.Count <= 1)
				{
					lock (locked)
					{
						ErrorMessages.Add($"Не найдено значение в таблице сопоставления {gbuObject.CadastralNumber}");
					}
					return;
				}
				{
					if (complianceGuides[0].IsResidential != null)
					{
						var attributeRoom = OMAttribute.Where(x => x.Id == param.IdTypeRoom).SelectAll()
							.ExecuteFirstOrDefault();
						// берем тип помещения
						ValueItem typeRoom = GetValueFactor(gbuObject, attributeRoom.RegisterId, attributeRoom.Id);
						if (string.IsNullOrEmpty(typeRoom.Value))
						{
							lock (locked)
							{
								ErrorMessages.Add($"Не найден тип помещения для объекта {gbuObject.CadastralNumber} ");
							}
							return;
						}
						var group = complianceGuides.FirstOrDefault(x => x.IsResidential == typeRoom.Value);
						AddValueFactor(gbuObject, attributeResult.Id, codeGroup.IdDocument, DateTime.Now, group.Group);
						return;
					}

					var attributeQuarter = OMAttribute.Where(x => x.Id == param.IdCodeQuarter).SelectAll()
						.ExecuteFirstOrDefault();
					// берем кадастровый квартал
					ValueItem codeQuarter = GetValueFactor(gbuObject, attributeQuarter.RegisterId, attributeQuarter.Id);

					if (string.IsNullOrEmpty(codeQuarter.Value))
					{
						lock (locked)
						{
							ErrorMessages.Add($"Не найден кадастровый квартал для объекта {gbuObject.CadastralNumber}.");
						}
						return;
					}

					var kv = OMKadastrKvartal.Where(x => x.KadastrKvartal == codeQuarter.Value).SelectAll().ExecuteFirstOrDefault();
					if (kv == null)
					{
						lock (locked)
						{
							ErrorMessages.Add($"Не найден кадастровый квартал {codeQuarter.Value}. Необходимо обновить справочник");
						}
						return;
					}
					var task = OMTask.Where(x => x.Id == param.IdTask).SelectAll().ExecuteFirstOrDefault();
					var tourYear = OMTour.Where(x => x.Id == task.TourId).SelectAll().ExecuteFirstOrDefault().Year;

					switch (tourYear)
					{
						case 2017:
							AddValueFactor(gbuObject, attributeResult.Id, codeGroup.IdDocument, DateTime.Now, complianceGuides.FirstOrDefault(x => x.SubGroup == kv.TypeTerritory2017).Group); break;
						case 2020:
							AddValueFactor(gbuObject, attributeResult.Id, codeGroup.IdDocument, DateTime.Now, complianceGuides.FirstOrDefault(x => x.SubGroup == kv.TypeTerritory2020).Group); break;
						default:
						{
							lock (locked)
							{
								ErrorMessages.Add("Для выбраного тура не предусмотренны параметры проставления оценки");
							}
							return;
						}
					}
				}
			});
			var strErrors = string.Join(',', ErrorMessages);

			ErrorMessages?.Clear();

			if (CountAllUnits != SuccessCount)
			{
				throw new Exception(strErrors);
			}
		}

		#region HelpMetods

		private static void AddValueFactor(OMMainObject mObject, long? idFactor, long? idDoc, DateTime date, string value)
		{
			var attributeValue = new GbuObjectAttribute
			{
				Id = -1,
				AttributeId = idFactor.Value,
				ObjectId = mObject.Id,
				ChangeDocId = (idDoc == null) ? -1 : idDoc.Value,
				S = date,
				ChangeUserId = SRDSession.Current.UserID,
				ChangeDate = DateTime.Now,
				Ot = date,
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

			List<GbuObjectAttribute> attribs = new GbuObjectService().GetAllAttributes(obj.Id, new List<long> { idRegister }, new List<long> { idFactor }, DateTime.Now);
			if (attribs.Count > 0)
			{
				if (attribs[0].StringValue != string.Empty && attribs[0].StringValue != null)
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

			string parentPrefix = complianceGuides.FirstOrDefault(x => x.ParentId == null)?.SubGroup;

			foreach (var complianceGuide in complianceGuides.Where(x => x.ParentId != null))
			{
				if(complianceGuide.SubGroup != null && int.TryParse(complianceGuide.SubGroup, out var subGroup))
					res.Add(new ComplianceGuid { Group = parentPrefix + '.' + subGroup, Code = complianceGuide.Code, IsResidential = complianceGuide.IsResidential, SubGroup = subGroup });
			}

			return res;
		}

		#endregion



	}
}