using ObjectModel;
using ObjectModel.Core.SRD;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.User
{
    /// <summary>
    /// Класс работы с сущностью User.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Метод получения имени пользователя.
        /// </summary>
        /// <param name="id">core_srd_user.id</param>
        /// <returns></returns>
        public string GetUserNameById(long? id)
        {
            if(id == null)
            {
                return string.Empty;
            }
            var srdUser = OMUser.Where(x => x.Id == id)
                                .Select(x => x.Username)
                                 .Select(x => x.FullName)
                                  .Select(x => x.Name)
                                   .ExecuteFirstOrDefault();

            return srdUser?.Username ?? srdUser?.FullName ?? srdUser?.Name;
        }
    }
}
