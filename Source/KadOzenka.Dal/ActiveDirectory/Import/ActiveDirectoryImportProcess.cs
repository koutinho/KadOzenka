using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

using Core.Register.LongProcessManagment;
using Core.SRD;
using Core.SRD.DAL;
using ObjectModel.Core.LongProcess;
using ObjectModel.Core.SRD;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using System.Security.Principal;
using Core.Main.ConfigParam.SystemCommon;
using Core.Register.ActiveDirectory;

namespace CIPJS.DAL.ActiveDirectory.Import
{
    public class ActiveDirectoryImportProcess : ILongProcess
    {
        private const string defaultDepartmentName = "ActiveDirectory Department";

        private StringBuilder logger = new StringBuilder();

        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            this.logger.Append("\r\n");
            this.Log("Запуск ActiveDirectoryImportProcess");
            try
            {
                string domain = SystemCommonConfiguration.SystemCommon.ActiveDirectoryDomain;

                /* Присоединиться к ActiveDirectory */
                this.Log(string.Format("Соединяемся с ActiveDirectory({0})", domain));
                ActiveDirectoryService service = new ActiveDirectoryService();
                /* Извлекаем список пользователей */
                this.Log("Извлекаем список пользователей");
                List<ActiveDirectoryItem> adItems = service.GetActiveDirectoryItems(domain);

                /* Для каждого найденого пользователя в ActiveDirectory... */
                foreach (ActiveDirectoryItem item in adItems)
                {
                    /* Найти пользователя в найшей системе */
                    OMUser user = SRDUserDAL.GetUserByUsername(item.msDSPrincipalName);
                    if (user == null)
                    {
                        this.Log(string.Format("Пользователь {0} не найден в нашей системе. Создаем его", item.msDSPrincipalName));
                        /* Добавить нового пользователя */
                        user = new OMUser();
                        user.Username = item.msDSPrincipalName;
                        //user.ExtId = 
                        this.AliasAD2OMUser(item, user);
                    }
                    else
                    {
                        this.Log(string.Format("Пользователь {0} найден в нашей системе. Обновляем его атрибуты", item.msDSPrincipalName));
                        /* Изменить атрибуты пользователя */
                        this.AliasAD2OMUser(item, user);
                    }

                    /* Сохранить пользователя */
                    SRDUserDAL.SaveFromActiveDirectoryUser(user);
                }
            }
            catch(Exception exception)
            {
                this.LogError(null, exception);
            }
            finally
            {
                processQueue.Message = "CIPJS.DAL.ActiveDirectory.Import.ActiveDirectoryImportProcess";
                processQueue.Log = logger.ToString();
                processQueue.Save();
            }
        }

        private void AliasAD2OMUser(ActiveDirectoryItem item, OMUser user)
        {
            user.FullName = string.Format("{0} {1}", item.FirstName, item.LastName);
            user.Name = item.DisplayName;
            user.Phone = item.Phone;
            user.Email = item.Email;
            user.Position = item.Title;

            OMDepartment department = SRDUserDAL.GetDepartmentByName(item.Department);
            /* Проверить есть ли такой отдел */
            if (department == null)
            {
                /* Использовать отдел по-умолчанию */
                department = SRDUserDAL.GetDepartmentByName(defaultDepartmentName);
                if (department == null)
                {
                    department = new OMDepartment();
                    department.Name = defaultDepartmentName;
                    SRDUserDAL.SaveDepartment(department);
                }
            }
            user.DepartmentId = department.Id;
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {
            this.Log(string.Format("errorId:{0}, objectId:{1}, Exception: {2}", errorId??0, objectId??0, ex.Message));
        }

        private void Log(string message)
        {
            this.logger.Append("\r\n");
            this.logger.Append(string.Format("{0}: {1}", DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss.fff"), message));
        }

        public bool Test()
        {
            return true;
        }
    }
}
