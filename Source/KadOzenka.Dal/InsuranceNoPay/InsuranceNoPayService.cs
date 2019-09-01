using Core.Shared.Extensions;
using ObjectModel.Insur;
using System.Collections.Generic;

namespace CIPJS.DAL.InsuranceNoPay
{
    public class InsuranceNoPayService
    {
        /// <summary>
        /// Получить список сведений об отказах в страховых выплатах по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Список сведений</returns>
        public List<OMNoPay> Get(long? id)
        {
            if (id.HasValue)
                return OMNoPay.Where(x => x.ObjId == id && x.ObjReestrId == OMFlat.GetRegisterId())
                   .SelectAll()
                   .Select(x => x.ParentInsuranceOrganization.FullName)
                   .Execute();

            return null;
        }

        /// <summary>
        /// Получить количество сведений об отказах в страховых выплатах по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Количество</returns>
        public int Count(long? id)
        {
            if (id.HasValue)
                return OMNoPay.Where(x => x.ObjId == id && x.ObjReestrId == OMFlat.GetRegisterId())
                    .GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt();

            return 0;
        }
    }
}
