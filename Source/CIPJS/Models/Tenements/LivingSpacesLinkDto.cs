using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.Tenements
{
    public class LivingSpacesLinkDto
    {
        /// <summary>
        /// Идентификатор помещения
        /// </summary>
        public long EmpId { get; set; }

        [Display(Name = "UNOM МКД")]
        public long? Unom { get; set; }

        public bool IsMatchUnom { get; set; }

        [Display(Name = "Кадастровый номер")]
        public string CadastralNumber { get; set; }

        public bool IsMatchCadastralNumber { get; set; }

        [Display(Name = "Округ")]
        public string Okrug { get; set; }

        [Display(Name = "Район")]
        public string District { get; set; }

        [Display(Name = "Номер квартиры")]
        public long? FlatNumber { get; set; }

        public bool IsMatchFlatNumber { get; set; }

        [Display(Name = "Тип помещения")]
        public string SpaceType { get; set; }

        [Display(Name = "Тип помещения")]
        public Assftp1? SpaceType_Code { get; set; }

        public bool IsMatchSpaceType { get; set; }

        [Display(Name = "Назначение помещения")]
        public string SpacePurpose { get; set; }

        [Display(Name = "Назначение помещения")]
        public Assftp_cd? SpacePurpose_Code { get; set; }

        public bool IsMatchSpacePurpose { get; set; }

        [Display(Name = "Общая площадь")]
        public decimal? TotalArea { get; set; }

        public bool IsMatchTotalArea { get; set; }

        public string GroupName
        {
            get
            {
                return string.Format("{0}{1}{2}{3}{4}", Unom, CadastralNumber, FlatNumber, SpaceType, SpacePurpose);
            }
        }

        public static LivingSpacesLinkDto Map(OMFlat omFlat)
        {
            return new LivingSpacesLinkDto
            {
                EmpId = omFlat.EmpId,
                Unom = omFlat.Unom,
                FlatNumber = omFlat.Kvnom.IsNotEmpty() ? omFlat.Kvnom.ParseToLong() : (long?)null,
                CadastralNumber = omFlat.CadastrNum,
                SpacePurpose = omFlat.KlassFlat,
                SpacePurpose_Code = omFlat.KlassFlat_Code,
                SpaceType = omFlat.TypeFlat,
                SpaceType_Code = omFlat.TypeFlat_Code,
                TotalArea = omFlat.Fopl,
                Okrug = omFlat.ParentBuilding?.ParentBtiOkrug?.Name,
                District = omFlat.ParentBuilding?.ParentBtiDistrict?.Name,
            };
        }

        public static LivingSpacesLinkDto Map(OMFlat omFlat, OMFlat comparerFlat)
        {
            return new LivingSpacesLinkDto
            {
                EmpId = omFlat.EmpId,
                Unom = omFlat.Unom,
                IsMatchUnom = omFlat.Unom == comparerFlat.Unom,
                FlatNumber = omFlat.Kvnom.IsNotEmpty() ? omFlat.Kvnom.ParseToLong() : (long?)null,
                IsMatchFlatNumber = omFlat.Kvnom == comparerFlat.Kvnom,
                CadastralNumber = omFlat.CadastrNum,
                IsMatchCadastralNumber = omFlat.CadastrNum == comparerFlat.CadastrNum,
                SpacePurpose = omFlat.KlassFlat,
                SpacePurpose_Code = omFlat.KlassFlat_Code,
                IsMatchSpacePurpose = omFlat.KlassFlat_Code == comparerFlat.KlassFlat_Code,
                SpaceType = omFlat.TypeFlat,
                SpaceType_Code = omFlat.TypeFlat_Code,
                IsMatchSpaceType = omFlat.TypeFlat_Code == comparerFlat.TypeFlat_Code,
                TotalArea = omFlat.Fopl,
                IsMatchTotalArea = omFlat.Fopl == comparerFlat.Fopl,
                Okrug = omFlat.ParentBuilding?.ParentBtiOkrug?.Name,
                District = omFlat.ParentBuilding?.ParentBtiDistrict?.Name,
            };
        }

        public static OMFlat Map(LivingSpacesLinkDto model)
        {
            return new OMFlat
            {
                EmpId = model.EmpId,
                Unom = model.Unom,
                Kvnom = model.FlatNumber.ToString(),
                CadastrNum = model.CadastralNumber,
                KlassFlat_Code = model.SpacePurpose_Code.HasValue ? model.SpacePurpose_Code.Value : Assftp_cd.None,
                TypeFlat_Code = model.SpaceType_Code.HasValue ? model.SpaceType_Code.Value : Assftp1.None,
                Fopl = model.TotalArea
            };
        }
    }
}