using CIPJS.DAL.Invoice;
using CIPJS.DAL.ReestrPayInvoice;
using CIPJS.Models.ReestrPayInvoice;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CIPJS.Controllers
{
    public class ReestrPayInvoiceController : BaseController
    {
        private ReestrPayInvoiceService _reestrPayInvoiceService;

        public ReestrPayInvoiceController()
        {
            _reestrPayInvoiceService = new ReestrPayInvoiceService();
        }

        public ViewResult GroupView(long payId)
        {
            return View(payId);
        }

        public ContentResult GroupRead(long payId)
        {
            List<GroupDto> groupList = new List<GroupDto>();
            
            foreach(OMInvoiceSvod row in _reestrPayInvoiceService.GetGroupInvoiceList(payId))
            {
                groupList.Add(GroupDto.OMMap(row));
            }

            return Content(JsonConvert.SerializeObject(groupList), "application/json");
        }

        public ContentResult GroupDetailRead(long payId, string subjectName, string numInvoice, DateTime? dateInvoice)
        {
            return Content(JsonConvert.SerializeObject(_reestrPayInvoiceService.GetGroupDetailList(payId, subjectName, numInvoice, dateInvoice)
                .Select(x => GroupDetailDto.OMMap(x))
                .ToList()), "application/json");
        }
    }
}