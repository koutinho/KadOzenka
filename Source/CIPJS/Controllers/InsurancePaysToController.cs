using CIPJS.DAL.InsurancePayTo;
using Microsoft.AspNetCore.Mvc;

namespace CIPJS.Controllers
{
    public class InsurancePaysToController : Controller
    {
        private readonly InsurancePayService _insurancePayService;

        public InsurancePaysToController(InsurancePayService insurancePayService)
        {
            _insurancePayService = insurancePayService;
        }

        [HttpPost]
        public int Count(long? id)
        {
            return _insurancePayService.Count(id);
        }
    }
}