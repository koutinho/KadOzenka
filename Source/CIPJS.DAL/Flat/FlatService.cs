using CIPJS.DAL.Building;
using CIPJS.DAL.ImportLogInsurFlat;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Bti;
using ObjectModel.Directory;
using ObjectModel.Ehd;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CIPJS.DAL.Flat
{
    public class FlatService
    {
        /// <summary>
        /// Ссылка на BuildingService.
        /// </summary>
        private readonly ImportLogInsurFlatService _importLogInsurFlatService;

        public FlatService()
        {
            _importLogInsurFlatService = new ImportLogInsurFlatService();
        }
        /// <summary>
        /// Получить помещение по его идентификатору (emp_id).
        /// </summary>
        /// <param name="id">Идентификатор помещения</param>
        /// <returns>Помещение</returns>
        public OMFlat Get(long? id)
        {
            if (!id.HasValue) return null;

            return OMFlat
                .Where(x => x.EmpId == id)
                .SelectAll()
                .Select(x => x.ParentFlatType.Id)
                .Select(x => x.ParentFlatType.Name)
                .Select(x => x.ParentFlatStatus.Id)
                .Select(x => x.ParentFlatStatus.Name)
                .Select(x => x.AccruedSumCurrent)
                .Select(x => x.AccruedOplCurrent)
                .Select(x => x.AccruedSumLast)
                .Select(x => x.AccruedOplLast)
                .Select(x => x.CreditedSumCurrent)
                .Select(x => x.CreditedOplCurrent)
                .Select(x => x.CreditedSumLast)
                .Select(x => x.CreditedOplLast)
                .Select(x => x.ParentBuilding.ParentBuildParcel.Area)
                .Select(x => x.ParentPremase.TotalArea)
                .Select(x => x.ParentPremase.RoomsCount)
                .Select(x => x.ParentPremase.Tres)
                .Select(x => x.ParentPremase.Sres)
                .Select(x => x.ParentPremase.Dres)
                .Select(x => x.ParentPremase.Nres)
                .Select(x => x.ParentPremase.ArDtvv)
                .Select(x => x.ParentPremase.ArDt)
                .Select(x => x.ParentPremase.ArOsn)
                .Select(x => x.ParentPremase.ArDtsn)
                .Select(x => x.ParentPremase.ArOsnsn)
                .Execute().FirstOrDefault();
        }

        /// <summary>
        /// Возвращает все исторические и актуальную строки для заданного идентификатора
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public DataTable GetAllDataById(long id)
        {
            string commandText =
$@"SELECT F.*, B.GUID_FIAS_MKD AS GUID, O.NAME AS OKRUG, D.NAME AS DISTRICT, A.REGION, A.CITY, A.STREET, A.HOUSE, A.CORPUS, A.STRUCTURE, A.FULL_ADDRESS
FROM INSUR_FLAT_Q F
LEFT JOIN INSUR_BUILDING_Q B ON B.EMP_ID=F.LINK_OBJECT_MKD AND B.ACTUAL=1
LEFT JOIN REF_ADDR_OKRUG O ON O.OKRUG_ID=B.OKRUG_ID 
LEFT JOIN REF_ADDR_DISTRICT D ON D.DISTRICT_ID=B.DISTRICT_ID
LEFT JOIN INSUR_ADDRESS A ON A.EMP_ID=B.ADDRESS_ID
WHERE F.EMP_ID={id}";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// Возвращает все исторические и актуальную строки для заданного идентификатора из таблицы BTI_PREMASE
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        public DataTable GetBtiAllDataById(long id)
        {
            string commandText =
$@"SELECT 
	P.*, A.S_ AS AS_, A.PO_ AS APO_, A.*, C.S_ AS CS_, C.PO_ AS CPO_, C.FULL_NAME AS ARCHIVE,
	(select o.full_name from ref_addr_okrug o where o.okrug_id = a.okrug_id) as OKRUG_NAME, 
    (select d.full_name from ref_addr_district d where d.district_id = a.district_id) as DISTRICT_NAME
FROM BTI_PREMASE P
LEFT JOIN BTI_FLOOR_Q F ON F.EMP_ID=P.FLOOR_ID
LEFT JOIN BTI_BUILDING_Q B ON B.EMP_ID=F.BUILDING_ID
LEFT JOIN BTI_ADDRLINK_Q L ON L.BUILDING_ID=B.EMP_ID AND L.ADDRESS_STATUS_ID in (685, 770)
LEFT JOIN BTI_ADDRESS_Q A ON A.EMP_ID=L.ADDRESS_ID
LEFT JOIN BTI_ADDRLINK_Q AL ON AL.BUILDING_ID=B.EMP_ID AND AL.ADDRESS_STATUS_ID in (773)
LEFT JOIN BTI_ADDRESS_Q C ON C.EMP_ID=AL.ADDRESS_ID
WHERE P.EMP_ID={id}";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(commandText);

            return DBMngr.Realty.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// Возвращает необходимые сущности для отображения прав ЕГРН
        /// </summary>
        /// <param name="flatId">Идентифиактор квартиры</param>
        public void GetEgrnRights(long flatId,
            ref List<ObjectModel.Ehd.OMRegister> omRegisters,
            ref List<ObjectModel.Ehd.OMBuildParcel> omBuildParcels,
            ref List<ObjectModel.Ehd.OMEgrp> omEgrns,
            ref List<ObjectModel.Ehd.OMRight> omRights)
        {
            OMFlat omFlat = OMFlat.Where(x => x.EmpId == flatId)
                .Select(x => x.ParentBuilding.LinkEgrnBild)
                .Select(x => x.Kvnom)
                .Execute().FirstOrDefault();
            long? linkEgrnBild = omFlat?.ParentBuilding?.LinkEgrnBild;

            if (!linkEgrnBild.HasValue) return;

            ObjectModel.Ehd.OMBuildParcel omBuildParcel = ObjectModel.Ehd.OMBuildParcel.Where(x => x.EmpId == linkEgrnBild)
                .Select(x => x.ObjectId).Execute().FirstOrDefault();

            if (omBuildParcel == null) return;

            omRegisters = ObjectModel.Ehd.OMRegister.Where(x => x.CadastralNumberOks == omBuildParcel.ObjectId)
                .Select(x => x.BuildingParcelId).Select(x => x.AssftpCd).Execute();

            if (!omRegisters.Any()) return;

            List<long?> buildingParcelIds = omRegisters.Select(x => x.BuildingParcelId).ToList();
            omBuildParcels = ObjectModel.Ehd.OMBuildParcel.Where(x => buildingParcelIds.Contains(x.BuildingParcelId) && (x.ActualEHD == 0 || x.ActualEHD == null))
                .Select(x => x.BuildingParcelId)
                .Select(x => x.ObjectId).Execute();

            if (!omBuildParcels.Any()) return;

            List<string> cadastralNumbers = omBuildParcels.Select(x => x.ObjectId).ToList();
            omEgrns = ObjectModel.Ehd.OMEgrp.Where(x => cadastralNumbers.Contains(x.NumCadnum) && x.AddrApart == omFlat.Kvnom && (x.ActualId == 0 || x.ActualId == null))
                .Select(x => x.EhdEgrnId)
                .Select(x => x.Area)
                .Select(x => x.NumCadnum)
                .Select(x => x.Name)
                .Execute();

            if (!omEgrns.Any()) return;

            List<long?> egrpIds = omEgrns.Select(x => x.EhdEgrnId).ToList();
            List<OMRight> rights = OMRight.Where(x => egrpIds.Contains(x.EgrpId)).SelectAll().Execute();
            omRights.AddRange(rights.Where(x => !x.RegCloseRegdt.HasValue || x.RegCloseRegdt.Value.Date == new DateTime(1, 1, 1).Date).OrderByDescending(x => x.RegOpenRegdt));
            omRights.AddRange(rights.Where(x => x.RegCloseRegdt.HasValue && x.RegCloseRegdt.Value.Date > new DateTime(1, 1, 1).Date).OrderByDescending(x => x.RegCloseRegdt));
        }

        /// <summary>
        /// Обновляет существующее помещение
        /// </summary>
        /// <param name="newOMFlat">Помещение с новыми данными</param>
        public void Update(OMFlat newOMFlat)
        {
            OMFlat currentOMFlat = OMFlat.Where(x => x.EmpId == newOMFlat.EmpId).SelectAll().Execute().FirstOrDefault();

            if (currentOMFlat == null) throw new Exception($"Объект не найден (ИД={newOMFlat.EmpId})");

            Dictionary<string, SourceInput> sources = new Dictionary<string, SourceInput>();

            try { sources = JsonConvert.DeserializeObject<Dictionary<string, SourceInput>>(currentOMFlat.AttributeSource); }
            catch { }

            if ((currentOMFlat.LoadDate.HasValue && !newOMFlat.LoadDate.HasValue)
                || (!currentOMFlat.LoadDate.HasValue && newOMFlat.LoadDate.HasValue)
                || (currentOMFlat.LoadDate.HasValue && newOMFlat.LoadDate.HasValue
                && currentOMFlat.LoadDate.Value.Date != newOMFlat.LoadDate.Value.Date))
            {
                currentOMFlat.LoadDate = newOMFlat.LoadDate;
                sources[OMFlat.GetAttributeData(x => x.LoadDate).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.FlagInsur != newOMFlat.FlagInsur)
            {
                currentOMFlat.FlagInsur = newOMFlat.FlagInsur;
                sources[OMFlat.GetAttributeData(x => x.FlagInsur).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Unom != newOMFlat.Unom)
            {
                currentOMFlat.Unom = newOMFlat.Unom;
                sources[OMFlat.GetAttributeData(x => x.Unom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Kvnom != newOMFlat.Kvnom)
            {
                currentOMFlat.Kvnom = newOMFlat.Kvnom;
                sources[OMFlat.GetAttributeData(x => x.Kvnom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.CadastrNum != newOMFlat.CadastrNum)
            {
                currentOMFlat.CadastrNum = newOMFlat.CadastrNum;
                sources[OMFlat.GetAttributeData(x => x.CadastrNum).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.CadastrDate != newOMFlat.CadastrDate)
            {
                currentOMFlat.CadastrDate = newOMFlat.CadastrDate;
                sources[OMFlat.GetAttributeData(x => x.CadastrDate).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.StatusEgrn_Code != newOMFlat.StatusEgrn_Code)
            {
                currentOMFlat.StatusEgrn_Code = newOMFlat.StatusEgrn_Code;
                sources[OMFlat.GetAttributeData(x => x.StatusEgrn).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.StatusEgrn_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.KlassFlat_Code != newOMFlat.KlassFlat_Code)
            {
                currentOMFlat.KlassFlat_Code = newOMFlat.KlassFlat_Code;
                sources[OMFlat.GetAttributeData(x => x.KlassFlat).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.KlassFlat_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.TypeFlat_Code != newOMFlat.TypeFlat_Code)
            {
                currentOMFlat.TypeFlat_Code = newOMFlat.TypeFlat_Code;
                sources[OMFlat.GetAttributeData(x => x.TypeFlat).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.TypeFlat_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.FlatStatus != newOMFlat.FlatStatus)
            {
                currentOMFlat.FlatStatus = newOMFlat.FlatStatus;
                sources[OMFlat.GetAttributeData(x => x.FlatStatus).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Prkom != newOMFlat.Prkom)
            {
                currentOMFlat.Prkom = newOMFlat.Prkom;
                sources[OMFlat.GetAttributeData(x => x.Prkom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Fopl != newOMFlat.Fopl)
            {
                currentOMFlat.Fopl = newOMFlat.Fopl;
                sources[OMFlat.GetAttributeData(x => x.Fopl).ValueField] = SourceInput.ManualInput;
            }
            //CIPJS-737 отсуствуют на карточке LivingSpaces
            //if (currentOMFlat.Ppl != newOMFlat.Ppl)
            //{
            //    currentOMFlat.Ppl = newOMFlat.Ppl;
            //    sources[OMFlat.GetAttributeData(x => x.Ppl).ValueField] = SourceInput.ManualInput;
            //}
            //if (currentOMFlat.Gpl != newOMFlat.Gpl)
            //{
            //    currentOMFlat.Gpl = newOMFlat.Gpl;
            //    sources[OMFlat.GetAttributeData(x => x.Gpl).ValueField] = SourceInput.ManualInput;
            //}
            if (currentOMFlat.KolGp != newOMFlat.KolGp)
            {
                currentOMFlat.KolGp = newOMFlat.KolGp;
                sources[OMFlat.GetAttributeData(x => x.KolGp).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Note != newOMFlat.Note)
            {
                currentOMFlat.Note = newOMFlat.Note;
                sources[OMBuilding.GetAttributeData(x => x.Note).ValueField] = SourceInput.ManualInput;
            }

            string attributeSource = JsonConvert.SerializeObject(sources);
            if (currentOMFlat.AttributeSource != attributeSource)
            {
                currentOMFlat.AttributeSource = attributeSource;
            }

            if (currentOMFlat.PropertyChangedList.Count > 0)
            {
                currentOMFlat.Save();
            }
        }

        /// <summary>
        /// Создает помещение
        /// </summary>
        /// <param name="omFlat">Помещение</param>
        public long Create(OMFlat omFlat)
        {
            Dictionary<string, SourceInput> sources = new Dictionary<string, SourceInput>();

            if (omFlat.LoadDate.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.LoadDate).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.FlagInsur.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.FlagInsur).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.Unom.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.Unom).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.Kvnom.IsNotEmpty())
            {
                sources[OMFlat.GetAttributeData(x => x.Kvnom).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.CadastrNum.IsNotEmpty())
            {
                sources[OMFlat.GetAttributeData(x => x.CadastrNum).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.CadastrDate.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.CadastrDate).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.StatusEgrn_Code != State.None)
            {
                sources[OMFlat.GetAttributeData(x => x.StatusEgrn).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.StatusEgrn_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.KlassFlat_Code != Assftp_cd.None)
            {
                sources[OMFlat.GetAttributeData(x => x.KlassFlat).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.KlassFlat_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.TypeFlat_Code != Assftp1.None)
            {
                sources[OMFlat.GetAttributeData(x => x.TypeFlat).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.TypeFlat_Code).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.FlatStatus.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.FlatStatus).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.Prkom.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.Prkom).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.Fopl.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.Fopl).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.Ppl.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.Ppl).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.Gpl.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.Gpl).ValueField] = SourceInput.ManualInput;
            }
            if (omFlat.KolGp.HasValue)
            {
                sources[OMFlat.GetAttributeData(x => x.KolGp).ValueField] = SourceInput.ManualInput;
            }

            omFlat.EmpId = -1;
            omFlat.AttributeSource = JsonConvert.SerializeObject(sources);

            return omFlat.Save();
        }

        /// <summary>
        /// Получение списка ЖП, у МКД которых есть привязка к ЕГРН, но нет к БТИ
        /// </summary>
        /// <returns></returns>
        public List<OMFlat> GetFlatWithoutBti(long? buildingFias, long? buildingBti)
        {
            List<OMFlat> flats = OMFlat.Where(x => x.LinkObjectMkd == buildingFias).SelectAll()
                .Select(x => x.ParentBuilding.ParentBtiOkrug.Name)
                .Select(x => x.ParentBuilding.ParentBtiDistrict.Name)
                .Execute()
                .ToList();

            List<OMFlat> btiFlats = OMFlat.Where(x => x.LinkObjectMkd == buildingBti).SelectAll().Execute().ToList();
            //нужно убедиться, что нет однозначного совпадения с записями, у которых есть привязка только БТИ
            for (int i = 0; i < flats.Count; ++i)
            {
                List<OMFlat> btiFlatSelect = btiFlats.Where(x =>
                        (((x.CadastrNum == flats[i].CadastrNum && flats[i].CadastrNum != "") || (flats[i].CadastrNum == "" && x.CadastrNum == "")) &&
                        ((x.KlassFlat_Code == flats[i].KlassFlat_Code && flats[i].KlassFlat_Code != Assftp_cd.None) || (flats[i].KlassFlat_Code == Assftp_cd.None && x.KlassFlat_Code == Assftp_cd.None)) &&
                        ((x.Kvnom == flats[i].Kvnom && flats[i].Kvnom != "") || (flats[i].Kvnom == "" && x.Kvnom == "")) &&
                        ((x.Fopl == flats[i].Fopl && flats[i].Fopl != null) || (flats[i].Fopl == null && x.Fopl == null)) &&
                        ((x.TypeFlat_Code == flats[i].TypeFlat_Code && flats[i].TypeFlat_Code != Assftp1.None) || (flats[i].TypeFlat_Code == Assftp1.None && x.TypeFlat_Code == Assftp1.None))))
                        .ToList();
                if (btiFlatSelect.Count == 1)
                {
                    flats.RemoveAt(i--);
                }
            }

            return flats;
        }

        /// <summary>
        /// Получение списка ЖП, у МКД которых есть привязка к БТИ, но нет к ЕГРН
        /// </summary>
        /// <param name="flat">отбираем только записи, у которых есть совпадение с полями из flat</param>
        /// <returns></returns>
        public List<OMFlat> GetFlatWithoutFsks(OMFlat flat, long? buildingBti)
        {
            if (flat == null)
            {
                return new List<OMFlat>();
            }
            List<OMFlat> falts = OMFlat.Where(x => x.LinkObjectMkd == buildingBti &&
                ((x.CadastrNum == flat.CadastrNum && flat.CadastrNum != "") ||
                (x.KlassFlat_Code == flat.KlassFlat_Code && flat.KlassFlat_Code != Assftp_cd.None) ||
                (x.Kvnom == flat.Kvnom && flat.Kvnom != "") ||
                (x.Fopl == flat.Fopl && flat.Fopl != null) ||
                (x.TypeFlat_Code == flat.TypeFlat_Code && flat.TypeFlat_Code != Assftp1.None)))
                .SelectAll()
                .Select(x => x.ParentBuilding.ParentBtiOkrug.Name)
                .Select(x => x.ParentBuilding.ParentBtiDistrict.Name)
                .Execute()
                .ToList();

            return falts;
        }

        /// <summary>
        /// Получение списка ЖП, у МКД которых есть привязка к ЕГРН, но нет к БТИ и найдена аналогичная запись с привязкой к БТИ, но без ЕГРН
        /// </summary>
        /// <returns></returns>
        public List<OMFlat> GetFlatWithoutBtiWithSingleLink(long? buildingFias, long? buildingBti)
        {
            List<OMFlat> totalFlats = new List<OMFlat>();
            List<OMFlat> flats = OMFlat.Where(x => x.LinkObjectMkd == buildingFias).SelectAll()
                .Select(x => x.ParentBuilding.ParentBtiOkrug.Name)
                .Select(x => x.ParentBuilding.ParentBtiDistrict.Name)
                .Execute()
                .ToList();

            List<OMFlat> btiFlats = OMFlat.Where(x => x.LinkObjectMkd == buildingBti).SelectAll().Execute().ToList();
            //нужно убедиться, что есть однозначное совпадение с записью, у которой есть привязка только БТИ, и она одна
            for (int i = 0; i < flats.Count; ++i)
            {
                List<OMFlat> btiFlatSelect = btiFlats.Where(x => (x.CadastrNum == flats[i].CadastrNum && flats[i].CadastrNum != "") || (flats[i].CadastrNum == "" && x.CadastrNum == "")).ToList();
                if(btiFlatSelect.Count == 1)
                {
                    totalFlats.AddRange(btiFlatSelect);
                }
                else if (btiFlatSelect.Count > 1)
                {
                    btiFlatSelect = btiFlatSelect.Where(x => (x.Kvnom == flats[i].Kvnom && flats[i].Kvnom != "") || (flats[i].Kvnom == "" && x.Kvnom == "")).ToList();
                    if(btiFlatSelect.Count == 1)
                    {
                        totalFlats.AddRange(btiFlatSelect);
                    }
                    else
                    {
                        flats.RemoveAt(i--);
                    }
                }
                else
                {
                    btiFlatSelect = btiFlats.Where(x => ((x.Kvnom == flats[i].Kvnom && flats[i].Kvnom != "") || (flats[i].Kvnom == "" && x.Kvnom == ""))).ToList();
                    if (btiFlatSelect.Count == 1)
                    {
                        totalFlats.AddRange(btiFlatSelect);
                    }
                    else
                    {
                        flats.RemoveAt(i--);
                    }
                }
            }
            if (flats.Count > 0)
            {
                totalFlats.AddRange(flats);
            }

            return totalFlats;
        }

        /// <summary>
        /// Обновляет существующее помещение
        /// </summary>
        /// <param name="newOMFlat">Помещение с новыми данными</param>
        public OMFlat UpdateLink(OMFlat newOMFlat, string reason)
        {
            OMFlat currentOMFlat = OMFlat.Where(x => x.EmpId == newOMFlat.EmpId).SelectAll().Execute().FirstOrDefault();

            if (currentOMFlat == null) throw new Exception($"Объект не найден (ИД={newOMFlat.EmpId})");

            Dictionary<string, SourceInput> sources = new Dictionary<string, SourceInput>();

            try { sources = JsonConvert.DeserializeObject<Dictionary<string, SourceInput>>(currentOMFlat.AttributeSource); }
            catch { }

            if (currentOMFlat.Unom != newOMFlat.Unom)
            {
                new OMChangesLog
                {
                    ObjectId = currentOMFlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.Unom,
                    Reason = reason,
                    OldValue = currentOMFlat.Unom.ToString(),
                    NewValue = newOMFlat.Unom.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMFlat.Unom = newOMFlat.Unom;
                sources[OMFlat.GetAttributeData(x => x.Unom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Kvnom != newOMFlat.Kvnom)
            {
                new OMChangesLog
                {
                    ObjectId = currentOMFlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.Kvnom,
                    Reason = reason,
                    OldValue = currentOMFlat.Kvnom.ToString(),
                    NewValue = newOMFlat.Kvnom.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMFlat.Kvnom = newOMFlat.Kvnom;
                sources[OMFlat.GetAttributeData(x => x.Kvnom).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.CadastrNum != newOMFlat.CadastrNum)
            {
                new OMChangesLog
                {
                    ObjectId = currentOMFlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.CadastrNum,
                    Reason = reason,
                    OldValue = currentOMFlat.CadastrNum.ToString(),
                    NewValue = newOMFlat.CadastrNum.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMFlat.CadastrNum = newOMFlat.CadastrNum;
                sources[OMFlat.GetAttributeData(x => x.CadastrNum).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.KlassFlat_Code != newOMFlat.KlassFlat_Code)
            {
                new OMChangesLog
                {
                    ObjectId = currentOMFlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.KlassFlat,
                    Reason = reason,
                    OldValue = currentOMFlat.KlassFlat_Code.ToString(),
                    NewValue = newOMFlat.KlassFlat_Code.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMFlat.KlassFlat_Code = newOMFlat.KlassFlat_Code;
                sources[OMFlat.GetAttributeData(x => x.KlassFlat).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.KlassFlat_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.TypeFlat_Code != newOMFlat.TypeFlat_Code)
            {
                new OMChangesLog
                {
                    ObjectId = currentOMFlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.TypeFlat,
                    Reason = reason,
                    OldValue = currentOMFlat.TypeFlat_Code.ToString(),
                    NewValue = newOMFlat.TypeFlat_Code.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMFlat.TypeFlat_Code = newOMFlat.TypeFlat_Code;
                sources[OMFlat.GetAttributeData(x => x.TypeFlat).ValueField] = SourceInput.ManualInput;
                sources[OMFlat.GetAttributeData(x => x.TypeFlat_Code).ValueField] = SourceInput.ManualInput;
            }
            if (currentOMFlat.Fopl != newOMFlat.Fopl)
            {
                new OMChangesLog
                {
                    ObjectId = currentOMFlat.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.Fopl,
                    Reason = reason,
                    OldValue = currentOMFlat.Fopl.ToString(),
                    NewValue = newOMFlat.Fopl.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();
                currentOMFlat.Fopl = newOMFlat.Fopl;
                sources[OMFlat.GetAttributeData(x => x.Fopl).ValueField] = SourceInput.ManualInput;
            }

            currentOMFlat.AttributeSource = JsonConvert.SerializeObject(sources);
            currentOMFlat.Save();

            return currentOMFlat;
        }

        /// <summary>
        /// Перевод ЖП в статус "не актуален"
        /// </summary>
        /// <param name="id"></param>
        public void FlatNotActual(long? id)
        {
            if (id.HasValue)
            {
                OMFlat flat = OMFlat.Where(x => x.EmpId == id).SelectAll(false).Execute().FirstOrDefault();
                if (flat != null)
                {
                    flat.DestroyLogically();
                }
            }
        }

        /// <summary>
        /// Связываются ли ЖП двух зданий
        /// </summary>
        /// <param name="fiasBuildingId"></param>
        /// <param name="btiBuildingId"></param>
        public string CheckLinkFlats(long? fiasBuildingId, long? btiBuildingId)
        {
            List<OMFlat> flatsFias = OMFlat.Where(x => x.LinkObjectMkd == fiasBuildingId && x.LinkBtiFlat == null).SelectAll().Execute().ToList();

            List<OMFlat> flatsBti = OMFlat.Where(x => x.LinkObjectMkd == btiBuildingId).SelectAll().Execute().ToList();

            StringBuilder message = new StringBuilder();

            for (int i = 0; i < flatsFias.Count; ++i)
            {
                // ищем по БТИ
                List<OMFlat> btiFlatSelect = flatsBti.Where(x =>
                        (((x.CadastrNum == flatsFias[i].CadastrNum && flatsFias[i].CadastrNum != "") || (flatsFias[i].CadastrNum == "" && x.CadastrNum == "")) &&
                        ((x.KlassFlat_Code == flatsFias[i].KlassFlat_Code && flatsFias[i].KlassFlat_Code != Assftp_cd.None) || (flatsFias[i].KlassFlat_Code == Assftp_cd.None && x.KlassFlat_Code == Assftp_cd.None)) &&
                        ((x.Kvnom == flatsFias[i].Kvnom && flatsFias[i].Kvnom != "") || (flatsFias[i].Kvnom == "" && x.Kvnom == "")) &&
                        ((x.Fopl == flatsFias[i].Fopl && flatsFias[i].Fopl != null) || (flatsFias[i].Fopl == null && x.Fopl == null)) &&
                        ((x.TypeFlat_Code == flatsFias[i].TypeFlat_Code && flatsFias[i].TypeFlat_Code != Assftp1.None) || (flatsFias[i].TypeFlat_Code == Assftp1.None && x.TypeFlat_Code == Assftp1.None))))
                        .ToList();
                if (btiFlatSelect.Count == 1)
                {
                    // нашли связь, исключаем обе записи из списков
                    flatsFias.RemoveAt(i--);
                    flatsBti.Remove(btiFlatSelect.First());
                }
            }
            // если остались записи, то что-то не удалось распределить
            if (flatsBti.Count > 0)
            {
                message.AppendLine("Имеются нераспределенные ЖП в МКД, свзяанном с БТИ");
                foreach(var flatBti in flatsBti)
                {
                    message.AppendLine($"Квартира №{flatBti.Kvnom}, UNOM: {flatBti.Unom}");
                }
            }
            if (flatsFias.Count > 0)
            {
                message.AppendLine("Имеются нераспределенные ЖП в МКД, свзяанном с ЕГРН");
                foreach (var flatFias in flatsFias)
                {
                    message.AppendLine($"Квартира №{flatFias.Kvnom}, UNOM: {flatFias.Unom}");
                }
            }

            return message.ToString();
        }

		/// <summary>
		/// Связывание ЖП, у котрого нет связки с БТИ и ЖП, у которой нет связки с ЕГРН
		/// </summary>
		/// <param name="egrnFlatId"></param>
		/// <param name="btiFlatId"></param>
		public void LinkFlat(long? egrnFlatId, long? btiFlatId, OMBuilding insurBuilding = null)
        {
            OMFlat flatFias = OMFlat.Where(x => x.EmpId == egrnFlatId).SelectAll().Execute().FirstOrDefault();
            if (flatFias == null)
            {
                throw new Exception("Не найдена запись по Жилому помещению");
            }
            OMFlat flatBti = OMFlat.Where(x => x.EmpId == btiFlatId).SelectAll().Execute().FirstOrDefault();
            if (flatBti == null)
            {
                throw new Exception("Не найдена запись по Жилому помещению БТИ");
            }
            // Была доработка с дублями.
            if (insurBuilding == null)
            {
                insurBuilding = OMBuilding.Where(x => x.EmpId == flatFias.LinkObjectMkd).Select(x => x.GuidFiasMkd).Execute().FirstOrDefault();
            }
            if (insurBuilding == null)
            {
                throw new Exception("Не найдено МКД, относящееся к ЖП");
            }

            #region Config
            OMBuildParcel buildParcel = OMBuildParcel.Where(x => x.EmpId == flatFias.LinkInsurEgrn).SelectAll().Execute().FirstOrDefault();
            if( (buildParcel != null && buildParcel.ActualEHD != 0 && buildParcel.ActualEHD != null || buildParcel == null) && flatBti.LinkInsurEgrn.HasValue)
            {
                buildParcel = OMBuildParcel.Where(x => x.EmpId == flatBti.LinkInsurEgrn).SelectAll().Execute().FirstOrDefault();
            }
            if (buildParcel == null)
            {
                throw new Exception("Не найдена информация по ЖП в ЕГРН");
            }
            OMRegister register = OMRegister.Where(x => x.BuildingParcelId == buildParcel.EmpId).SelectAll().Execute().FirstOrDefault();
            OMEgrp egrp = OMEgrp.Where(x => x.NumCadnum == buildParcel.ObjectId).SelectAll().Execute().FirstOrDefault();
            OMLocation locatuon = OMLocation.Where(x => x.BuildingParcelId == flatFias.LinkInsurEgrn).SelectAll().Execute().FirstOrDefault();
            if(locatuon == null && flatBti.LinkInsurEgrn.HasValue)
            {
                locatuon = OMLocation.Where(x => x.BuildingParcelId == flatBti.LinkInsurEgrn).SelectAll().Execute().FirstOrDefault();
            }
            if (locatuon == null)
            {
                //throw new Exception("Не найдена ehd.location по ЖП в ЕГРН");
            }
            OMPremase premise = OMPremase.Where(x => x.EmpId == flatBti.LinkBtiFlat).SelectAll().Execute().FirstOrDefault();
            if (premise == null)
            {
                throw new Exception("Не найдена информация по ЖП в БТИ");
            }
            OMFloor floor = OMFloor.Where(x => x.EmpId == premise.FloorId).SelectAll().Execute().FirstOrDefault();
            if (floor == null)
            {
                throw new Exception("Не найдена информация по связи по ЖП в БТИ");
            }

            Dictionary<long, Object> objects = new Dictionary<long, object>
            {
				{ OMBuilding.GetRegisterId(), insurBuilding },

				{ OMPremase.GetRegisterId(), premise },
                { OMFloor.GetRegisterId(), floor },

                { OMBuildParcel.GetRegisterId(), buildParcel },
                { OMRegister.GetRegisterId(), register },
                { OMEgrp.GetRegisterId(), egrp },
                { OMLocation.GetRegisterId(), locatuon },
            };

            var flat = BuildingService.Map<OMFlat>("InsurObjectMapFlat", OMFlat.GetRegisterId(), objects, flatFias.AttributeSource, out string mapLogAttribute);
            flat.EmpId = flatFias.EmpId;
            flat.LinkInsurEgrn = flatFias.LinkInsurEgrn;
            flat.LinkBtiFlat = flatBti.LinkBtiFlat;
            flat.GuidFiasMkd = insurBuilding.GuidFiasMkd;
            flat.FlagInsur = insurBuilding.FlagInsur;
            flat.LinkObjectMkd = insurBuilding.EmpId;
            flat.LoadDate = DateTime.Now;
            flat.AttributeSource = mapLogAttribute;
            // Дополнительная обработка
            if (flat.PropertyChangedList.Contains("Kvnom"))
            {
                flat.Kvnom = flat.Kvnom.Replace("Квартира", "").Trim();
            }
            #endregion            

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Required))
            {
                flat.Save();

              
                //flatBti.FlagInsur = false;
                //flatBti.Save();

                List<OMFsp> fsps = OMFsp.Where(x => x.ObjId == btiFlatId && x.ObjReestrId == OMFlat.GetRegisterId()).Execute();
                if (fsps != null)
                {
                    foreach (OMFsp fsp in fsps)
                    {
                        fsp.ObjId = egrnFlatId;
                        fsp.Save();
                    }
                }

                List<OMDamage> damages = OMDamage.Where(x => x.ObjId == btiFlatId && x.ObjReestrId == OMFlat.GetRegisterId()).Execute();
                if (damages != null)
                {
                    foreach (OMDamage damage in damages)
                    {
                        damage.ObjId = egrnFlatId;
                        damage.Save();
                    }
                }

                new OMChangesLog
                {
                    ObjectId = flatFias.EmpId,
                    LoadDate = DateTime.Now,
                    ReestrId = OMFlat.GetRegisterId(),
                    OperationType_Code = ChangeOperationType.LinkFlats,
                    Reason = "Связка объектов ЖП привязанных к ЕГРН и БТИ ",
                    OldValue = null,
                    NewValue = flatBti.LinkBtiFlat.ToString(),
                    UserId = SRDSession.GetCurrentUserId()
                }.Save();

                // В import_log_insur_flat делать запись о изменении ссылки в insur_building_id на новый МКД
                _importLogInsurFlatService.FillInsurFlatBuildingLog(flat);
                _importLogInsurFlatService.RemoveInsurFlatBuildingLog(flatBti);
                flatBti.DestroyLogically();

                ts.Complete();
            }
        }

        /// <summary>
        /// Получаем emp_id ЖП по unom.
        /// </summary>
        /// <param name="unom"></param>
        /// <param name="kvnom"></param>
        /// <returns></returns>
        public long? GetFlatIdByUnom(long unom, string kvnom)
        {
            if (kvnom == null)
                return null;
            var flat = OMFlat
                .Where(x => x.ParentBuilding.Unom == unom && x.Kvnom == kvnom)
                .SetPackageSize(1)
                .ExecuteFirstOrDefault();
            return flat?.EmpId;
        }

        /// <summary>
        /// Получаем данные changes_user_id и changes_date по ЖП.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public (string changeUser, string changeDate) GetFlatChangesData(long? id)
        {
            string query =
 $@"select changes_user_id, changes_date from insur_flat_q where emp_id = {id} and actual = 1";
            DbCommand command = DBMngr.Realty.GetSqlStringCommand(query);
            var response = DBMngr.Realty.ExecuteDataSet(command).Tables[0].Rows;
            if (response.Count > 0)
            {
                var row = response[0];
                return (changeUser: row["changes_user_id"].ToString(), changeDate: row["changes_date"].ToString());
            }

            return (string.Empty, string.Empty);
        }
    }
}