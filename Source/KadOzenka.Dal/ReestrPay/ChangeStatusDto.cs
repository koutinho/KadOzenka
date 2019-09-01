using System;
using System.Collections.Generic;
using Core.SRD;
using Microsoft.AspNetCore.Http;
using ObjectModel.Directory;

namespace CIPJS.DAL.ReestrPay
{
    public class ChangeStatusDto
    {
        public List<long> Ids { get; set; }

        public ReestrPayStatus Status { get; set; }

        public string Note { get; set; }

        public DateTime? Date { get; set; }

        public IFormFile File { get; set; }

        public string UserName => SRDSession.Current.User.FullName;

        public bool NeedFile => Status == ReestrPayStatus.ApprovedDGI || Status == ReestrPayStatus.TransferredPayment;
    }
}