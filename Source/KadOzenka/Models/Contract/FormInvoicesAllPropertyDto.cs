using ObjectModel.Insur;
using System;

namespace CIPJS.Models.Contract
{
    public class FormInvoicesAllPropertyDto
    {
        public long Ordinal { get; set; }

        public string Ndog { get; set; }

        public DateTime? Ndogdat { get; set; }

        public decimal? RasPripay { get; set; }

        public decimal? PartCity { get; set; }

        public static FormInvoicesAllPropertyDto OMMap(OMAllProperty allProperty, int ordinal = 0)
        {
            return new FormInvoicesAllPropertyDto
            {
                Ordinal = ordinal,
                Ndog = allProperty.Ndog,
                Ndogdat = allProperty.Ndogdat,
                RasPripay = allProperty.RasPripay,
                PartCity = allProperty.PartCity
            };
        }
    }
}
