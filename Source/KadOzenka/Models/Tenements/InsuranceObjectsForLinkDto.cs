using ObjectModel.Insur;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Tenements
{
    public class InsuranceObjectsForLinkDto
    {
        /// <summary>
        /// Идентификатор объекта МКД
        /// </summary>
        public long EmpId { get; set; }

        [Display(Name = "UNOM")]
        public long? Unom { get; set; }

        public bool IsMatchUnom { get; set; }

        [Display(Name = "Кадастровый номер")]
        public string CadastralNumber { get; set; }

        public bool IsMatchCadastralNumber { get; set; }

        public long? OkrugId { get; set; }

        public bool IsMatchOkrugId { get; set; }

        [Display(Name = "Округ")]
        public string OkrugShortName { get; set; }

        public long? DistrictId { get; set; }

        public bool IsMatchDistrictId { get; set; }

        [Display(Name = "Район")]
        public string DistrictShortName { get; set; }

        public long? AddressId { get; set; }

        public bool IsMatchAddressId { get; set; }

        public string AddressFullName { get; set; }

        [Display(Name = "Год постройки")]
        public long? ConstructionYear { get; set; }

        public bool IsMatchConstructionYear { get; set; }

        [Display(Name = "Количество квартир в доме")]
        public long? ApartmentsCount { get; set; }

        public bool IsMatchApartmentsCount { get; set; }

        [Display(Name = "Общая площадь")]
        public decimal? TotalArea { get; set; }

        public bool IsMatchTotalArea { get; set; }

        public string GroupName
        {
            get
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7:0.00}", Unom, CadastralNumber, OkrugShortName, DistrictShortName, AddressFullName, ConstructionYear, ApartmentsCount, TotalArea);
            }
        }

        public static InsuranceObjectsForLinkDto Empty()
        {
            return new InsuranceObjectsForLinkDto();
        }

        public static InsuranceObjectsForLinkDto Map(OMBuilding omBuilding)
        {
            return new InsuranceObjectsForLinkDto
            {
                EmpId = omBuilding.EmpId,
                Unom = omBuilding.Unom,
                CadastralNumber = omBuilding.CadasrNum,
                OkrugId = omBuilding.OkrugId,
                OkrugShortName = omBuilding.OkrugId.HasValue ? (new ObjectModel.Bti.OMBtiOkrug { Id = omBuilding.OkrugId.Value })?.ShortName : null,
                DistrictId = omBuilding.DistrictId,
                DistrictShortName = omBuilding.ParentBtiDistrict?.ShortName,
                AddressId = omBuilding.AddressId,
                AddressFullName = InsuranceObjectAddressDto.Map(omBuilding)?.FullName,
                ConstructionYear = omBuilding.YearStroi,
                ApartmentsCount = omBuilding.KolGp,
                TotalArea = omBuilding.Opl
            };
        }

        public static InsuranceObjectsForLinkDto Map(OMBuilding omBuilding, OMBuilding comparerBuilding)
        {
            return new InsuranceObjectsForLinkDto
            {
                EmpId = omBuilding.EmpId,
                Unom = omBuilding.Unom,
                IsMatchUnom = omBuilding.Unom == comparerBuilding.Unom,
                CadastralNumber = omBuilding.CadasrNum,
                IsMatchCadastralNumber = omBuilding.CadasrNum == comparerBuilding.CadasrNum,
                OkrugId = omBuilding.OkrugId,
                IsMatchOkrugId = omBuilding.OkrugId == comparerBuilding.OkrugId,
                OkrugShortName = omBuilding.OkrugId.HasValue ? (new ObjectModel.Bti.OMBtiOkrug { Id = omBuilding.OkrugId.Value })?.ShortName : null,
                DistrictId = omBuilding.DistrictId,
                IsMatchDistrictId = omBuilding.DistrictId == comparerBuilding.DistrictId,
                DistrictShortName = omBuilding.ParentBtiDistrict?.ShortName,
                AddressId = omBuilding.AddressId,
                IsMatchAddressId = omBuilding.AddressId == comparerBuilding.AddressId,
                AddressFullName = InsuranceObjectAddressDto.Map(omBuilding)?.FullName,
                ConstructionYear = omBuilding.YearStroi,
                IsMatchConstructionYear = omBuilding.YearStroi == comparerBuilding.YearStroi,
                ApartmentsCount = omBuilding.KolGp,
                IsMatchApartmentsCount = omBuilding.KolGp == comparerBuilding.KolGp,
                TotalArea = omBuilding.Opl,
                IsMatchTotalArea = omBuilding.Opl == comparerBuilding.Opl,
            };
        }

        public static OMBuilding Map(InsuranceObjectsForLinkDto model)
        {
            return new OMBuilding
            {
                EmpId = model.EmpId,
                Unom = model.Unom,
                CadasrNum = model.CadastralNumber,
                OkrugId = model.OkrugId,
                DistrictId = model.DistrictId,
                AddressId = model.AddressId,
                YearStroi = model.ConstructionYear,
                KolGp = model.ApartmentsCount,
                Opl = model.TotalArea
            };
        }
    }
}
