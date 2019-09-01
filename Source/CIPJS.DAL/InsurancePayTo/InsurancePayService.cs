using Core.Shared.Extensions;
using ObjectModel.Insur;
using System.Collections.Generic;

namespace CIPJS.DAL.InsurancePayTo
{
    public class InsurancePayService
    {
        /// <summary>
        /// Получить список страхрвых выплат по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>список дел</returns>
        public List<OMPayTo> Get(long? id)
        {
            if (id.HasValue)
                return OMPayTo.Where(x => x.ObjId == id && x.ObjReestrId == OMFlat.GetRegisterId())
                   .SelectAll()
                   .Select(x => x.ParentInsuranceOrganization.FullName)
                   .Execute();

            return null;
        }

        /// <summary>
        /// Получить количество страхрвых выплат по идентификатору объекта
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Количество</returns>
        public int Count(long? id)
        {
            if (id.HasValue)
                return OMPayTo.Where(x => x.ObjId == id && x.ObjReestrId == OMFlat.GetRegisterId())
                    .GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt();

            return 0;
        }
    }
}
