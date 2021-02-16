using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;

namespace Parser
{

    class Hash
    {

        public List<AgregatorsData> AgregatorsData { get; set; }
        public List<AgregatorsDataExtra> AgregatorsDataExtra { get; set; }
        public List<CustomDistricts> CustomDistricts { get; set; }
        public List<CustomRegions> CustomRegions { get; set; }
        public List<DealTypes> DealTypes { get; set; }
        public List<ProcessTypes> ProcessTypes { get; set; }
        public List<CIPJSPropertyTypes> CIPJSPropertyTypes { get; set; }
        public List<Segments> Segments { get; set; }
        public List<UrlTypes> UrlTypes { get; set; }
        public List<ExactSegments> ExactSegments { get; set; }
        public List<CapchaSettings> CapchaSettingsHash { get; set; }
        public List<HashCombined> CIANHashCombined { get; set; }
        public List<HashCombined> AvitoHashCombined { get; set; }
        public List<HashCombined> YandexPropertyHashCombined { get; set; }

        public Hash()
        {
            AgregatorsData = new AgregatorsData().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_agregators");
            AgregatorsDataExtra = new AgregatorsDataExtra().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_markert_agregators_specifications");
            CustomDistricts = new CustomDistricts().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_custom_districts");
            CustomRegions = new CustomRegions().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_custom_regions");
            DealTypes = new DealTypes().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_deal_types");
            ProcessTypes = new ProcessTypes().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_process_types");
            CIPJSPropertyTypes = new CIPJSPropertyTypes().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_property_type_cipjs");
            Segments = new Segments().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_segments");
            UrlTypes = new UrlTypes().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_market_url_types");
            ExactSegments = new ExactSegments().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_exact_segments");
            CapchaSettingsHash = new CapchaSettings().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_capcha_settings");
            CIANHashCombined = GenerateHash(AgregatorsData.First(x => x.Code == 1));
            AvitoHashCombined = GenerateHash(AgregatorsData.First(x => x.Code == 2));
            YandexPropertyHashCombined = GenerateHash(AgregatorsData.First(x => x.Code == 3));

            GenerateURL(CIANHashCombined);
            GenerateURL(AvitoHashCombined);
            GenerateURL(YandexPropertyHashCombined);
        }

        public void RefreshAgregatorsDataExtra()=> 
            AgregatorsDataExtra = new AgregatorsDataExtra().GetAllCashe(ConfigurationManager.AppSettings["ConnectionString"], "parser_markert_agregators_specifications");

