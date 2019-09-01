using ObjectModel.Insur;
using System.Linq;

namespace CIPJS.DAL.LivingPremiseInsurCost
{
    public class LivingPremiseInsurCostService
    {
        private LivingPremiseInsurCostDto CreateEmpty()
        {
            return new LivingPremiseInsurCostDto
            {
                Id = -1
            };
        }

        public LivingPremiseInsurCostDto Get(long? id)
        {
            if (id.HasValue)
            {
                OMLivingPremiseInsurCost livPrem = OMLivingPremiseInsurCost.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                if (livPrem != null)
                {
                    return new LivingPremiseInsurCostDto
                    {
                        Id = livPrem.Id,
                        DateBegin = livPrem.DateBegin,
                        Condition = livPrem.Condition,
                        Value = livPrem.Value
                    };
                }
            }

            return CreateEmpty();
        }
    }
}
