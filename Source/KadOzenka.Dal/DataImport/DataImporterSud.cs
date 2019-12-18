using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
using GemBox.Spreadsheet;
using ObjectModel.Commission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;
using ObjectModel.Common;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.LongProcess;
using System.Linq;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterSud : ILongProcess
	{
		public const string LongProcessName = "DataImporterSud";

		class ErrorRow
		{
			public int rowIndex = 0;
			public bool allerror = false;
			public List<int> colIndex;
			public ErrorRow(int index)
			{
				rowIndex = index;
				colIndex = new List<int>();
			}
		}
		
		public static void AddImportToQueue(long mainRegisterId, string registerViewId, string templateFileName, Stream templateFile)
		{
			var export = new OMImportDataLog
			{
				UserId = SRDSession.GetCurrentUserId().Value,
				DateCreated = DateTime.Now,
				Status_Code = ObjectModel.Directory.Common.ImportStatus.Added, // TODO: доработать платформу, чтоб формировался Enum
				DataFileName = templateFileName,
				MainRegisterId = mainRegisterId,
				RegisterViewId = registerViewId
			};
			export.Save();

			FileStorageManager.Save(templateFile, DataImporterCommon.FileStorageName, export.DateCreated, DataImporterCommon.GetTemplateName(export.Id));

			LongProcessManager.AddTaskToQueue(LongProcessName, OMExportByTemplates.GetRegisterId(), export.Id);
		}

		public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
		{
			if (!processQueue.ObjectId.HasValue)
			{
				return;
			}

			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == processQueue.ObjectId)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Running;
			import.DateStarted = DateTime.Now;
			import.Save();

			// Запустить формирование файла
			var templateFile = FileStorageManager.GetFileStream(DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetTemplateName(import.Id));

			ExcelFile excelTemplate = ExcelFile.Load(templateFile, LoadOptions.XlsxDefault);
			
			Stream resultFile = ImportDataSudFromExcel(excelTemplate);

			// Сохранение файла
			FileStorageManager.Save(resultFile, DataImporterCommon.FileStorageName, import.DateCreated, DataImporterCommon.GetResultFileName(import.Id));

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Completed;
			import.DateFinished = DateTime.Now;
			import.Save();

			// Отправка уведомления о завершении загрузки
			DataImporterCommon.SendResultNotification(import);
		}

		public void LogError(long? objectId, Exception ex, long? errorId = null)
		{
			OMImportDataLog import = OMImportDataLog
				.Where(x => x.Id == objectId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (import == null)
			{
				return;
			}

			import.Status_Code = ObjectModel.Directory.Common.ImportStatus.Faulted;
			import.DateFinished = DateTime.Now;
			import.ResultMessage = $"{ex.Message}{(errorId != null ? $" (журнал № {errorId})" : String.Empty)}";
			import.Save();
		}

		public bool Test()
		{
			return true;
		}
		
		public static Stream ImportDataSudFromExcel(ExcelFile excelFile)
		{	
			List<ErrorRow> errorrowMain = new List<ErrorRow>();
			List<ErrorRow> errorrowOtcher = new List<ErrorRow>();
			List<ErrorRow> errorrowSud = new List<ErrorRow>();
			List<ErrorRow> errorrowZak = new List<ErrorRow>();
			int counterr = 0;


			var mainWorkSheet = excelFile.Worksheets[0];

			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
			ParallelOptions options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 10
			};

			int maxColumns = mainWorkSheet.CalculateMaxUsedColumns();

			Parallel.ForEach(mainWorkSheet.Rows, options, row =>
			{
				bool ReadyImport = false;
				ErrorRow errorMain = new ErrorRow(row.Index);
				ErrorRow errorOtchet = new ErrorRow(row.Index);
				ErrorRow errorSud = new ErrorRow(row.Index);
				ErrorRow errorZak = new ErrorRow(row.Index);
				string errortext = string.Empty;
				try
				{
					if (row.Index != 0) //все, кроме заголовков
					{

						string cKn = mainWorkSheet.Rows[row.Index].Cells[0].Value.ParseToString();
						DateTime? cDate = mainWorkSheet.Rows[row.Index].Cells[4].Value.ParseToDateTimeNullable();
						ObjectModel.Sud.OMObject sud_object = ObjectModel.Sud.OMObject.Where(x => x.Kn == cKn && x.Date == cDate).SelectAll().ExecuteFirstOrDefault();


						decimal? cSq = mainWorkSheet.Rows[row.Index].Cells[3].Value.ParseToDecimalNullable();
						decimal? cKC = mainWorkSheet.Rows[row.Index].Cells[5].Value.ParseToDecimalNullable();
						string cName_Center = mainWorkSheet.Rows[row.Index].Cells[6].Value.ParseToString();
						string cStat_Dgi = mainWorkSheet.Rows[row.Index].Cells[7].Value.ParseToString();
						string cOwner = mainWorkSheet.Rows[row.Index].Cells[8].Value.ParseToString();
						string cAdres = mainWorkSheet.Rows[row.Index].Cells[2].Value.ParseToString();
						ObjectModel.Directory.Sud.SudObjectType cType_code = ObjectModel.Directory.Sud.SudObjectType.None;
						string cType = mainWorkSheet.Rows[row.Index].Cells[1].Value.ParseToString();
						if (cType.ToUpper() == "ЗДАНИЕ") cType_code = ObjectModel.Directory.Sud.SudObjectType.Building;
						if (cType.ToUpper() == "ПОМЕЩЕНИЕ") cType_code = ObjectModel.Directory.Sud.SudObjectType.Room;
						if (cType.ToUpper() == "УЧАСТОК") cType_code = ObjectModel.Directory.Sud.SudObjectType.Site;
						if (cType.ToUpper() == "СООРУЖЕНИЕ") cType_code = ObjectModel.Directory.Sud.SudObjectType.Construction;
						if (cType.ToUpper() == "ОНС") cType_code = ObjectModel.Directory.Sud.SudObjectType.Ons;
						if (cType.ToUpper() == "МАШИНОМЕСТО") cType_code = ObjectModel.Directory.Sud.SudObjectType.ParkingPlace;
						bool newobj = false;

						if (sud_object == null)
						{
							if (cKn != null && cDate != null && cSq != null && cKC != null && cType_code != ObjectModel.Directory.Sud.SudObjectType.None)
							{
								sud_object = new ObjectModel.Sud.OMObject
								{
									Kn = cKn,
									Date = cDate,
									Square = cSq,
									Kc = cKC,
									NameCenter = cName_Center,
									StatDgi = cStat_Dgi,
									Owner = cOwner,
									Adres = cAdres,
									Typeobj_Code = cType_code,
									Workstat_Code = ObjectModel.Directory.Sud.ProcessingStatus.Processed
								};
								newobj = true;
								sud_object.SaveAndCheckParam();
								ReadyImport = true;
							}
							else
							{
								ReadyImport = false;
								errortext = "Отсутствует тип объекта/площадь/оспариваемая стоимость";
								errorMain.allerror = true;
								if (cSq == null)
								{
									errorMain.colIndex.Add(3);
								}
								if (cKC == null)
								{
									errorMain.colIndex.Add(5);
								}
							}

						}
						else
						{
							cSq = (cSq == null) ? sud_object.Square : cSq;
							cKC = (cKC == null) ? sud_object.Kc : cKC;
							cName_Center = (cName_Center == string.Empty) ? sud_object.NameCenter : cName_Center;
							cStat_Dgi = (cStat_Dgi == string.Empty) ? sud_object.StatDgi : cStat_Dgi;
							cOwner = (cOwner == string.Empty) ? sud_object.Owner : cOwner;
							cAdres = (cAdres == string.Empty) ? sud_object.Adres : cAdres;
							cType_code = (cType_code == ObjectModel.Directory.Sud.SudObjectType.None) ? sud_object.Typeobj_Code : cType_code;
							ReadyImport = true;

							if (cType_code != sud_object.Typeobj_Code && cType_code != ObjectModel.Directory.Sud.SudObjectType.None)
							{
								errorMain.colIndex.Add(1);
							}
							if (cAdres.ToUpper() != sud_object.Adres.ToUpper() && cAdres != string.Empty)
							{
								errorMain.colIndex.Add(2);
							}
							if (cSq != null && cSq != sud_object.Square)
							{
								errorMain.colIndex.Add(3);
							}
							if (cKC != null && cKC != sud_object.Kc)
							{
								errorMain.colIndex.Add(5);
							}
							if (cName_Center.IsNotEmpty() && cName_Center.ToUpper() != sud_object.NameCenter.ToUpper())
							{
								errorMain.colIndex.Add(6);
							}
							if (cStat_Dgi.IsNotEmpty() && cStat_Dgi.ToUpper() != sud_object.StatDgi.ToUpper())
							{
								errorMain.colIndex.Add(7);
							}
							if (cOwner.IsNotEmpty() && cOwner.ToUpper() != sud_object.Owner.ToUpper())
							{
								errorMain.colIndex.Add(8);
							}

							sud_object.Kn = cKn;
							sud_object.Date = cDate;
							sud_object.Square = cSq;
							sud_object.Kc = cKC;
							sud_object.NameCenter = cName_Center;
							sud_object.StatDgi = cStat_Dgi;
							sud_object.Owner = cOwner;
							sud_object.Adres = cAdres;
							sud_object.Typeobj_Code = cType_code;

							sud_object.SaveAndCheckParam();
						}

						if (ReadyImport)
						{
							#region Отчеты
							string oNumber = mainWorkSheet.Rows[row.Index].Cells[9].Value.ParseToString();
							DateTime? oDate = mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToDateTimeNullable();
							DateTime? oDateIn = mainWorkSheet.Rows[row.Index].Cells[17].Value.ParseToDateTimeNullable();
							string oOrgName = mainWorkSheet.Rows[row.Index].Cells[12].Value.ParseToString();
							string oFioName = mainWorkSheet.Rows[row.Index].Cells[13].Value.ParseToString();
							string oSroName = mainWorkSheet.Rows[row.Index].Cells[14].Value.ParseToString();
							string oJalob = mainWorkSheet.Rows[row.Index].Cells[18].Value.ParseToString();

							ObjectModel.Sud.OMOtchet sud_otchet = ObjectModel.Sud.OMOtchet.Where(x => x.Number == oNumber).SelectAll().ExecuteFirstOrDefault();
							ObjectModel.Sud.OMDict oOrg = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == oOrgName.ToUpper() && x.Type == 1).SelectAll().ExecuteFirstOrDefault();
							if (oOrg == null && oOrgName != string.Empty)
							{
								oOrg = new ObjectModel.Sud.OMDict
								{
									Id = -1,
									Name = oOrgName,
									Type = 1,
									IdParent = -1,
								};
								oOrg.Save();
							}
							ObjectModel.Sud.OMDict oFio = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == oFioName.ToUpper() && x.Type == 3).SelectAll().ExecuteFirstOrDefault();
							if (oFio == null && oFioName != string.Empty)
							{
								oFio = new ObjectModel.Sud.OMDict
								{
									Id = -1,
									Name = oFioName,
									Type = 3,
									IdParent = -1,
								};
								oFio.Save();
							}
							ObjectModel.Sud.OMDict oSro = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == oSroName.ToUpper() && x.Type == 2).SelectAll().ExecuteFirstOrDefault();
							if (oSro == null && oSroName != string.Empty)
							{
								oSro = new ObjectModel.Sud.OMDict
								{
									Id = -1,
									Name = oSroName,
									Type = 2,
									IdParent = -1,
								};
								oSro.Save();
							}


							bool newotch = false;

							if (sud_otchet == null)
							{
								if (oNumber != string.Empty)
								{
									sud_otchet = new ObjectModel.Sud.OMOtchet
									{
										Id = -1,
										Number = oNumber,
										Date = oDate,
										DateIn = oDateIn,
										Jalob = (oJalob.ToUpper() == "ДА") ? 1 : 0,
										Fio = (oFio == null) ? string.Empty : oFio.Name,
										IdFio = (oFio == null) ? (long?)null : oFio.Id,
										Org = (oOrg == null) ? string.Empty : oOrg.Name,
										IdOrg = (oOrg == null) ? (long?)null : oOrg.Id,
										Sro = (oSro == null) ? string.Empty : oSro.Name,
										IdSro = (oSro == null) ? (long?)null : oSro.Id,
									};
									sud_otchet.SaveAndCheckParam();
									newotch = true;
								}
							}
							if (sud_otchet != null)
							{
								if (!newotch)
								{

									oDate = (oDate == null) ? sud_otchet.Date : oDate;
									oDateIn = (oDateIn == null) ? sud_otchet.DateIn : oDateIn;
									long? lJalob = (oJalob == string.Empty) ? sud_otchet.Jalob : ((oJalob.ToUpper() == "ДА") ? 1 : 0);

									long? oSroId = (oSro == null) ? sud_otchet.IdSro : oSro.Id;
									oSroName = (oSro == null) ? sud_otchet.Sro : oSro.Name;

									long? oFioId = (oFio == null) ? sud_otchet.IdFio : oFio.Id;
									oFioName = (oFio == null) ? sud_otchet.Fio : oFio.Name;

									long? oOrgId = (oOrg == null) ? sud_otchet.IdOrg : oOrg.Id;
									oOrgName = (oOrg == null) ? sud_otchet.Org : oOrg.Name;

									if ((sud_otchet.Date != oDate) && (oDate != null))
									{
										errorOtchet.colIndex.Add(10);
									}
									if (sud_otchet.DateIn != oDateIn && oDateIn != null)
									{
										errorOtchet.colIndex.Add(17);
									}
									if (sud_otchet.Jalob != ((oJalob.ToUpper() == "ДА") ? 1 : 0) && oJalob.ToUpper() != string.Empty)
									{
										errorOtchet.colIndex.Add(18);
									}
									if (sud_otchet.Org.ToUpper() != oOrgName.ToUpper() && oOrgName != string.Empty)
									{
										errorOtchet.colIndex.Add(12);
									}
									if (sud_otchet.Sro.ToUpper() != oSroName.ToUpper() && oSroName != string.Empty)
									{
										errorOtchet.colIndex.Add(14);
									}
									if (sud_otchet.Fio.ToUpper() != oFioName.ToUpper() && oFioName != string.Empty)
									{
										errorOtchet.colIndex.Add(13);
									}

									sud_otchet.Number = oNumber;
									sud_otchet.Date = oDate;
									sud_otchet.DateIn = oDateIn;
									sud_otchet.Jalob = lJalob;
									sud_otchet.Fio = oFioName;
									sud_otchet.IdFio = oFioId;
									sud_otchet.Org = oOrgName;
									sud_otchet.IdOrg = oOrgId;
									sud_otchet.Sro = oSroName;
									sud_otchet.IdSro = oSroId;

									sud_otchet.SaveAndCheckParam();
								}


								{
									decimal? cRc = mainWorkSheet.Rows[row.Index].Cells[15].Value.ParseToDecimalNullable();
									decimal? cUc = mainWorkSheet.Rows[row.Index].Cells[16].Value.ParseToDecimalNullable();
									string cUse = mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString();

									if (cRc != null || cUc != null)
									{
										ObjectModel.Sud.OMOtchetLink sud_otchet_link = ObjectModel.Sud.OMOtchetLink.Where(x => x.IdObject == sud_object.Id && x.IdOtchet == sud_otchet.Id).SelectAll().ExecuteFirstOrDefault();
										if (sud_otchet_link == null)
										{
											sud_otchet_link = new ObjectModel.Sud.OMOtchetLink
											{
												Id = -1,
												IdObject = sud_object.Id,
												IdOtchet = sud_otchet.Id,
												Use = cUse,
												Rs = cRc,
												Uprs = cUc,
											};
										}
										else
										{

											cUse = (cUse == string.Empty) ? sud_otchet_link.Use : cUse;
											cRc = (cRc == null) ? sud_otchet_link.Rs : cRc;
											cUc = (cUc == null) ? sud_otchet_link.Uprs : cUc;



											if ((sud_otchet_link.Use.ToUpper() != cUse.ToUpper()) && (cUse != string.Empty))
											{
												errorOtchet.colIndex.Add(11);
											}
											if ((sud_otchet_link.Rs != cRc) && (cRc != null))
											{
												errorOtchet.colIndex.Add(15);
											}
											if ((sud_otchet_link.Uprs != cUc) && (cUc != null))
											{
												errorOtchet.colIndex.Add(16);
											}

											sud_otchet_link.Use = cUse;
											sud_otchet_link.Rs = cRc;
											sud_otchet_link.Uprs = cUc;

											sud_otchet_link.SaveAndCheckParam();
										}
									}
									else
									{
										errorOtchet.colIndex.Add(15);
										errorOtchet.colIndex.Add(16);
									}
								}
							}
							else
							{
								if (oNumber == string.Empty && (mainWorkSheet.Rows[row.Index].Cells[10].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[11].Value.ParseToString() != string.Empty || oOrgName != string.Empty || oFioName != string.Empty || oSroName != string.Empty || mainWorkSheet.Rows[row.Index].Cells[15].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[16].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[17].Value.ParseToString() != string.Empty || oJalob != string.Empty))
								{
									errorOtchet.colIndex.Add(9);
								}
							}
							#endregion

							#region Судебные решения
							string sName = mainWorkSheet.Rows[row.Index].Cells[19].Value.ParseToString();
							string sStatus = mainWorkSheet.Rows[row.Index].Cells[20].Value.ParseToString();
							string sNumber = mainWorkSheet.Rows[row.Index].Cells[21].Value.ParseToString();
							DateTime? sDate = mainWorkSheet.Rows[row.Index].Cells[22].Value.ParseToDateTimeNullable();
							DateTime? sDateAct = mainWorkSheet.Rows[row.Index].Cells[23].Value.ParseToDateTimeNullable();
							long? lStatus = -1;
							switch (sStatus.ToLower())
							{
								case "без статуса":
									lStatus = 0;
									break;
								case "удовлетворено":
									lStatus = 1;
									break;
								case "отказано":
									lStatus = 2;
									break;
								case "приостановлено":
									lStatus = 3;
									break;
								case "частично удовлетворено":
									lStatus = 4;
									break;
							}

							ObjectModel.Sud.OMSud sud_sud = ObjectModel.Sud.OMSud.Where(x => x.Number == sNumber).SelectAll().ExecuteFirstOrDefault();

							bool newsud = false;
							if (sud_sud == null)
							{
								if (sNumber != string.Empty)
								{
									sud_sud = new ObjectModel.Sud.OMSud
									{
										Id = -1,
										Date = sDate,
										Name = sName,
										Number = sNumber,
										Status = (lStatus == -1) ? 0 : lStatus,
										SudDate = sDateAct,
									};
									sud_sud.SaveAndCheckParam();
									newsud = true;
								}
							}
							if (sud_sud != null)
							{
								if (!newsud)
								{
									sName = (sName == string.Empty) ? sud_sud.Name : sName;
									sNumber = (sNumber == string.Empty) ? sud_sud.Number : sNumber;
									sDate = (sDate == null) ? sud_sud.Date : sDate;
									sDateAct = (sDateAct == null) ? sud_sud.SudDate : sDateAct;
									lStatus = (lStatus == -1) ? sud_sud.Status : lStatus;

									if ((sud_sud.Date != sDate) && (sDate != null))
									{
										errorSud.colIndex.Add(22);
									}
									if ((sud_sud.SudDate != sDateAct) && (sDateAct != null))
									{
										errorSud.colIndex.Add(23);
									}
									if (sud_sud.Name.ToUpper() != sName.ToUpper() && (sName != string.Empty))
									{
										errorSud.colIndex.Add(19);
									}
									if ((sud_sud.Status != lStatus) && (lStatus != -1))
									{
										errorSud.colIndex.Add(20);
									}

									sud_sud.Date = sDate;
									sud_sud.Name = sName;
									sud_sud.Number = sNumber;
									sud_sud.Status = lStatus;
									sud_sud.SudDate = sDateAct;
									sud_sud.SaveAndCheckParam();
								}

								{
									decimal? sRc = mainWorkSheet.Rows[row.Index].Cells[24].Value.ParseToDecimalNullable();
									string sUse = mainWorkSheet.Rows[row.Index].Cells[25].Value.ParseToString();
									string sDesc = mainWorkSheet.Rows[row.Index].Cells[26].Value.ParseToString();

									ObjectModel.Sud.OMSudLink sud_sud_link = ObjectModel.Sud.OMSudLink.Where(x => x.IdObject == sud_object.Id && x.IdSud == sud_sud.Id).SelectAll().ExecuteFirstOrDefault();
									if (sud_sud_link == null)
									{
										sud_sud_link = new ObjectModel.Sud.OMSudLink
										{
											Id = -1,
											Descr = sDesc,
											IdObject = sud_object.Id,
											IdSud = sud_sud.Id,
											Rs = sRc,
											Use = sUse,
										};
										sud_sud_link.SaveAndCheckParam();
									}
									else
									{
										sUse = (sUse == string.Empty) ? sud_sud_link.Use : sUse;
										sDesc = (sDesc == string.Empty) ? sud_sud_link.Descr : sDesc;
										sRc = (sRc == null) ? sud_sud_link.Rs : sRc;


										if ((sud_sud_link.Use.ToUpper() != sUse.ToUpper()) && (sUse != string.Empty))
										{
											errorSud.colIndex.Add(25);
										}
										if ((sud_sud_link.Rs != sRc) && (sRc != null))
										{
											errorSud.colIndex.Add(24);
										}

										sud_sud_link.Use = sUse;
										sud_sud_link.Rs = sRc;
										sud_sud_link.Descr = sDesc;
										sud_sud_link.SaveAndCheckParam();
									}
								}
							}
							else
							{
								if (mainWorkSheet.Rows[row.Index].Cells[19].Value.ParseToString() == string.Empty && (mainWorkSheet.Rows[row.Index].Cells[20].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[21].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[22].Value.ParseToString() != null || mainWorkSheet.Rows[row.Index].Cells[23].Value.ParseToString() != null || mainWorkSheet.Rows[row.Index].Cells[24].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[25].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[26].Value.ParseToString() != string.Empty))
								{
									errorSud.colIndex.Add(21);
								}
							}
							#endregion

							#region Заключения
							string zNumber = mainWorkSheet.Rows[row.Index].Cells[27].Value.ParseToString();
							DateTime? zDate = mainWorkSheet.Rows[row.Index].Cells[28].Value.ParseToDateTimeNullable();
							DateTime? zRecDate = mainWorkSheet.Rows[row.Index].Cells[37].Value.ParseToDateTimeNullable();
							string zOrgName = mainWorkSheet.Rows[row.Index].Cells[30].Value.ParseToString();
							string zFioName = mainWorkSheet.Rows[row.Index].Cells[31].Value.ParseToString();
							string zSroName = mainWorkSheet.Rows[row.Index].Cells[32].Value.ParseToString();
							string zRecUser = mainWorkSheet.Rows[row.Index].Cells[38].Value.ParseToString();
							string zRecLetter = mainWorkSheet.Rows[row.Index].Cells[39].Value.ParseToString();
							string zBefore = mainWorkSheet.Rows[row.Index].Cells[35].Value.ParseToString();
							string zAfter = mainWorkSheet.Rows[row.Index].Cells[36].Value.ParseToString();
							string zSoglas = mainWorkSheet.Rows[row.Index].Cells[41].Value.ParseToString();

							ObjectModel.Sud.OMZak sud_zak = ObjectModel.Sud.OMZak.Where(x => x.Number == zNumber).SelectAll().ExecuteFirstOrDefault();
							ObjectModel.Sud.OMDict zOrg = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == zOrgName.ToUpper() && x.Type == 1).SelectAll().ExecuteFirstOrDefault();
							if (zOrg == null && zOrgName != string.Empty)
							{
								zOrg = new ObjectModel.Sud.OMDict
								{
									Id = -1,
									Name = zOrgName,
									Type = 1,
									IdParent = -1,
								};
								zOrg.Save();
							}
							ObjectModel.Sud.OMDict zFio = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == zFioName.ToUpper() && x.Type == 3).SelectAll().ExecuteFirstOrDefault();
							if (zFio == null && zFioName != string.Empty)
							{
								zFio = new ObjectModel.Sud.OMDict
								{
									Id = -1,
									Name = zFioName,
									Type = 3,
									IdParent = -1,
								};
								zFio.Save();
							}
							ObjectModel.Sud.OMDict zSro = ObjectModel.Sud.OMDict.Where(x => x.Name.ToUpper() == zSroName.ToUpper() && x.Type == 2).SelectAll().ExecuteFirstOrDefault();
							if (zSro == null && zSroName != string.Empty)
							{
								zSro = new ObjectModel.Sud.OMDict
								{
									Id = -1,
									Name = zSroName,
									Type = 2,
									IdParent = -1,
								};
								zSro.Save();
							}
							bool newzak = false;

							if (sud_zak == null)
							{
								if (zNumber != string.Empty)
								{
									sud_zak = new ObjectModel.Sud.OMZak
									{
										Id = -1,
										Number = zNumber,
										Date = zDate,
										Fio = (oFio == null) ? string.Empty : oFio.Name,
										IdFio = (oFio == null) ? (long?)null : oFio.Id,
										Org = (oOrg == null) ? string.Empty : oOrg.Name,
										IdOrg = (oOrg == null) ? (long?)null : oOrg.Id,
										Sro = (oSro == null) ? string.Empty : oSro.Name,
										IdSro = (oSro == null) ? (long?)null : oSro.Id,
										RecAfter = (zAfter.ToUpper() == "ДА") ? 1 : 0,
										RecBefore = (zBefore.ToUpper() == "ДА") ? 1 : 0,
										RecSoglas = (zSoglas.ToUpper() == "ДА") ? 1 : 0,
										RecDate = zRecDate,
										RecUser = zRecUser,
										RecLetter = zRecLetter,
									};
									sud_zak.SaveAndCheckParam();
									newzak = true;
								}
							}
							if (sud_zak != null)
							{
								if (!newzak)
								{
									zDate = (zDate == null) ? sud_zak.Date : zDate;
									zRecDate = (zRecDate == null) ? sud_zak.RecDate : zRecDate;
									zRecUser = (zRecUser == string.Empty) ? sud_zak.RecUser : zRecUser;
									zRecLetter = (zRecLetter == string.Empty) ? sud_zak.RecLetter : zRecLetter;

									long? lBefore = (zBefore == string.Empty) ? sud_zak.RecBefore : ((zBefore.ToUpper() == "ДА") ? 1 : 0);
									long? lAfter = (zAfter == string.Empty) ? sud_zak.RecAfter : ((zAfter.ToUpper() == "ДА") ? 1 : 0);
									long? lSoglas = (zSoglas == string.Empty) ? sud_zak.RecSoglas : ((zSoglas.ToUpper() == "ДА") ? 1 : 0);

									long? zSroId = (zSro == null) ? sud_zak.IdSro : zSro.Id;
									zSroName = (zSro == null) ? sud_zak.Sro : zSro.Name;

									long? zFioId = (zFio == null) ? sud_zak.IdFio : zFio.Id;
									zFioName = (zFio == null) ? sud_zak.Fio : zFio.Name;

									long? zOrgId = (zOrg == null) ? sud_zak.IdOrg : zOrg.Id;
									zOrgName = (zOrg == null) ? sud_zak.Org : zOrg.Name;




									if ((sud_zak.Date != zDate) && (zDate != null))
									{
										errorZak.colIndex.Add(28);
									}
									if ((sud_zak.RecDate != zRecDate) && (zRecDate != null))
									{
										errorZak.colIndex.Add(37);
									}
									if ((sud_zak.RecBefore != ((zBefore.ToUpper() == "ДА") ? 1 : 0)) && (zBefore != string.Empty))
									{
										errorZak.colIndex.Add(35);
									}
									if ((sud_zak.RecAfter != ((zAfter.ToUpper() == "ДА") ? 1 : 0)) && (zAfter != string.Empty))
									{
										errorZak.colIndex.Add(36);
									}
									if ((sud_zak.RecSoglas != ((zSoglas.ToUpper() == "ДА") ? 1 : 0)) && (zSoglas != string.Empty))
									{
										errorZak.colIndex.Add(41);
									}
									if ((sud_zak.RecUser.ToUpper() != zRecLetter.ToUpper()) && (zRecLetter != string.Empty))
									{
										errorZak.colIndex.Add(38);
									}
									if ((sud_zak.Org.ToUpper() != zOrgName.ToUpper()) && (zOrgName != string.Empty))
									{
										errorZak.colIndex.Add(30);
									}
									if ((sud_zak.Sro.ToUpper() != zSroName.ToUpper()) && (zSroName != string.Empty))
									{
										errorZak.colIndex.Add(32);
									}
									if ((sud_zak.Fio.ToUpper() != zFioName.ToUpper()) && (zFioName != string.Empty))
									{
										errorZak.colIndex.Add(31);
									}

									sud_zak.Date = zDate;
									sud_zak.RecDate = zRecDate;
									sud_zak.RecUser = zRecUser;
									sud_zak.RecLetter = zRecLetter;
									sud_zak.RecBefore = lBefore;
									sud_zak.RecAfter = lAfter;
									sud_zak.RecSoglas = lSoglas;
									sud_zak.IdSro = zSroId;
									sud_zak.Sro = zSroName;
									sud_zak.IdFio = zFioId;
									sud_zak.Fio = zFioName;
									sud_zak.IdOrg = zOrgId;
									sud_zak.Org = zOrgName;
									sud_zak.SaveAndCheckParam();
								}
								{
									decimal? zRc = mainWorkSheet.Rows[row.Index].Cells[33].Value.ParseToDecimalNullable();
									decimal? zUc = mainWorkSheet.Rows[row.Index].Cells[34].Value.ParseToDecimalNullable();
									string zUse = mainWorkSheet.Rows[row.Index].Cells[29].Value.ParseToString();
									string zDesc = mainWorkSheet.Rows[row.Index].Cells[40].Value.ParseToString();

									if (zRc > 0 || zUc > 0)
									{
										ObjectModel.Sud.OMZakLink sud_zak_link = ObjectModel.Sud.OMZakLink.Where(x => x.IdObject == sud_object.Id && x.IdZak == sud_zak.Id).SelectAll().ExecuteFirstOrDefault();
										if (sud_zak_link == null)
										{
											sud_zak_link = new ObjectModel.Sud.OMZakLink
											{
												Id = -1,
												Descr = zDesc,
												IdObject = sud_object.Id,
												IdZak = sud_zak.Id,
												Rs = zRc,
												Uprs = zUc,
												Use = zUse,
											};
											sud_zak_link.SaveAndCheckParam();
										}
										else
										{
											zUse = (zUse == string.Empty) ? sud_zak_link.Use : zUse;
											zDesc = (zDesc == string.Empty) ? sud_zak_link.Descr : zDesc;
											zRc = (zRc == null) ? sud_zak_link.Rs : zRc;
											zUc = (zUc == null) ? sud_zak_link.Uprs : zUc;


											if ((sud_zak_link.Use.ToUpper() != zUse.ToUpper()) && (zUse != string.Empty))
											{
												errorZak.colIndex.Add(29);
											}
											if ((sud_zak_link.Rs != zRc) && (zRc != null))
											{
												errorZak.colIndex.Add(33);
											}
											if ((sud_zak_link.Uprs != zUc) && (zUc != null))
											{
												errorZak.colIndex.Add(34);
											}

											sud_zak_link.Use = zUse;
											sud_zak_link.Descr = zDesc;
											sud_zak_link.Rs = zRc;
											sud_zak_link.Uprs = zUc;
											sud_zak_link.SaveAndCheckParam();
										}
									}
									else
									{
										errorZak.colIndex.Add(33);
										errorZak.colIndex.Add(34);
									}
								}
							}
							else
							{
								if (mainWorkSheet.Rows[row.Index].Cells[27].Value.ParseToString() == string.Empty && (mainWorkSheet.Rows[row.Index].Cells[28].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[29].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[30].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[31].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[32].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[33].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[34].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[35].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[36].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[37].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[38].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[39].Value.ParseToString() != string.Empty || mainWorkSheet.Rows[row.Index].Cells[40].Value.ParseToString() != string.Empty ||
								mainWorkSheet.Rows[row.Index].Cells[41].Value.ParseToString() != string.Empty))
								{
									errorZak.colIndex.Add(27);
								}
							}
							#endregion

							if (newobj)
							{
								try
								{
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Новый объект");
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.LightGreen));
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
								}
								catch
								{

								}
							}
							else
							{
								try
								{
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Обновлено");
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Yellow));
									mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
								}
								catch
								{

								}
							}

						}
						else
						{
							try
							{
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue("Ошибка");
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
								mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
							}
							catch
							{

							}
						}

					}
				}
				catch (Exception ex)
				{
					long errorId = ErrorManager.LogError(ex);
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].SetValue($"{ex.Message} (подробно в журнале №{errorId})");
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.FillPattern.SetSolid(SpreadsheetColor.FromName(ColorName.Red));
					mainWorkSheet.Rows[row.Index].Cells[maxColumns].Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);
					counterr++;
					errorMain.allerror = true;
				}

				if (errorMain.allerror || errorMain.colIndex.Count > 0)
					errorrowMain.Add(errorMain);
				if (errorOtchet.colIndex.Count > 0)
					errorrowOtcher.Add(errorOtchet);
				if (errorSud.colIndex.Count > 0)
					errorrowSud.Add(errorSud);
				if (errorZak.colIndex.Count > 0)
					errorrowZak.Add(errorZak);
			});

			foreach (ErrorRow ind in errorrowMain)
			{
				if (ind.rowIndex > 0)
				{
					for (int i = 0; i < 42; i++)
						mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 200, 200));
				}
				foreach (int indX in ind.colIndex)
				{
					mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
				}
			}
			foreach (ErrorRow ind in errorrowOtcher)
			{
				if (ind.rowIndex > 0)
				{
					for (int i = 9; i < 19; i++)
						mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 200, 255));
				}
				foreach (int indX in ind.colIndex)
				{
					mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
				}
			}
			foreach (ErrorRow ind in errorrowSud)
			{
				if (ind.rowIndex > 0)
				{
					for (int i = 19; i < 27; i++)
						mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 255));
				}
				foreach (int indX in ind.colIndex)
				{
					mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
				}
			}
			foreach (ErrorRow ind in errorrowZak)
			{
				if (ind.rowIndex > 0)
				{
					for (int i = 27; i < 42; i++)
						mainWorkSheet.Rows[ind.rowIndex].Cells[i].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(255, 255, 200));
				}
				foreach (int indX in ind.colIndex)
				{
					mainWorkSheet.Rows[ind.rowIndex].Cells[indX].Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(200, 255, 200));
				}
			}
			
			MemoryStream streamResult = new MemoryStream();
			excelFile.Save(streamResult, SaveOptions.XlsxDefault);
			streamResult.Seek(0, SeekOrigin.Begin);
			
			return streamResult;
		}
	}
}