        private List<HashCombined> GenerateHash(AgregatorsData agregator)
        {
            List<HashCombined> result = new List<HashCombined>();
            switch (agregator.Code)
            {
                case 1:
                    DealTypes.Where(x => x.Cian_url_code != null).ToList().ForEach(x => 
                    {
                        ExactSegments.Where(y => y.Url_type_id_cian != null).ToList().ForEach(y =>
                        {
                            CustomRegions.Where(z => z.Cian_code != null).ToList().ForEach(z =>
                            {
                                CustomDistricts district = CustomDistricts.First(i => i.Id == z.District_id);
                                result.Add(new HashCombined
                                {
                                    Id = y.Id,
                                    Type = y.Type,
                                    AgregatorData = agregator,
                                    AgregatorDataExtra = AgregatorsDataExtra.First(i => agregator.Id == i.Agregator_id),
                                    PropertyType = CIPJSPropertyTypes.First(i => y.Property_type_id == i.Id),
                                    Segment = Segments.First(i => y.Market_segment_id == i.Id),
                                    Comment = y.Comment,
                                    UrlType = (UrlType)y.Url_type_id_cian,
                                    UrlMainPart = y.Url_main_part_cian,
                                    UrlExectPart = y.Url_exect_part_cian,
                                    UrlAsParameterType = y.Url_as_parameter_type_cian,
                                    UrlAsParameterValue = y.Url_as_parameter_value_cian,
                                    PropertyTypeForScripts = y.Cian_property_type,
                                    DealTypes = new HashDealTypes
                                    {
                                        Id = x.Id,
                                        Code = x.Code,
                                        ReferenceId = x.ReferenceId,
                                        Name = x.Name,
                                        Value = x.Value,
                                        Comment = x.Comment,
                                        UrlCode = x.Cian_url_code
                                    },
                                    CustomRegions = new HashCustomRegions
                                    {
                                        Id = z.Id,
                                        Code = z.Code,
                                        DistrictId = z.District_id,
                                        ReferenceId = z.ReferenceId,
                                        Name = z.Name,
                                        Value = z.Value,
                                        RegionCode = z.Cian_code
                                    },
                                    CustomDistricts = new HashCustomDistricts
                                    {
                                        Id = district.Id,
                                        Code = district.Code,
                                        ReferenceId = district.ReferenceId,
                                        Name = district.Name,
                                        Value = district.Value,
                                        DistrictCode = district.Cian_code
                                    }
                                });
                            });
                        });
                    });
                    break;
                case 2:
                    DealTypes.Where(x => x.Avito_url_code != null).ToList().ForEach(x =>
                    {
                        ExactSegments.Where(y => y.Url_type_id_avito != null).ToList().ForEach(y =>
                        {
                            CustomRegions.Where(z => z.Avito_code != null).ToList().ForEach(z =>
                            {
                                CustomDistricts district = CustomDistricts.First(i => i.Id == z.District_id);
                                result.Add(new HashCombined
                                {
                                    Id = y.Id,
                                    Type = y.Type,
                                    AgregatorData = agregator,
                                    AgregatorDataExtra = AgregatorsDataExtra.First(i => agregator.Id == i.Agregator_id),
                                    PropertyType = CIPJSPropertyTypes.First(i => y.Property_type_id == i.Id),
                                    Segment = Segments.First(i => y.Market_segment_id == i.Id),
                                    Comment = y.Comment,
                                    UrlType = (UrlType)y.Url_type_id_avito,
                                    UrlMainPart = y.Url_main_part_avito,
                                    UrlExectPart = y.Url_exect_part_avito,
                                    UrlAsParameterType = y.Url_as_parameter_type_avito,
                                    UrlAsParameterValue = y.Url_as_parameter_value_avito,
                                    PropertyTypeForScripts = y.Avito_property_type,
                                    DealTypes = new HashDealTypes
                                    {
                                        Id = x.Id,
                                        Code = x.Code,
                                        ReferenceId = x.ReferenceId,
                                        Name = x.Name,
                                        Value = x.Value,
                                        Comment = x.Comment,
                                        UrlCode = x.Avito_url_code
                                    },
                                    CustomRegions = new HashCustomRegions
                                    {
                                        Id = z.Id,
                                        Code = z.Code,
                                        DistrictId = z.District_id,
                                        ReferenceId = z.ReferenceId,
                                        Name = z.Name,
                                        Value = z.Value,
                                        RegionCode = z.Avito_code
                                    },
                                    CustomDistricts = new HashCustomDistricts
                                    {
                                        Id = district.Id,
                                        Code = district.Code,
                                        ReferenceId = district.ReferenceId,
                                        Name = district.Name,
                                        Value = district.Value,
                                        DistrictCode = district.Avito_code
                                    }
                                });
                            });
                        });
                    });
                    break;
                case 3:
                    DealTypes.Where(x => x.Yandex_property_url_code != null).ToList().ForEach(x =>
                    {
                        ExactSegments.Where(y => y.Url_type_id_yandex_property != null).ToList().ForEach(y =>
                        {
                            CustomRegions.Where(z => z.Yandex_property_code != null).ToList().ForEach(z =>
                            {
                                CustomDistricts district = CustomDistricts.First(i => i.Id == z.District_id);
                                result.Add(new HashCombined
                                {
                                    Id = y.Id,
                                    Type = y.Type,
                                    AgregatorData = agregator,
                                    AgregatorDataExtra = AgregatorsDataExtra.First(i => agregator.Id == i.Agregator_id),
                                    PropertyType = CIPJSPropertyTypes.First(i => y.Property_type_id == i.Id),
                                    Segment = Segments.First(i => y.Market_segment_id == i.Id),
                                    Comment = y.Comment,
                                    UrlType = (UrlType)y.Url_type_id_yandex_property,
                                    UrlMainPart = y.Url_main_part_yandex_property,
                                    UrlExectPart = y.Url_exect_part_yandex_property,
                                    UrlAsParameterType = y.Url_as_parameter_type_yandex_property,
                                    UrlAsParameterValue = y.Url_as_parameter_value_yandex_property,
                                    PropertyTypeForScripts = y.Yandex_property_property_type,
                                    DealTypes = new HashDealTypes
                                    {
                                        Id = x.Id,
                                        Code = x.Code,
                                        ReferenceId = x.ReferenceId,
                                        Name = x.Name,
                                        Value = x.Value,
                                        Comment = x.Comment,
                                        UrlCode = x.Yandex_property_url_code
                                    },
                                    CustomRegions = new HashCustomRegions
                                    {
                                        Id = z.Id,
                                        Code = z.Code,
                                        DistrictId = z.District_id,
                                        ReferenceId = z.ReferenceId,
                                        Name = z.Name,
                                        Value = z.Value,
                                        RegionCode = z.Yandex_property_code
                                    },
                                    CustomDistricts = new HashCustomDistricts
                                    {
                                        Id = district.Id,
                                        Code = district.Code,
                                        ReferenceId = district.ReferenceId,
                                        Name = district.Name,
                                        Value = district.Value,
                                        DistrictCode = district.Yandex_property_code
                                    }
                                });
                            });
                        });
                    });
                    break;
            }
            return result;
        }

        private void GenerateURL(List<HashCombined> combinedHash)
        {
            combinedHash.ForEach(x => 
            {
                switch (x.UrlType)
                {
                    case UrlType.Ordinar:
                        x.Url = string.Format(x.AgregatorDataExtra.Ordinar_url_template, x.DealTypes.UrlCode, x.UrlMainPart, x.UrlExectPart, x.CustomRegions.RegionCode, "1");
                        break;
                    case UrlType.Root:
                        x.Url = string.Format(x.AgregatorDataExtra.Root_url_template, x.DealTypes.UrlCode, x.UrlMainPart, x.CustomRegions.RegionCode, "1");
                        break;
                    case UrlType.AsParameter:
                        x.Url = string.Format(x.AgregatorDataExtra.As_parameter_url_template, x.DealTypes.UrlCode, x.UrlMainPart, x.UrlAsParameterType, x.UrlAsParameterValue, x.CustomRegions.RegionCode, "1");
                        break;
                }
            });
        }

    }

}
