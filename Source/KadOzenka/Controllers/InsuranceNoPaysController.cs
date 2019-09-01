using CIPJS.DAL.InsuranceNoPay;
using Microsoft.AspNetCore.Mvc;

namespace CIPJS.Controllers
{
    public class InsuranceNoPaysController : Controller
    {
        private readonly InsuranceNoPayService _insuranceNoPayService;

        public InsuranceNoPaysController(InsuranceNoPayService insuranceNoPayService)
        {
            _insuranceNoPayService = insuranceNoPayService;
        }

        [HttpPost]
        public int Count(long? id)
        {
            return _insuranceNoPayService.Count(id);
        }
    }
}