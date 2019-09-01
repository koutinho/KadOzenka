using System;

namespace CIPJS.Models.Tenements
{
    public class EgrnRightDto
    {
        public string Type { get; set; }

        public string Area { get; set; }

        public string CadastralNumber { get; set; }

        public string RightKind { get; set; }

        public string Number { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ShareSize { get; set; }

        public string ShareSizeText { get; set; }

        public string ShareArea { get; set; }

        public string Note { get; set; }
    }
}