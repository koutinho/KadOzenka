using ObjectModel.Insur;
using System.Linq;

namespace CIPJS.DAL.Fsp
{
    public class SkPolicySvdService
    {

        /// <summary>
        /// Получить Полис/свидетельство по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>ФСП</returns>
        public OMPolicySvd Get(long? id)
        {
            OMPolicySvd entity = OMPolicySvd.Where(x => x.EmpId == id)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();

            return entity;
        }
    }
}
