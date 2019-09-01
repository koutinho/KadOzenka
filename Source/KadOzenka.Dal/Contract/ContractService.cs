using Core.SRD;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Linq;

namespace CIPJS.DAL.Contract
{
    public class ContractService
    {
        /// <summary>
        /// Возвращает договор страхования общего имущества
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public OMAllProperty GetById(long id)
        {
            OMAllProperty omAllProperty = OMAllProperty
                .Where(x => x.EmpId == id)
                .SelectAll()
                .Select(x => x.ParentInsuranceOrganization.ShortName)
                .Select(x => x.ParentBuilding.Unom)
                .Select(x => x.ParentBuilding.CadasrNum)
                .Select(x => x.ParentBuilding.Opl)
                .Select(x => x.ParentBuilding.Bpl)
                .Select(x => x.ParentBuilding.Lpl)
                .Select(x => x.ParentBuilding.KolGp)
                .Select(x => x.ParentBuilding.ParentAddress.FullAddress)
                .Execute()
                .FirstOrDefault();

            if (omAllProperty == null) throw new Exception($"Сущность \"Договор страхования\" с EmpId={id} не найдена");

            return omAllProperty;
        }

        /// <summary>
        /// Переводит договор в статус "Согласовано"
        /// </summary>
        public void Agreed(long id)
        {
            OMAllProperty omAllProperty = OMAllProperty.Where(x => x.EmpId == id).Execute().FirstOrDefault();

            if (omAllProperty == null) throw new Exception($"Сущность \"Договор страхования\" с EmpId={id} не найдена");

            omAllProperty.Status_Code = ContractStatus.Agreed;
            //omAllProperty.MainAgreementId = SRDSession.GetCurrentUserId();
            //omAllProperty.DateFillMain = DateTime.Now;

            omAllProperty.Save();
        }
    }
}