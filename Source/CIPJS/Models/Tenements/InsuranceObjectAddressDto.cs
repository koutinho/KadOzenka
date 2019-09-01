using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CIPJS.Models.Tenements
{
    public class InsuranceObjectAddressDto
    {
        public long? Id { get; set; }

        /// <summary>
        /// Код ФИАС.
        /// </summary>
        [Display(Name = "Код ФИАС")]
        public string FIAS { get; set; }

        /// <summary>
        /// Индекс.
        /// </summary>
        [Display(Name = "Индекс")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Регион.
        /// </summary>
        [Display(Name = "Регион")]
        public string Region { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        [Display(Name = "Город")]
        public string City { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        [Display(Name = "Улица")]
        public string Street { get; set; }

        /// <summary>
        /// Тип дома.
        /// </summary>
        [Display(Name = "Тип дома")]
        public string TypeHouse { get; set; }

        /// <summary>
        /// Дом.
        /// </summary>
        [Display(Name = "Дом")]
        public string House { get; set; }

        /// <summary>
        /// Корпус.
        /// </summary>
        [Display(Name = "Корпус")]
        public string Corpus { get; set; }

        /// <summary>
        /// Строение.
        /// </summary>
        [Display(Name = "Строение")]
        public string Structure { get; set; }

        /// <summary>
        /// Населенный пункт.
        /// </summary>
        [Display(Name = "Населенный пункт")]
        public string Locality { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        [Display(Name = "Адрес")]
        public string FullName { get; set; }

        public static InsuranceObjectAddressDto Map(OMBuilding omBuilding)
        {
            return new InsuranceObjectAddressDto
            {
                Id = omBuilding?.AddressId,
                FIAS = omBuilding?.GuidFiasMkd,
                PostalCode = omBuilding?.ParentAddress?.PostalCode,
                FullName = omBuilding?.ParentAddress?.FullAddress,
                Region = omBuilding?.ParentAddress?.Region,
                City = omBuilding?.ParentAddress?.City,
                Street = $"{omBuilding?.ParentAddress?.Street} {omBuilding?.ParentAddress?.TypeStreet}",
                TypeHouse = omBuilding?.ParentAddress?.TypeHouse,
                House = omBuilding?.ParentAddress?.House,
                Corpus = omBuilding?.ParentAddress?.Corpus,
                Structure = omBuilding?.ParentAddress?.Structure,
                Locality = omBuilding?.ParentAddress?.Locality
            };
        }

        public static OMAddress Map(InsuranceObjectAddressDto model)
        {
            var region = GetSeparateRegion(model?.Region);
            var street = GetSeparateObject(model?.Street);


            return new OMAddress
            {
                EmpId = model?.Id ?? -1,
                FullAddress = model?.FullName,
                TypeRegion = region.Item1,
                Region = $"{region.Item2}",
                City = model?.City,
                TypeStreet = street.Item2,
                Street = street.Item1.Trim(),
                TypeHouse = model?.TypeHouse,
                House = model?.House,
                Corpus = model?.Corpus,
                Structure = model?.Structure,
                ShortAddress = GetShortAddress(model),
                Locality = model.Locality
            };
        }

        /// <summary>
        /// Получить по отдельности тип региона и регион
        /// </summary>
        /// <param name="region">Регион</param>
        private static Tuple<string, string> GetSeparateRegion(string region)
        {
            var result = Tuple.Create("", "");

            if (!string.IsNullOrEmpty(region))
            {
                if (region.Contains("Москва"))
                {
                    result = Tuple.Create("г.", "Москва");
                }
                if (region.Contains("Московская"))
                {
                    result = Tuple.Create("обл.", "Московская");
                }
            }

            return result;
        }

        /// <summary>
        /// Получить по отдельности тип объекта и сам объект
        /// </summary>
        /// <param name="obj">объект</param>
        private static Tuple<string, string> GetSeparateObject(string obj)
        {
            string pattern = @"(.*)(д.|кв-л|ст.|просек|просек-зд-|платф.|пр-д|пл.|ш.|наб.|тер.|мкр.|пер.|лн.|ал.|ул.|б-р|пр-кт|туп.|км)(.*)";

            var result = Tuple.Create("", "");

            if (!string.IsNullOrEmpty(obj))
            {
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = regex.Match(obj);

                if (match.Success)
                {
                    string v = match.Groups[1].Value;
                    result = Tuple.Create(match.Groups[1].Value, match.Groups[2].Value.TrimStart());
                }
            }

            return result;
        }

        /// <summary>
        /// Получить короткий адресс
        /// </summary>
        private static string GetShortAddress(InsuranceObjectAddressDto model)
        {
            string result = string.Empty;
            var address = new List<string>();

            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.Street))
                {
                    var res = GetSeparateObject(model.Street);
                    address.Add($"{res.Item1.Trim()} {res.Item2}");
                }
                if (!string.IsNullOrEmpty(model.House))
                    address.Add("д. " + model.House);
                if (!string.IsNullOrEmpty(model.Corpus))
                    address.Add("корп. " + model.Corpus);
                if (!string.IsNullOrEmpty(model.Structure))
                    address.Add("стр. " + model.Structure);

                return string.Join(", ", address);
            }

            return result;
        }
    }
}