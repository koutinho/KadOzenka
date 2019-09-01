using System.Collections.Generic;

namespace CIPJS.Models.InputPlat
{
    public class LinkToContractDto
    {
        public List<long> InputPlatIds { get; set; }

        public long ContractId { get; set; }

        public List<string> ExistsLinks { get; set; }
    }
}